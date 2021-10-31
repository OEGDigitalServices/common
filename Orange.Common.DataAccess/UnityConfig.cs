using Orange.Common.DataAccess;
using Orange.Common.Utilities;
using Unity;
namespace Orange.Common.DataAccess
{
    public class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IChannelsDataDataAccess, ChannelsDataDataAccess>();
            container.RegisterType<IChannelsPrivilegesDataAccess, ChannelsPrivilegesDataAccess>();
            container.RegisterType<IChannelsTokensDataAccess, ChannelsTokensDataAccess>();
            container.RegisterType<IServicesFailedRequestsDataAccess, ServicesFailedRequestsDataAccess>();
            container.RegisterType<IQueuedEmailDataAccess, QueuedEmailDataAccess>();
        }
    }
}
