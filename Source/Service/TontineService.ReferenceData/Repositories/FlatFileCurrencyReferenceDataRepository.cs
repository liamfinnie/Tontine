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

        public Currency GetCurrency(string currencyCode)
        {
            return _currencies.Find(curr => curr.CurrencyChar3Code == currencyCode.ToUpper());
        }

        public void AddCurrency(Currency currency)
        {
            File.AppendAllText(_fileName,
               string.Format("{4}{0},{1},{2},{3}",
                    currency.CurrencyName
                    , currency.CurrencyChar3Code
                    , currency.CurrencyNumberCode
                    , currency.NumberOfDigits
                    , Environment.NewLine)
                );

            _currencies.Add(currency);
        }

        public void UpdateCurrency(Currency currency)
        {
            
        }

        public void DeleteCurrency(string currencyCode)
        {
            
        }
    }
}