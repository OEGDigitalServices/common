using System;

namespace Orange.Common.Utilities
{
    public interface IUnityResolver
    {
        T Resolve<T>(string name);
        T Resolve<T>();
        object ResolveType(Type type);
    }
}
