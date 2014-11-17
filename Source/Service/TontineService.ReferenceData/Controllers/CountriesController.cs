using NLog;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using TontineService.ReferenceData.Models;
using TontineService.ReferenceData.Repositories;

namespace TontineService.ReferenceData.Controllers
{
    public class CountriesController : ApiController
    {
        private readonly ICountryReferenceDataRepository _repository;
        private static readonly Logger _logger = LogManager.GetLogger("CountryRefDataService");

        public CountriesController(ICountryReferenceDataRepository repository)
        {
            _repository = repository;
        }

        [Route("api/countries")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repository.GetCountries());
        }

        [Route("api/countries/{countryName}")]
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
            _logger.Info(notFoundMessage);
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