using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TontineService.ReferenceData.Models;

namespace TontineService.ReferenceData.Repositories
{
    public class FlatFileCurrencyReferenceDataRepository : ICurrencyReferenceDataRepository
    {
        private readonly string _fileName;
        readonly List<Currency> _currencies = new List<Currency>();

        public FlatFileCurrencyReferenceDataRepository(string fileName)
        {
            _fileName = fileName;
            _currencies = new List<Currency>();

            string[] rawCurrencies = File.ReadAllLines(_fileName);

            foreach (var properties in rawCurrencies.Select(rawCurrency => rawCurrency.Split(',')))
            {
                var currency = new Currency
                {
                    CurrencyName = properties[0],
                    CurrencyChar3Code = properties[1],
                    CurrencyNumberCode = Int32.Parse(properties[2]),
                };

                int result;
                if(Int32.TryParse(properties[3], out result))
                    currency.NumberOfDigits = result; 
                
                _currencies.Add(currency);
            }
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            return _currencies;
        }

        public Currency GetCurrency(string currencyChar3Code)
        {
            return _currencies.Find(c => c.CurrencyChar3Code == currencyChar3Code.ToUpper());
        }

        public void AddCurrency(Currency currency)
        {
            _currencies.Add(currency);
            Persist();
        }

        public void UpdateCurrency(Currency currency)
        {
            var targetCurrency = _currencies.Find(c => c.CurrencyChar3Code == currency.CurrencyChar3Code.ToUpper());
            _currencies.Remove(targetCurrency);
            
            _currencies.Add(currency);
            Persist();
        }

        public void DeleteCurrency(string currencyChar3Code)
        {
            var targetCurrency = _currencies.Find(c => c.CurrencyChar3Code == currencyChar3Code.ToUpper());
            _currencies.Remove(targetCurrency);
            Persist();
        }

        private void Persist()
        {
            File.WriteAllLines(_fileName, _currencies.Select(c => c.ToString()).ToList());
        }

    }
}