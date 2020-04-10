using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MyWebApiApplication.CustomActionFilters
{
    public enum CacheType
    {
        Public,
        Private,
        NoCache
    }
    public class ClientSideCachingAttribute:ActionFilterAttribute
    {
        private readonly int duration;
        private readonly CacheType cacheType;
        public int MyProperty { get; set; }

        public ClientSideCachingAttribute(int duration=60,CacheType typeOfCache=CacheType.NoCache)
        {
            this.duration = duration;
            cacheType = typeOfCache;
        }
        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (cacheType == CacheType.NoCache)
            {
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    NoStore = true
                };
                actionExecutedContext.Response.Headers.Date = DateTime.Now;
                actionExecutedContext.Response.Headers.Pragma.ParseAdd("No-Caching");
                actionExecutedContext.Response.Content.Headers.Expires = actionExecutedContext.Response.Headers.Date;
            }
            else
            {
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromSeconds(duration),
                    NoStore = false,
                    Private = (cacheType == CacheType.Private),
                    Public = (cacheType == CacheType.Public),
                    NoCache = (cacheType == CacheType.NoCache)
                };
                actionExecutedContext.Response.Headers.Date = DateTime.Now;
                actionExecutedContext.Response.Content.Headers.Expires = actionExecutedContext.Response.Headers.Date + TimeSpan.FromSeconds(duration);
            }
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
        
    }
}