namespace TontineService.ReferenceData.Models
{
    public class Currency
    {
        public string CurrencyName { get; set; }

        public string CurrencyChar3Code { get; set; }

        public int CurrencyNumberCode { get; set; }

        public int? NumberOfDigits { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}"
                    , CurrencyName
                    , CurrencyChar3Code
                    , CurrencyNumberCode
                    , NumberOfDigits);
        }
    }
}