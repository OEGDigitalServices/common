using Orange.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Orange.Common.WebApi.Controllers
{
    [TokenAuthentication]
    public class TestController : ApiController
    {
        public IUtilities _Utilities { get; }

        public TestController(IUtilities utilities)
        {
            _Utilities = utilities;
            _Utilities.GetAppSetting("7amada");
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}