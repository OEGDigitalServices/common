using Orange.Common.Utilities;
using Unity;
namespace Orange.Common.Profile.Membership
{
    public class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            Utilities.UnityConfig.RegisterTypes(container);
        }
    }
}
