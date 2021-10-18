using Orange.Common.Entities;

namespace Orange.Common.Business
{
    public interface IServicesFailedRequestsManager
    {
        bool Add(ServicesFailedRequest request);
    }
}
