using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyWebApiApplication.CustomDelegatingHandler
{
    public class TimerHandler:DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string timerHeader = "X-Timer-Shakti-msec";
            var stopwatch = Stopwatch.StartNew();
            var response=await base.SendAsync(request, cancellationToken);
            string timeElapsed = stopwatch.ElapsedMilliseconds.ToString();
            response.Headers.Add(timerHeader, timeElapsed);
            return response;
        }
    }
}