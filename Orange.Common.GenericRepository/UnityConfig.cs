using Unity;

namespace Orange.Common.GenericRepository
{
    public class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>(TypeLifetime.PerResolve);
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>),TypeLifetime.PerResolve);
        }
    }
}
