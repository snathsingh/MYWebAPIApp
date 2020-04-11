using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using MyWebApiApplication.CustomActionFilters;
using MyWebApiApplication.CustomConstraints;
using MyWebApiApplication.CustomDelegatingHandler;

namespace MyWebApiApplication.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        //[AcceptVerbs("HEAD","GET")]
        //[Route("shakti/{id:Divisibleby10}", Name ="JackAss",Order =1)]
        //[ClientSideCaching(duration:20,typeOfCache:CacheType.NoCache)]
        public IEnumerable<string> GetA(int id,int b)
        {
            Thread.Sleep(2000);
            return new string[] { "value1", "value2",Request.GetApiKey(),Request.RequestUri.ToString() };
        }

        // GET api/values/5
        //[AcceptVerbs("VIEW","GET")]
        ////[Route("{id:Divisibleby10}")]
        [Route("api/values/{id:int}", Name = "JackAss1", Order = 2)]
        public HttpResponseMessage Get(int id)
        {
            //return "value";            
            //string uri = Url.Link("JackAss", new { id = id });
            //var response = Request.CreateResponse(HttpStatusCode.MovedPermanently);
            //response.Headers.Location = new Uri(uri);
            var response = Request.CreateResponse(HttpStatusCode.OK, new List<Names>() { new Names() { Name="Shakti" },new Names { Name = "Amar" },new Names { Name = "Madhuri" } });
            return response;
        }
        [Route("api/values/{id:int}/{b:alpha}")]
        public IHttpActionResult Get(int id,string b)
        {
            //return "value";            
            //string uri = Url.Link("JackAss", new { id = id });
            //var response = Request.CreateResponse(HttpStatusCode.MovedPermanently);
            //response.Headers.Location = new Uri(uri);
            var response = Ok("Done")
                .Cache(5,CacheType.Private);
            return response;
        }
        public class Names
        {
            public string Name { get; set; }
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]string value)
        {
            var response = Request.CreateResponse(HttpStatusCode.Created,"Shakti Created");
            var uri = Url.Link("JackAss1", new { id = 9 });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT api/values/5
        public void Put([FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
