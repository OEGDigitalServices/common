using Microsoft.Practices.Unity.Configuration;
using System;
using Unity;

namespace Orange.Common.Utilities
{
    public class UnityResolver : IUnityResolver
    {

        private readonly Lazy<IUnityContainer> _container;

        public UnityResolver(string containerName)
        {
            _container = new Lazy<IUnityContainer>(() =>
            {
                return new UnityContainer().AddExtension(new Diagnostic()).LoadConfiguration(containerName);
            });
        }
        public object Resolve(Type type)
        {
            return _container.Value.Resolve(type);
        }
        //public object ResolveType(Type type,string name)
        //{
        //    return _container.Value.Resolve(type,name) as Type;
        //}

        public T Resolve<T>(string name)
        {
            return _container.Value.Resolve<T>(name);
        }

        public T Resolve<T>()
        {
            return _container.Value.Resolve<T>();
        }
        public object ResolveType(Type type)
        {
            return _container.Value.Resolve(type);
        }
    }
}
