using AutoMapper;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using TontineService.CountryReferenceData.Models;

namespace TontineService.CountryReferenceData.Repositories
{
    public sealed class CountryTableEntity : TableEntity
    {
        public int NumberCode { get; set; }

        public string Alpha2Code { get; set; }

        public string Alpha3Code { get; set; }

        public string Capital { get; set; }

        public string CurrencyAlpha3Code { get; set; }

        public Byte[] Flag { get; set; }
    }

    public sealed class AzureCountryReferenceDataRepository : ICountryReferenceDataRepository
    {
        public AzureCountryReferenceDataRepository()
        {
            Mapper.CreateMap<CountryTableEntity, Country>()
                .ForMember(target => target.CountryName, map => map.MapFrom(source => source.RowKey))
                .ForMember(target => target.Region, map => map.MapFrom(source => source.PartitionKey));

            Mapper.CreateMap<Country, CountryTableEntity>()
                .ForMember(target => target.RowKey, map => map.MapFrom(source => source.CountryName))
                .ForMember(target => target.PartitionKey, map => map.MapFrom(source => source.Region));
        }

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

            TableQuery<CountryTableEntity> query =
                new TableQuery<CountryTableEntity>().Where(TableQuery.GenerateFilterCondition("RowKey",
                    QueryComparisons.Equal, countryName.ToUpper()));

            var countryTableEntity = table.ExecuteQuery(query).FirstOrDefault();

            return countryTableEntity == null ? null : Mapper.Map<Country>(countryTableEntity);
        }

        public IEnumerable<Country> GetCountries()
        {
            var table = GetCountriesTable();

            var query = new TableQuery<CountryTableEntity>();

            var countryTableEntities = table.ExecuteQuery(query).ToList();

            return countryTableEntities.Select(Mapper.Map<Country>).ToList();
        }

        public void AddCountry(Country country)
        {
            var table = GetCountriesTable();

            var entity = Mapper.Map<CountryTableEntity>(country);

            TableOperation insertOperation = TableOperation.InsertOrMerge(entity);

            table.Execute(insertOperation);
        }

        public void UpdateCountry(Country country)
        {
            var table = GetCountriesTable();

            TableOperation insertOperation = TableOperation.InsertOrMerge(Mapper.Map<CountryTableEntity>(country));

            table.Execute(insertOperation);
        }

        public void DeleteCountry(string countryName)
        {
            var table = GetCountriesTable();

            var country = Mapper.Map<CountryTableEntity>(GetCountry(countryName));
            country.ETag = "*";

            TableOperation deleteOperation = TableOperation.Delete(country);

            table.Execute(deleteOperation);
        }

    }
}