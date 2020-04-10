using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyWebApiApplication.CustomDelegatingHandler
{
    public class LoadBalancerHandler : DelegatingHandler
    {
        public const string XForwardedFor = "X-Forwarded-For";
        public const string XForwardedHost = "X-Forwarded-Host";
        public const string XForwardedProto = "X-Forwarded-Proto";
        public const string Forwarded = "Forwarded";
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains(Forwarded))
            {
                IEnumerable<string> headers = new List<string>();
                Dictionary<string, string> dict = new Dictionary<string, string>();
                request.Headers.TryGetValues(Forwarded, out headers);
                string[] values = headers.FirstOrDefault().Split(';');
                foreach (var item in values)
                {
                    string[] innerString = item.Trim().ToLower().Split('=');
                    dict.Add(innerString[0], innerString[1]);
                }
                UriBuilder uri = new UriBuilder(request.RequestUri)
                {
                    Host = dict["host"],
                    Port = 80,
                    Scheme = dict["proto"]
                };
                request.RequestUri = uri.Uri;
            }
            else if (request.Headers.Contains(XForwardedFor) && request.Headers.Contains(XForwardedHost) && request
                .Headers.Contains(XForwardedProto))
            {
                UriBuilder uri = new UriBuilder(request.RequestUri)
                {
                    Host = request.Headers.GetValues(XForwardedHost).First(),
                    Port = 80,
                    Scheme = request.Headers.GetValues(XForwardedProto).First()
                };
                request.RequestUri = uri.Uri;
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}