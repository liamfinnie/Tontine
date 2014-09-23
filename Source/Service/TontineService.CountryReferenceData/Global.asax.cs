using System.Web.Http;
using Ninject;
using TontineService.CountryReferenceData.Repositories;
using WebApiContrib.IoC.Ninject;

namespace TontineService.CountryReferenceData
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            var kernal = new StandardKernel();
            kernal.Bind<ICountryReferenceDataRepository>().To<AzureCountryReferenceDataRepository>();
            var dependencyResolver = new NinjectResolver(kernal);
            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
        }
    }
}
