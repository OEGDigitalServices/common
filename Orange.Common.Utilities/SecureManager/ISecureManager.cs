using System.Threading.Tasks;

namespace Orange.Common.Utilities
{
    public interface ISecureManager
    {
        T CallSecureConnect<T>(string endpoint, object requestInput, string verb);
        T CallSecureConnect<T>(string v, object p, object post);
    }
}
