using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;

namespace MyWebApiApplication.CustomDelegatingHandler
{
    public class ApiKeyHandler : DelegatingHandler
    {
        public const string apiKey = "api_key";
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string apiKeyQuery = null;
            IEnumerable<string> str = null;
            request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value).TryGetValue(apiKey, out apiKeyQuery);
            request.Headers.TryGetValues(apiKey, out str);
            if(str!=null)
            apiKeyQuery = str.FirstOrDefault();
            request.Properties.Add(apiKey, apiKeyQuery);
            return await base.SendAsync(request, cancellationToken);
        }

    }

    public static class RequestExtension
    {
        public static string GetApiKey(this HttpRequestMessage request)
        {
            object val = null;
            request.Properties.TryGetValue(ApiKeyHandler.apiKey, out val);
            return val.ToString();
        }
    }
}