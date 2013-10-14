using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            return table.ExecuteQuery(query).FirstOrDefault();
        }

        // POST api/countries
        public HttpResponseMessage Post([FromBody] Country country)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");

            TableOperation insertOperation = TableOperation.InsertOrMerge(country);

            table.Execute(insertOperation);

            var response = Request.CreateResponse(HttpStatusCode.Created, country);
            string uri = string.Format("{0}/{1}", Url.Link("DefaultApi", null), country.RowKey);
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT api/countries/Afghanistan
        public HttpResponseMessage Put(string countryName, [FromBody] Country country)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");

            TableOperation insertOperation = TableOperation.InsertOrMerge(country);

            table.Execute(insertOperation);

            return new HttpResponseMessage {StatusCode = HttpStatusCode.OK};
        }

        // DELETE api/countries/Afghanistan
        public void Delete(string countryName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");

            var country = Get(countryName);
            if (country != null)
            {
                country.ETag = "*";

                TableOperation deleteOperation = TableOperation.Delete(country);

                table.Execute(deleteOperation);
            }
        }
    }
}