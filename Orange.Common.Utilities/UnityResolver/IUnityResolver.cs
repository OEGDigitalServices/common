using System;
using Unity.Resolution;

namespace Orange.Common.Utilities
{
    public interface IUnityResolver
    {
        T Resolve<T>(string name);
        T Resolve<T>();
        T Resolve<T>(ResolverOverride[] resolverOverrides);
        object ResolveType(Type type);
    }
}
