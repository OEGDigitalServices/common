using System;
using Confluent.Kafka;
using Newtonsoft.Json;
using System.Threading;

namespace Orange.Common.Utilities
{
    public class KafkaProducer : IKafkaProducer
    {
        #region fields

        private readonly KafkaConfigurations _configurations;
        private readonly ILogger _logger;

        #endregion

        #region ctor

        public KafkaProducer(KafkaConfigurations kafkaConfigurations, ILogger logger)
        {
            _configurations = kafkaConfigurations;
            _logger = logger;
        }

        #endregion

        #region Log


        public void Log(object message, string topicName)
        {
            var sendMessage = new Thread(() =>
            {
                try
                {
                    ProducerConfig config = new ProducerConfig
                    {
                        BootstrapServers = _configurations.BootstrapServers,
                        LingerMs = _configurations.LingerMs,
                        MessageTimeoutMs = _configurations.MessageTimeoutMs
                    };

                    using (var p = new ProducerBuilder<Null, string>(config).SetErrorHandler((producer, error) =>
                    {
                        _logger.LogError(error.Reason, new Exception(JsonConvert.SerializeObject(error)));
                    }).Build())
                    {
                        p.Produce(topicName, new Message<Null, string> { Value = JsonConvert.SerializeObject(message) }, handler);

                        // wait for up to 10 seconds for any inflight messages to be delivered.
                        //p.Flush(TimeSpan.FromSeconds(5));
                        p.Flush();
                    }
                }
                catch (Exception exp)
                {
                    _logger.LogError(exp.Message, exp);
                }
            });
            sendMessage.Start();
        }

        #endregion

        #region DeliveryReport 

        Action<DeliveryReport<Null, string>> handler = r =>
             Console.WriteLine(!r.Error.IsError
               ? $"Delivered message to {r.TopicPartitionOffset}"
               : $"Delivery Error : {r.Error.Reason}");

        #endregion
    }
}
