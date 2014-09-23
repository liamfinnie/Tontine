using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;
using TontineService.CountryReferenceData.Models;

namespace TontineService.CountryReferenceData.Repositories
{
    //public sealed class CountryTableEntity : TableEntity
    //{
    //    public int NumberCode { get; set; }

    //    public string Alpha2Code { get; set; }

    //    public string Alpha3Code { get; set; }

    //    public string Capital { get; set; }

    //    public string CurrencyAlpha3Code { get; set; }

    //    public Byte[] Flag { get; set; }

    //    public CountryTableEntity(string region, string countryName)
    //    {
    //        PartitionKey = region;
    //        RowKey = countryName;
    //    }

    //    public CountryTableEntity()
    //    {

    //    }

    //    public Country MapCountry()
    //    {
    //        return new Country
    //        {
    //            Alpha2Code = Alpha2Code,
    //            Alpha3Code = Alpha3Code,
    //            Capital = Capital,
    //            Flag = Flag,
    //            CurrencyAlpha3Code = CurrencyAlpha3Code,
    //            NumberCode = NumberCode
    //        };
    //    }
    //}

    public sealed class AzureCountryReferenceDataRepository : ICountryReferenceDataRepository
    {
        private static CloudTable GetCountriesTable()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");

            return table;
        }

        public Country GetCountry(string countryName)
        {
            var table = GetCountriesTable();

            TableQuery<Country> query =
                new TableQuery<Country>().Where(TableQuery.GenerateFilterCondition("RowKey",
                    QueryComparisons.Equal, countryName.ToUpper()));

            var country = table.ExecuteQuery(query).FirstOrDefault();

            return country;
        }

        public IEnumerable<Country> GetCountries()
        {
            var table = GetCountriesTable();

            var query = new TableQuery<Country>();

            return table.ExecuteQuery(query).ToList();
        }

        public void AddCountry(Country country)
        {
            var table = GetCountriesTable();

            TableOperation insertOperation = TableOperation.InsertOrMerge(country);

            table.Execute(insertOperation);
        }

        public void UpdateCountry(Country country)
        {
            var table = GetCountriesTable();

            TableOperation insertOperation = TableOperation.InsertOrMerge(country);

            table.Execute(insertOperation);
        }

        public void DeleteCountry(string countryName)
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