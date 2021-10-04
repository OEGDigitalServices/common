using Orange.Common.Utilities;
using Unity;
namespace Orange.Common.Business
{
    public class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {

            container.RegisterType<IChannelsTokensManager, ChannelsTokensManager>();
            container.RegisterType<IServicesFailedRequestsManager, ServicesFailedRequestsManager>();
           
            Common.Utilities.UnityConfig.RegisterTypes(container);
            Common.DataAccess.UnityConfig.RegisterTypes(container);
            Common.GenericRepository.UnityConfig.RegisterTypes(container);
        }
    }
}
