using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        private readonly HttpClient _client;
        private readonly ILogger _logger;
        #endregion

        #region CTOR

        public HttpClientManager(HttpClient client, ILogger logger)
        {
            _client = client;
            _logger = logger;
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
        public async Task<T> Post<T, TBody>(string url, TBody body, Dictionary<string, string> headers = null, int timeoutInSeconds = 100)
            where TBody : class
        {
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

        public async Task<T> PostXml<T, TBody>(string url, TBody body, Dictionary<string, string> headers = null, int timeoutInSeconds = 100)
            where TBody : class
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

            FillHeaders(headers);
            var serializedContent = JsonConvert.SerializeObject(body);
            var content = new StringContent(serializedContent, Encoding.UTF8, "application/xml");
            var response = await _client.PostAsync(url, content, cts.Token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var desrializedContent = JsonConvert.DeserializeObject<T>(stringContent);
            return desrializedContent;
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

        public async Task<object> PostAsJson<T, TBody>(string url, TBody body, Dictionary<string, string> headers = null, int timeoutInSeconds = 100)
            where TBody : class
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

            FillHeaders(headers);
            var serializedContent = JsonConvert.SerializeObject(body);
            var content = new StringContent(serializedContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content, cts.Token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var formatted = stringContent.Replace("null", "\" \"").Replace("\"", "\'");
            var desrializedContent = JsonConvert.DeserializeObject<object>(formatted);
            return desrializedContent;
        }
        #endregion

        #region Helpers
        private void FillHeaders(Dictionary<string, string> headers)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (headers == null || headers.Count == 0) return;
            foreach (var header in headers)
            {
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
        #endregion
    }
}
