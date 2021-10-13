using Unity;
using Unity.Injection;

namespace Orange.Common.Utilities
{
    public class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IServicesUtilties, ServicesUtilities>(TypeLifetime.PerResolve);
            container.RegisterType<IUtilities, Utilities>(TypeLifetime.PerResolve);
            container.RegisterType<ILogger, Logger>(TypeLifetime.PerResolve);
            container.RegisterType<ISecurityUtilities, SecurityUtilities>(TypeLifetime.PerResolve);
            container.RegisterType<IEntityFramworkUtilties, EntityFramworkUtilties>(TypeLifetime.PerResolve);
            container.RegisterType<IUnityResolver, UnityResolver>(TypeLifetime.PerResolve, new InjectionConstructor(container));
            container.RegisterType<ISMSUtilities, SMSUtilities>(TypeLifetime.PerResolve);
            container.RegisterType<IEmailUtilities, EmailUtilities>(TypeLifetime.PerResolve);
            container.RegisterType<ISmsMessageManager, SmsMessageManager>(TypeLifetime.PerResolve);
            container.RegisterType<IGlobalResourceReader, GlobalResourceReader>(TypeLifetime.PerResolve);
            container.RegisterType<IHttpClientManager, HttpClientManager>(TypeLifetime.PerResolve);


        }
    }
}
