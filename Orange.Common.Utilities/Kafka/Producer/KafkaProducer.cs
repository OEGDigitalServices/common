using System;
using Confluent.Kafka;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Orange.Common.Utilities
{
    public class KafkaProducer : IKafkaProducer
    {
        #region fields

        private readonly KafkaConfigurations _configurations;
        private readonly ILogger _logger;
        private readonly Action<DeliveryReport<Null, string>> _handler;

        #endregion

        #region ctor

        public KafkaProducer(KafkaConfigurations kafkaConfigurations, ILogger logger)
        {
            _configurations = kafkaConfigurations;
            _logger = logger;

            #region DeliveryReport 

            _handler = report =>
            {
                if(report.Error.IsError)
                {
                    _logger.LogError(report.Error.Reason, new Exception(JsonConvert.SerializeObject(report)));
                }
            };

            #endregion
        }

        #endregion

        #region Log

        public void Log(object message, string topicName)
        {
            Task.Run(() =>
            {
                try
                {
                    ProducerConfig config = new ProducerConfig
                    {
                        BootstrapServers = _configurations.BootstrapServers,
                        LingerMs = _configurations.LingerMs,
                        MessageTimeoutMs = _configurations.MessageTimeoutMs
                    };

                    using (var p = new ProducerBuilder<Null, string>(config).Build())
                    {
                        p.Produce(topicName, new Message<Null, string> { Value = JsonConvert.SerializeObject(message) }, _handler);
                        p.Flush();
                    }
                }
                catch (Exception exp)
                {
                    _logger.LogError(exp.Message, exp);
                }
            });
        }

        #endregion
    }
}
