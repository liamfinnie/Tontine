using System.Collections.Generic;
using TontineService.ReferenceData.Models;

namespace TontineService.ReferenceData.Repositories
{
    public interface ICurrencyReferenceDataRepository
    {
        IEnumerable<Currency> GetCurrencies();
        
        Currency GetCurrency(string CurrencyChar3Code);
        
        void AddCurrency(Currency currency);
        
        void UpdateCurrency(Currency currency);
        
        void DeleteCurrency(string currencyCode);
    }
}