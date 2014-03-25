using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TontineService.CountryReferenceData.ExceptionHandling;
using TontineService.CountryReferenceData.Formatters;
using WebApiContrib.Formatting.Jsonp;

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