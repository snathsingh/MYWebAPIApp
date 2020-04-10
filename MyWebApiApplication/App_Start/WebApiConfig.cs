using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using MyWebApiApplication.CustomActionFilters;
using MyWebApiApplication.CustomConstraints;
using MyWebApiApplication.CustomDelegatingHandler;

namespace MyWebApiApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.MessageHandlers.Add(new TimerHandler());
            config.MessageHandlers.Add(new ApiKeyHandler());
            config.Filters.Add(new CheckApiKey());
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("Divisibleby10", typeof(Divisibleby10Constraint));
            // Web API routes
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
