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
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/countries/Spain
        public Country Get(string id)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");
            
            TableQuery<Country> query =
                new TableQuery<Country>().Where(TableQuery.GenerateFilterCondition("RowKey",
                    QueryComparisons.Equal, id));

            return table.ExecuteQuery(query).First();
        }

        // POST api/countries
        public void Post([FromBody]string value)
        {
        }

        // PUT api/countries/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/countries/5
        public void Delete(int id)
        {
        }
    }
}
