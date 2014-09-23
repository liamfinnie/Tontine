using System.Collections.Generic;
using TontineService.CountryReferenceData.Models;

namespace TontineService.CountryReferenceData.Repositories
{
    public interface ICountryReferenceDataRepository
    {
        Country GetCountry(string countryName);

        IEnumerable<Country> GetCountries();

        void AddCountry(Country county);
        
        void UpdateCountry(Country county);
        
        void DeleteCountry(string countryName);
    }
}