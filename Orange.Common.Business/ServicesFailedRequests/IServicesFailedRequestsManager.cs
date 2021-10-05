using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orange.Common.Entities;

namespace Orange.Common.Business
{
    public interface IServicesFailedRequestsManager
    {
        bool Add(ServicesFailedRequest request);
    }
}
