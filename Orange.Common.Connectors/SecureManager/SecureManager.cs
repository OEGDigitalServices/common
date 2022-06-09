using Newtonsoft.Json;
using Orange.Common.Utilities;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Connectors
{
    public class SecureManager : ISecureManager
    {
        #region Props

        private readonly ILogger _logger;

        #endregion

        #region Ctors

        public SecureManager(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region CallSecureConnect

        public T CallSecureConnect<T>(string endpoint, object requestInput)
        {
            try
            {
                string response = string.Empty;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
                httpWebRequest.ContentType = Strings.Services.JsonContentType;
                httpWebRequest.Method = Strings.Services.PostVerb;
                InitiateSSLTrust();

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(requestInput));
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        response = streamReader.ReadToEnd();
                    }
                }
                T model = JsonConvert.DeserializeObject<T>(response);
                return model;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return default;
            }
        }

        #region Helpers

        /// <summary>
        /// Change SSL checks so that all checks pass
        /// </summary>
        void InitiateSSLTrust()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback =
                    new RemoteCertificateValidationCallback(
                        delegate
                        { return true; }
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex, false);
            }
        }

        #endregion

        #endregion
    }
}
