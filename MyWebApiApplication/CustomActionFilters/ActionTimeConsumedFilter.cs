using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Diagnostics;

namespace MyWebApiApplication.CustomActionFilters
{
    public class ActionTimeConsumedFilter:ActionFilterAttribute
    {
        private const string timeHeader = "X-Action-Time-Taken";
        Stopwatch timer = new Stopwatch();
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            timer = Stopwatch.StartNew();
            string actionName = actionContext.ActionDescriptor.ActionName;
            
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            //actionExecutedContext.Request.Properties.Add(timeHeader, timer.ElapsedMilliseconds);
            actionExecutedContext.Response.Headers.Add(timeHeader+actionExecutedContext.ActionContext.ActionDescriptor.ActionName, timer.ElapsedMilliseconds.ToString());
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }
}