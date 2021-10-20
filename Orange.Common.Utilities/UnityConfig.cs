using Unity;
using Unity.Lifetime;

namespace Orange.Common.Utilities
{
    public class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IServicesUtilties, ServicesUtilities>();
            container.RegisterType<IUtilities, Utilities>();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<ISecurityUtilities, SecurityUtilities>();
            container.RegisterType<IEntityFramworkUtilties, EntityFramworkUtilties>();
            container.RegisterType<IUnityResolver, UnityResolver>();
            container.RegisterType<ICachingUtilities, CachingUtilities>();
            container.RegisterType<ISharePointUtilities, SharePointUtilities>();
        }
    }
}
