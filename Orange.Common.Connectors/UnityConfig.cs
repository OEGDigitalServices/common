using Unity;

namespace Orange.Common.Connectors
{
    public class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ISecureManager, SecureManager>();
            Utilities.UnityConfig.RegisterTypes(container);
        }
    }
}
