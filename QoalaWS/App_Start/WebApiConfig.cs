﻿using QoalaWS.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace QoalaWS
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { controller="Default", action="Index", id = RouteParameter.Optional }
            );

            var json = config.Formatters.JsonFormatter;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //log all requests-For DEBUG

            config.Filters.Add(new LogRequests());

        }
    }
}
