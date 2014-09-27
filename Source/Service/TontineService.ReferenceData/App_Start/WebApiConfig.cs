using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using System.Net.Http.Formatting;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
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
            RegisterExceptionHandling(config);

            config.MapHttpAttributeRoutes();

            RegisterDependencyResolver();

            RegisterFormatters(GlobalConfiguration.Configuration.Formatters);
        }

        public static void RegisterExceptionHandling(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IExceptionHandler), new GenericTextExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new GenericExceptionLogger());
        }

        public static void RegisterDependencyResolver()
        {
            var kernal = new StandardKernel();
            kernal.Bind<ICountryReferenceDataRepository>().To<AzureCountryReferenceDataRepository>();

            string currenciesFileName = HostingEnvironment.MapPath(@"~\Resources\iso_4217_currencies.csv");
            kernal.Bind<ICurrencyReferenceDataRepository>().To<FlatFileCurrencyReferenceDataRepository>()
                .WithConstructorArgument("fileName", currenciesFileName);

            var dependencyResolver = new NinjectResolver(kernal);
            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
        }

        public static void RegisterFormatters(MediaTypeFormatterCollection formatters)
        {
            var jsonFormatter = formatters.JsonFormatter;
            jsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            formatters.Add(new CountryImageFormatter());

            GlobalConfiguration.Configuration.AddJsonpFormatter();
        }

    }
}