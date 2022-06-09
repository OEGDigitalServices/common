using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orange.Common.Utilities
{
    public interface IHttpClientManager
    {
        Task<string> Get(string url, Dictionary<string, string> headers = null, int timeoutInSeconds = 100);
        Task<T> Get<T>(string url, Dictionary<string, string> headers = null, int timeoutInSeconds = 100);
        Task<T> Post<T>(string url, Dictionary<string, string> headers = null, int timeoutInSeconds = 100);
        Task<T> Post<T, TBody>(string url, TBody body, Dictionary<string, string> headers = null, int timeoutInSeconds = 100, bool disableSSL = false)
            where TBody : class;
        Task<T> PostXml<T, TBody>(string url, TBody body, Dictionary<string, string> headers = null, int timeoutInSeconds = 100)
            where TBody : class;

        Task<object> PostAsJson<T, TBody>(string url, TBody body, Dictionary<string, string> headers = null, int timeoutInSeconds = 100)
            where TBody : class;
    }
}
