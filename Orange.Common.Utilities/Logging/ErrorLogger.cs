using log4net;
using log4net.Config;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Orange.Common.Utilities
{
    public class Logger : ILogger
    {
        public Logger()
        {

        }
        static Logger()
        {
            var mongoConnectionString = System.Configuration.ConfigurationManager.AppSettings["MongoExceptionsConnectionString"];
            _client = new MongoClient(mongoConnectionString);
            Initialize();
        }
        public static void Initialize()
        {
            XmlConfigurator.Configure();
        }
        /// <summary>
        /// 
        /// </summary>
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly MongoClient _client;
        /// <summary>
        /// Log Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="rethrowException"></param>
        public void LogError(string message, Exception exception, bool rethrowException = false)
        {
            if (message != null && message.Length > 4000)
            {
                message = message.Substring(0, 4000);//4000 maximum size of Message field in DB
            }
            _log.Error(message, exception);
            LogToMongo(message);

            if (rethrowException)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Log Debug
        /// </summary>
        /// <param name="message"></param>
        public void LogDebug(string message)
        {
            _log.Debug(message);
        }

        private static void LogToMongo(string message)
        {
            var doccument = new BsonDocument { { "Message", message } };
            _client.GetDatabase("Logging")
                .GetCollection<BsonDocument>("EAI_Exceptions")
                .InsertOne(doccument);
        }
    }
}
