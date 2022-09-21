using Orange.Common.Utilities;
using Unity;
namespace Orange.Common.Profile
{
    public class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            Utilities.UnityConfig.RegisterTypes(container);

            container.RegisterType<IUserDialsDataAccess, UserDialsDataAccess>();
            container.RegisterType<IAuthenticationContext, AuthenticationContext>();
            container.RegisterType<IProfileContext, ProfileContext>();
            container.RegisterType<IProfileUtilities, ProfileUtilities>();
        }
    }
}
