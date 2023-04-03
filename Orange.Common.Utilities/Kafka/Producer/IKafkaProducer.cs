namespace Orange.Common.Utilities
{
    public interface IKafkaProducer
    {
        void Log(object message, string topicName);
    }
}
