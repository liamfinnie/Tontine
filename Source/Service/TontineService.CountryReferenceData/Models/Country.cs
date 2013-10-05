using Microsoft.WindowsAzure.Storage.Table;

namespace TontineService.CountryReferenceData.Models
{
    public class Country : TableEntity
    {
        public string CapitalCity { get; set; }
        public string Code { get; set; }

        public Country(string continent, string countryName)
        {
            PartitionKey = continent;
            RowKey = countryName;
        }

        public Country()
        {

        }
    }
}