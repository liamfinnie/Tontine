﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using TontineService.CountryReferenceData.ExceptionHandling;
using TontineService.CountryReferenceData.Formatters;
using TontineService.CountryReferenceData.Repositories;
using WebApiContrib.Formatting.Jsonp;
using WebApiContrib.IoC.Ninject;

namespace TontineService.CountryReferenceData
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IExceptionHandler), new GenericTextExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new GenericExceptionLogger());

            FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters);
            GlobalConfiguration.Configuration.AddJsonpFormatter();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{countryName}"
                , new { countryName = RouteParameter.Optional });

            var kernal = new StandardKernel();
            kernal.Bind<ICountryReferenceDataRepository>().To<AzureCountryReferenceDataRepository>();
            var dependencyResolver = new NinjectResolver(kernal);
            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
        }

        public class FormatterConfig
        {
            public static void RegisterFormatters(MediaTypeFormatterCollection formatters)
            {
                var jsonFormatter = formatters.JsonFormatter;
                jsonFormatter.SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                formatters.Add(new CountryImageFormatter());
            }
        }

    }
}