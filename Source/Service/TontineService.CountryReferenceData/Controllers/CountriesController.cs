using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TontineService.CountryReferenceData.Models;

namespace TontineService.CountryReferenceData.Controllers
{
    public class CountriesController : ApiController
    {
        // GET api/countries
        public IEnumerable<Country> Get()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");

            var query = new TableQuery<Country>();
            
            return table.ExecuteQuery(query).ToList();
        }

        // GET api/countries/Afghanistan
        public Country Get(string countryName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");
            
            TableQuery<Country> query =
                new TableQuery<Country>().Where(TableQuery.GenerateFilterCondition("RowKey",
                    QueryComparisons.Equal, countryName));

            return table.ExecuteQuery(query).First();
        }

        // POST api/countries
        public void Post([FromBody]string value)
        {
        }

        // PUT api/countries/Afghanistan
        public void Put(string countryName, [FromBody]string value)
        {
        }

        // DELETE api/countries/Afghanistan
        public void Delete(string countryName)
        {
        }
    }
}
