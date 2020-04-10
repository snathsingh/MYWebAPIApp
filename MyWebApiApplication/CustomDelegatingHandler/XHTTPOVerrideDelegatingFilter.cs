using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyWebApiApplication.CustomDelegatingHandler
{
    public class XHTTPOVerrideDelegatingFilter : DelegatingHandler
    {
        public const string XHTTPHeader = "X-HTTP-Method-Override";
        private IEnumerable<string> allowedHeaders = new List<string>()
        {
            "PUT","PATCH","DELETE","HEAD","VIEW"
        };
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var methodname = request.Method;
            if (methodname.Method == "POST")
            {
                if (request.Headers.TryGetValues(XHTTPHeader, out var header))
                {
                    string givenHeader = header.FirstOrDefault();
                    if (allowedHeaders.Contains(givenHeader))
                    {
                        request.Method = new HttpMethod(givenHeader);
                    }

                }
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}