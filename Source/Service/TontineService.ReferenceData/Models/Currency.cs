namespace TontineService.ReferenceData.Models
{
    public class Currency
    {
        public string CurrencyName { get; set; }

        public string CurrencyChar3Code { get; set; }

        public int CurrencyNumberCode { get; set; }

        public int? NumberOfDigits { get; set; }
    }
}