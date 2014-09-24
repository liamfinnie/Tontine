using NLog;
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
        private readonly ICountryReferenceDataRepository _repository;
        private static readonly Logger Logger = LogManager.GetLogger("CountryRefDataService");

        public CountriesController(ICountryReferenceDataRepository repository)
        {
            _repository = repository;
        }

        // GET api/countries
        public IEnumerable<Country> Get()
        {
            return _repository.GetCountries();
        }

        // GET api/countries/Afghanistan
        public HttpResponseMessage Get(string countryName)
        {
            var country = _repository.GetCountry(countryName);

            if (country != null)
            {
                if(Request.Headers.Accept.Contains(new MediaTypeWithQualityHeaderValue("image/png")))
                    return Request.CreateResponse(HttpStatusCode.OK, country, new MediaTypeHeaderValue("image/png"));

                return Request.CreateResponse(HttpStatusCode.OK, country);
            }

            var notFoundMessage = string.Format("A country with name '{0}' was not found.", countryName);
            Logger.Info(notFoundMessage);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound , notFoundMessage);
        }

        // POST api/countries
        public HttpResponseMessage Post([FromBody] Country country)
        {
            _repository.AddCountry(country);

            var response = Request.CreateResponse(HttpStatusCode.Created, country);
            var uri = string.Format("{0}/{1}", Url.Link("DefaultApi", null), country.CountryName);
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT api/countries/Afghanistan
        public HttpResponseMessage Put(string countryName, [FromBody] Country country)
        {
            _repository.UpdateCountry(country);

            return new HttpResponseMessage {StatusCode = HttpStatusCode.OK};
        }

        // DELETE api/countries/Afghanistan
        public void Delete(string countryName)
        {
            _repository.DeleteCountry(countryName);
        }
    }
}