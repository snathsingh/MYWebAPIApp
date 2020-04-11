using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace MyWebApiApplication.CustomActionFilters
{    
    public class CacheActionResult : IHttpActionResult 
    {
        private readonly IHttpActionResult Inner;
        private static int Duration;
        private readonly CacheType tCache;
        public CacheActionResult(IHttpActionResult inner,int duration,CacheType cacheType=CacheType.NoCache)
        {
            Inner = inner;
            Duration = duration;
            tCache = cacheType;
        }       
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {            
            var response=await Inner.ExecuteAsync(cancellationToken);
            if (tCache == CacheType.NoCache)
            {
                Duration = 0;
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    NoStore = true
                };
                response.Headers.Date = DateTime.Now;
                response.Content.Headers.Expires = response.Headers.Date;

                response.Headers.Pragma.TryParseAdd("No-Cache-Boy");
            }
            else
            {
                response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = false,
                    NoStore = false,
                    Private = (CacheType.Private == tCache),
                    Public = (CacheType.Public == tCache),
                    MaxAge = TimeSpan.FromSeconds(Duration)
                };
                response.Headers.Date = DateTime.Now;
                Thread.Sleep(2000);
                response.Content.Headers.Expires = response.Headers.Date+TimeSpan.FromSeconds(Duration+30);
            }
            return response;
        }
    }
    public static class CacheExtension
    {
        public static IHttpActionResult Cache(this IHttpActionResult httpActionResult,int seconds,CacheType cacheType=CacheType.NoCache)
        {
            return new CacheActionResult(httpActionResult, seconds,cacheType);
        }
    }
}