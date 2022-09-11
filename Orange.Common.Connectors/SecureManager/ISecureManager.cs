using System.Threading.Tasks;

namespace Orange.Common.Connectors
{
    public interface ISecureManager
    {
        T CallSecureConnect<T>(string endpoint, object requestInput);
    }
}
