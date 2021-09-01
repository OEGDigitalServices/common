using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orange.Common.DataAccess;
using Orange.Common.Entities;

namespace Orange.Common.Business
{
  public  class ServicesFailedRequestsManager : IServicesFailedRequestsManager
    {
        private  readonly  IServicesFailedRequestsDataAccess _servicesFailedRequestsDataAccessManager;
        public ServicesFailedRequestsManager(IServicesFailedRequestsDataAccess servicesFailedRequestsDataAccessManager)
        {
            _servicesFailedRequestsDataAccessManager = servicesFailedRequestsDataAccessManager;
        }
        public bool Add(ServicesFailedRequest request)
        {
            return _servicesFailedRequestsDataAccessManager.Add(new EntityFramework.ServicesFailedRequest(){ActionName = request.ActionName,Channel = request.Channel,ControllerName = request.ControllerName,Dial = request.Dial,ErrorCode = request.ErrorCode,ErrorDescription = request.ErrorDescription});
        }
    }
}
