using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.SelfHost;
using Microsoft.Data.Edm;
using ODataTrades.Models;

namespace ODataTrades
{
    class Program
    {
        static void Main()
        {
            var config = new HttpSelfHostConfiguration("http://localhost:50232");
            config.Routes.MapODataRoute("default", "odata", GetServiceModel());


            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            Console.WriteLine("OData Trade service started...");
            Console.ReadKey();
        }

        private static IEdmModel GetServiceModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Trade>("trades");
            builder.EntitySet<Currency>("currencies");
            builder.EntitySet<Country>("countries");
            return builder.GetEdmModel();
        }
    }

    public class TradesController : ODataController
    {
        private readonly HermesContext _db = new HermesContext();
        
        [Queryable(PageSize = 1, EnsureStableOrdering = false)]
        public IQueryable<Trade> GetTrades()
        {
            return _db.trades;
        }
    }

    public class CurrenciesController : ODataController
    {
        private readonly HermesContext _db = new HermesContext();

        [Queryable(PageSize = 8)]
        public IQueryable<Currency> GetCurrencies()
        {
            return _db.currencies;
        }

        public Currency GetCurrency([FromODataUri]string key)
        {
            return _db.currencies.Find(key);
        }
    }

    public class CountriesController : EntitySetController<Country, string>
    {
        private readonly HermesContext _db = new HermesContext();

        public override IQueryable<Country> Get()
        {
            return _db.countries;
        }

        protected override Country GetEntityByKey(string key)
        {
            return _db.countries.Find(key);
        }

        protected override string GetKey(Country entity)
        {
            return entity.country_numeric_code;
        }
    }
}
