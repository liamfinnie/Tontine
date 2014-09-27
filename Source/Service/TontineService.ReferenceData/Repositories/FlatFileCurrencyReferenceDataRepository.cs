using System.Collections.Generic;
using System.IO;
using System.Linq;
using TontineService.ReferenceData.Models;

namespace TontineService.ReferenceData.Repositories
{
    public class FlatFileCurrencyReferenceDataRepository : ICurrencyReferenceDataRepository
    {
        readonly List<Currency> _currencies = new List<Currency>();

        public FlatFileCurrencyReferenceDataRepository(string fileName)
        {
            _currencies = new List<Currency>();

            string[] rawCurrencies = File.ReadAllLines(fileName);

            foreach (var properties in rawCurrencies.Select(rawCurrency => rawCurrency.Split(',')))
            {
                _currencies.Add(new Currency
                {
                    CurrencyName = properties[0],
                    CurrencyChar3Code = properties[1],
                    CurrencyNumberCode = properties[2],
                    NumberOfDigits = properties[3]
                });
            }
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            return _currencies;
        }

        public Currency GetCurrency(string currencyCode)
        {
            return _currencies.Find(curr => curr.CurrencyChar3Code == currencyCode.ToUpper());
        }

        public void AddCurrency(Currency currency)
        {
            
        }

        public void UpdateCurrency(Currency currency)
        {
            
        }

        public void DeleteCurrency(string currencyCode)
        {
            
        }
    }
}