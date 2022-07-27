using log4net;
using log4net.Config;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Orange.Common.Utilities
{
    public class Logger : ILogger
    {
        public Logger()
        {

        }
        static Logger()
        {
            string mongoConnectionString = GetAppSetting("EAI_Exceptions_ConnectionString");
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

            string logId = Guid.NewGuid().ToString();
            _log.Error($"LogId: {logId}: {message}", exception);

            if (LoggingExceptionsToMongoEnabled())
            {
                LogToMongo(exception, logId);
            }

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

        private static bool LoggingExceptionsToMongoEnabled()
        {
            bool.TryParse(GetAppSetting("EAI_Exceptions_IsEnabled"), out bool enabled);
            return enabled;
        }

        private static void LogToMongo(Exception exception, string logId)
        {
            var doccument = new BsonDocument
            {
                { "Message", exception.Message },
                { "Trace", exception.StackTrace },
                { "Site", HostingEnvironment.ApplicationHost.GetSiteName() },
                { "Directory", HostingEnvironment.ApplicationHost.GetVirtualPath() },
                { "ServerIP", GetInternalServerIP() },
                { "CreatedDate" , DateTime.Now.ToString() },
                { "LogId", logId }
            };

            Task.Run(() =>
            {
                _client.GetDatabase(GetAppSetting("EAI_Exceptions_Database"))
                    .GetCollection<BsonDocument>(GetAppSetting("EAI_Exceptions_Collection"))
                    .InsertOne(doccument);
            });
        }

        private static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        private static string GetInternalServerIP()
        {
            return HttpContext.Current?.Request.ServerVariables["LOCAL_ADDR"] ?? string.Empty;
        }
    }
}
