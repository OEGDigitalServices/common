using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orange.Common.Utilities
{
    public class HttpClientManager : IHttpClientManager
    {
        #region Props
        private static readonly HttpClient _client;
        private object obj = new object();
        private readonly IUtilities _utilities;
        #endregion

        #region CTOR
        static HttpClientManager()
        {
            _client = new HttpClient();
        }
        public HttpClientManager(IUtilities utilities)
        {
            _utilities = utilities;
        }
        #endregion

        #region Methods
        public async Task<T> Get<T>(string url, Dictionary<string, string> headers = null, int timeoutInSeconds = 100)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

            FillHeaders(headers);
            var response = await _client.GetAsync(url, cts.Token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var content = JsonConvert.DeserializeObject<T>(stringContent);
            return content;
        }

        public async Task<T> Post<T>(string url, Dictionary<string, string> headers = null, int timeoutInSeconds = 100)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

            FillHeaders(headers);
            var response = await _client.PostAsync(url, null, cts.Token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var desrializedContent = JsonConvert.DeserializeObject<T>(stringContent);
            return desrializedContent;
        }
        public async Task<T> Post<T, TBody>(string url, TBody body, Dictionary<string, string> headers = null, int timeoutInSeconds = 100, bool disableSSL = false)
            where TBody : class
        {
            if (disableSSL)
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

            FillHeaders(headers);
            var serializedContent = JsonConvert.SerializeObject(body);
            var content = new StringContent(serializedContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content, cts.Token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var desrializedContent = JsonConvert.DeserializeObject<T>(stringContent);
            return desrializedContent;
        }

        public async Task<T> PostXml<T, TBody>(string url, TBody body, Dictionary<string, string> headers = null, int timeoutInSeconds = 100) where TBody : class
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

            var serializedContent = _utilities.ObjectToXML<TBody>(body);
            var content = new StringContent(serializedContent, Encoding.UTF8, "application/xml");

            var response = await _client.PostAsync(url, content, cts.Token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return (T)Convert.ChangeType(stringContent, typeof(T));
        }

        public async Task<object> PostAsJson<T, TBody>(string url, TBody body, Dictionary<string, string> headers = null, bool disableSSLVerification = false) where TBody : class
        {
            FillHeaders(headers);
            string serializedContent = JsonConvert.SerializeObject((object)body);
            StringContent content = new StringContent(serializedContent, Encoding.UTF8, "application/json");
            if (disableSSLVerification)
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            HttpResponseMessage response = await _client.PostAsync(url, content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string formatted = (await response.Content.ReadAsStringAsync().ConfigureAwait(false)).Replace("null", "\" \"").Replace("\"", "'");
            return JsonConvert.DeserializeObject<object>(formatted);
        }

        public Task<TOut> PostAsJson<TOut, TBody>(string url,
            TBody body,
            Dictionary<string, string> headers = null,
            bool disableSSLVerification = false,
            int timeoutInSeconds = 100,
            string authorizationToken = null,
            bool useCamelCase = false) where TBody : class
        {
            return SendRequestAsJson<TOut, TBody>(url, body, headers, authorizationToken, disableSSLVerification, timeoutInSeconds, useCamelCase, HttpMethod.Post);
        }

        #endregion

        #region Helpers

        private void FillHeaders(Dictionary<string, string> headers)
        {
            lock (obj)
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Clear();
                if (!_client.DefaultRequestHeaders.Accept.Contains(new MediaTypeWithQualityHeaderValue("application/json")))
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (headers == null || headers.Count == 0) return;
                foreach (var header in headers)
                    _client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        private string ConcatQueryParameters(string url, Dictionary<string, string> QueryParameters)
        {
            if (QueryParameters == null || QueryParameters.Count == 0) return url;
            StringBuilder parameters = new StringBuilder(url + "?");
            foreach (var parameter in QueryParameters)
            {
                parameters.Append(parameter.Key + "=" + parameter.Value + "&");
            }
            var concatenatedURL = parameters.ToString();
            if (concatenatedURL.EndsWith("&"))
                concatenatedURL.Remove(concatenatedURL.Length - 2, 1);
            return concatenatedURL;
        }

        public async Task<string> Get(string url, Dictionary<string, string> headers = null, int timeoutInSeconds = 100)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

            FillHeaders(headers);
            var response = await _client.GetAsync(url, cts.Token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false); ;
        }

        private async Task<TOut> SendRequestAsJson<TOut, TBody>(string url,
            TBody body,
            Dictionary<string, string> headers,
            string authorizationToken,
            bool disableSSLVerification,
            int timeoutInSeconds,
            bool useCamelCase,
            HttpMethod method) where TBody : class
        {
            using (var requestMessage = new HttpRequestMessage(method, url))
            {
                if (disableSSLVerification)
                    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));
                FillMessageHeaders(requestMessage, headers, authorizationToken);
                string serializedContent = JsonConvert.SerializeObject(body, new JsonSerializerSettings
                {

                    ContractResolver = useCamelCase 
                        ? new CamelCasePropertyNamesContractResolver()
                        : new DefaultContractResolver()
                });
                requestMessage.Content = new StringContent(serializedContent, Encoding.UTF8, "application/json");
                var response = await _client.SendAsync(requestMessage, cts.Token).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var content = JsonConvert.DeserializeObject<TOut>(stringContent);
                return content;
            }
        }

        private void FillMessageHeaders(HttpRequestMessage requestMessage, Dictionary<string, string> headers, string authorizationToken)
        {
            if (authorizationToken != null)
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(Strings.AuthorizationSchemes.Bearer, authorizationToken);

            if (headers == null)
                return;

            foreach (var header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }
        }

        #endregion
    }
}
