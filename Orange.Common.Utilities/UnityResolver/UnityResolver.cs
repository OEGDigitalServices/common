using System;
using Unity;
using Unity.Resolution;

namespace Orange.Common.Utilities
{
    public class UnityResolver : IUnityResolver
    {

        private readonly IUnityContainer _container;

        public UnityResolver(IUnityContainer containerName)
        {
            _container = containerName;
        }
        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
        //public object ResolveType(Type type,string name)
        //{
        //    return _container.Resolve(type,name) as Type;
        //}

        public T Resolve<T>(string name)
        {
            return _container.Resolve<T>(name);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
        public T Resolve<T>(ResolverOverride[] resolverOverrides)
        {
            return _container.Resolve<T>(resolverOverrides);
        }
        public object ResolveType(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
