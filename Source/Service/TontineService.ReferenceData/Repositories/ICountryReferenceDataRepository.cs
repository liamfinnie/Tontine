using System.Collections.Generic;
using TontineService.ReferenceData.Models;

namespace TontineService.ReferenceData.Repositories
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