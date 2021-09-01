using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orange.Common.EntityFramework;
using Orange.Common.Utilities;
using Orange.Common.EntityFramework;

namespace Orange.Common.DataAccess
{
    public class ServicesFailedRequestsDataAccess : IServicesFailedRequestsDataAccess
    {
        private readonly IEntityFramworkUtilties _entityFramworkUtilties;
        public ServicesFailedRequestsDataAccess(IEntityFramworkUtilties entityFramworkUtilties)
        {
            _entityFramworkUtilties = entityFramworkUtilties;
        }
        public bool Add(ServicesFailedRequest request)
        {
            return _entityFramworkUtilties.SaveChanges<CommonModel>(dbModel => Add(dbModel, request));
        }
        void Add(CommonModel dbModel, ServicesFailedRequest request)
        {
            request.CreatedDate = DateTime.Now;
            dbModel.ServicesFailedRequests.Add(request);
        }
    }
}
