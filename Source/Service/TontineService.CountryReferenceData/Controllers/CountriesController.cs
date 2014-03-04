using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using TontineService.CountryReferenceData.Models;
using TontineService.CountryReferenceData.Repositories;

namespace TontineService.CountryReferenceData.Controllers
{
    public class CountriesController : ApiController
    {
        // GET api/countries
        public IEnumerable<Country> Get()
        {
            return CountryReferenceDataRepository.GetCountries();
        }

        // GET api/countries/Afghanistan
        public HttpResponseMessage Get(string countryName)
        {
            var country = CountryReferenceDataRepository.GetCountry(countryName);

            if (country != null)
            {
                if(Request.Headers.Accept.Contains(new MediaTypeWithQualityHeaderValue("image/png")))
                    return Request.CreateResponse(HttpStatusCode.OK, country, new MediaTypeHeaderValue("image/png"));

                return Request.CreateResponse(HttpStatusCode.OK, country);
            }
            
            return Request.CreateErrorResponse(HttpStatusCode.NotFound
                , string.Format("Country with name '{0}' Not Found.", countryName));
        }

        // POST api/countries
        public HttpResponseMessage Post([FromBody] Country country)
        {
            CountryReferenceDataRepository.AddCountry(country);

            var response = Request.CreateResponse(HttpStatusCode.Created, country);
            var uri = string.Format("{0}/{1}", Url.Link("DefaultApi", null), country.RowKey);
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT api/countries/Afghanistan
        public HttpResponseMessage Put(string countryName, [FromBody] Country country)
        {
            CountryReferenceDataRepository.UpdateCountry(country);

            return new HttpResponseMessage {StatusCode = HttpStatusCode.OK};
        }

        // DELETE api/countries/Afghanistan
        public void Delete(string countryName)
        {
            CountryReferenceDataRepository.DeleteCountry(countryName);
        }
    }
}