using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orange.Common.Utilities
{
    public interface IHttpClientManager
    {
        Task<string> Get(string url, Dictionary<string, string> headers = null);
        Task<T> Get<T>(string url, Dictionary<string, string> headers = null);
        Task<T> Post<T>(string url, Dictionary<string, string> headers = null);
        Task<T> Post<T, TBody>(string url, TBody body, Dictionary<string, string> headers = null)
            where TBody : class;
        Task<T> PostXml<T, TBody>(string url, TBody body, Dictionary<string, string> headers = null)
            where TBody : class;
    }
}
