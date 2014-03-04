using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TontineService.CountryReferenceData.Filters;
using TontineService.CountryReferenceData.Formatters;
using WebApiContrib.Formatting.Jsonp;

namespace TontineService.CountryReferenceData
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new UnhandledExceptionFilterAttribute());

            FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters);

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{countryName}"
                , new { countryName = RouteParameter.Optional }
            );
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

                // Insert the JSONP formatter in front of the standard JSON formatter.
                var jsonpFormatter = new JsonpMediaTypeFormatter(formatters.JsonFormatter);
                formatters.Insert(0, jsonpFormatter);

                formatters.Add(new CountryImageFormatter());
            }
        }

    }
}