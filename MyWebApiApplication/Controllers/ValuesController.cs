using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyWebApiApplication.CustomConstraints;

namespace MyWebApiApplication.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [Route("shakti/{id:Divisibleby10}", Name ="JackAss",Order =0)]
        public IEnumerable<string> GetA(int id)
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        //[AcceptVerbs("PURGE")]
        //[Route("{id:Divisibleby10}")]
        [Route("shakti/{id:int}", Name = "JackAss1", Order = 1)]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
