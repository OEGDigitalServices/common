using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Utilities
{
    public class HttpClientManager : IHttpClientManager
    {
        #region Props
        private static readonly HttpClient _client = new HttpClient();
        #endregion

        #region CTOR
        public HttpClientManager()
        {
        }
        #endregion

        #region Methods
        public async Task<T> Get<T>(string url, Dictionary<string, string> headers = null)
        {
            FillHeaders(headers);
            var response = await _client.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var content = JsonConvert.DeserializeObject<T>(stringContent);
            return content;
        }

        public async Task<T> Post<T>(string url, Dictionary<string, string> headers = null, int timeoutInSeconds = 100)
        {
            var client = new HttpClient();
            FillHeaders(headers);
            client.Timeout = new TimeSpan(0, 0, timeoutInSeconds);
            var response = await client.PostAsync(url, null).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var desrializedContent = JsonConvert.DeserializeObject<T>(stringContent);
            client.Timeout = new TimeSpan(0, 0, 100);
            return desrializedContent;
        }
        public async Task<T> Post<T, TBody>(string url, TBody body, Dictionary<string, string> headers = null, int timeoutInSeconds = 100)
            where TBody : class
        {
            var client = new HttpClient();
            FillHeaders(headers);
            var serializedContent = JsonConvert.SerializeObject(body);
            var content = new StringContent(serializedContent, Encoding.UTF8, "application/json");
            client.Timeout = new TimeSpan(0, 0, timeoutInSeconds);
            var response = await client.PostAsync(url, content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var desrializedContent = JsonConvert.DeserializeObject<T>(stringContent);
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

        public async Task<string> Get(string url, Dictionary<string, string> headers = null)
        {
            FillHeaders(headers);
            var response = await _client.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false); ;
        }
        #endregion
    }
}
