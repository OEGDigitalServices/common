using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orange.Common.WebApi.TokenAuthUtilities
{
    public class Log4Net : GSM.Profile.Auth.TokenAuthentication.BL.Utilities.Logger.ILogger
    {
        public Log4Net()
        {

        }
        static Log4Net()
        {
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
    }
}