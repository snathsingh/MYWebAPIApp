using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace MyWebApiApplication.CustomConstraints
{
    public class Divisibleby10Constraint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (values.TryGetValue(parameterName, out object value) && value != null)
            {
                if (Convert.ToInt32(value) % 10 == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}