using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using TontineService.ReferenceData.ExceptionHandling;
using TontineService.ReferenceData.Formatters;
using TontineService.ReferenceData.Repositories;
using WebApiContrib.Formatting.Jsonp;
using WebApiContrib.IoC.Ninject;

namespace TontineService.ReferenceData
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IExceptionHandler), new GenericTextExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new GenericExceptionLogger());

            config.MapHttpAttributeRoutes();

            FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters);
            GlobalConfiguration.Configuration.AddJsonpFormatter();
         
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