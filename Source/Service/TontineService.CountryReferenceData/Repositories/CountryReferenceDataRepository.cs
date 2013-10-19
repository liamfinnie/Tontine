using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TontineService.CountryReferenceData.Models;

namespace TontineService.CountryReferenceData.Repositories
{
    public sealed class CountryReferenceDataRepository
    {
        private static CloudTable GetCountriesTable()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");

            return table;
        }

        public static Country GetCountry(string countryName)
        {
            var table = GetCountriesTable();

            TableQuery<Country> query =
                new TableQuery<Country>().Where(TableQuery.GenerateFilterCondition("RowKey",
                    QueryComparisons.Equal, countryName.ToUpper()));

            var country = table.ExecuteQuery(query).FirstOrDefault();

            return country;
        }

        public static IEnumerable<Country> GetCountries()
        {
            var table = GetCountriesTable();

            var query = new TableQuery<Country>();

            return table.ExecuteQuery(query).ToList();
        }

        public static void AddCountry(Country country)
        {
            var table = GetCountriesTable();

            TableOperation insertOperation = TableOperation.InsertOrMerge(country);

            table.Execute(insertOperation);
        }

        public static void UpdateCountry(Country country)
        {
            var table = GetCountriesTable();

            TableOperation insertOperation = TableOperation.InsertOrMerge(country);

            table.Execute(insertOperation);
        }

        public static void DeleteCountry(string countryName)
        {
            var table = GetCountriesTable();

            Country country = GetCountry(countryName);
            if (country != null)
            {
                country.ETag = "*";

                TableOperation deleteOperation = TableOperation.Delete(country);

                table.Execute(deleteOperation);
            }
        }

    }
}