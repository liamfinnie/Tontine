using System;

namespace TontineService.ReferenceData.Models
{
    public class Country
    {
        public string CountryName { get; set; }

        public string Region { get; set; }

        public int NumberCode { get; set; }

        public string Alpha2Code { get; set; }

        public string Alpha3Code { get; set; }

        public string Capital { get; set; }

        public string CurrencyAlpha3Code { get; set; }

        public Byte[] Flag { get; set; }
    }
}