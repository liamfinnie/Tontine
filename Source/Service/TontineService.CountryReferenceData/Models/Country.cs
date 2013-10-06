using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace TontineService.CountryReferenceData.Models
{
    public class Country : TableEntity
    {
        public int NumberCode { get; set; }

        public string Alpha2Code { get; set; }

        public string Alpha3Code { get; set; }

        public string Capital { get; set; }

        public string CurrencyAlpha3Code { get; set; }

        public Byte[] Flag { get; set; }

        public Country(string region, string countryName)
        {
            PartitionKey = region;
            RowKey = countryName;
        }

        public Country()
        {

        }
    }
}