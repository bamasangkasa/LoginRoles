﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Company
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));



            config.Routes.MapHttpRoute("customRoute", "myservice/getAllCities", new { controller = "Service", action = "GetCities" }
           );

            config.Routes.MapHttpRoute("GetCityById", "myservice/getCityById/{id}", new { controller = "Service", action = "GetCity", id = RouteParameter.Optional }
 );

            //, City = UrlParameter.Optional     
                /*MapHttpRoute(
                name:"customRoute",
                routeTemplate: "myservice/getAllCities"
                );*/

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
}