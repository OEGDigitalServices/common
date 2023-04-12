using Orange.Common.Business.OrangeTriplePlay;
using Unity;
namespace Orange.Common.Business
{
    public class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {

            container.RegisterType<IChannelsTokensManager, ChannelsTokensManager>();
            container.RegisterType<IServicesFailedRequestsManager, ServicesFailedRequestsManager>();
            container.RegisterType<INotificationManager, SendMailNotificationManager>();
            container.RegisterType<IChannelsDataManager, ChannelsDataManager>();
            container.RegisterType<IChannelsPrivilegesManager, ChannelsPrivilegesManager>();
            container.RegisterType<IChannelsTokensManager, ChannelsTokensManager>();
            container.RegisterType<IProfileManager, ProfileManager>();            
            container.RegisterType<ICaptchaService, CaptchaService>();
            container.RegisterType<IDSLBasicAuthenticationTokenManager, DSLBasicAuthenticationTokenManager>();
            container.RegisterType<IOrangeTPManager, OrangeTPManager>();
            container.RegisterType<IOrangeCashManager, OrangeCashManager>();

            Utilities.UnityConfig.RegisterTypes(container);
            DataAccess.UnityConfig.RegisterTypes(container);
            Profile.UnityConfig.RegisterTypes(container);
        }
    }
}
