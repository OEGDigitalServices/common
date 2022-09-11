using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Entities
{
    public class HttpResponseModel<T>
    {
        public T ResponseData { get; set; }
        public double ElapsedMillisecond { get; set; }
        public short StatusCode { get; set; }
        public string URL { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}
