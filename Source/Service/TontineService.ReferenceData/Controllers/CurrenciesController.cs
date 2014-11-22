using System;
using NLog;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TontineService.ReferenceData.Models;
using TontineService.ReferenceData.Repositories;

namespace TontineService.ReferenceData.Controllers
{
    public class CurrenciesController : ApiController
    {
        private readonly ICurrencyReferenceDataRepository _repository;
        private static readonly Logger _logger = LogManager.GetLogger("CurrencyRefDataService");

        public CurrenciesController(ICurrencyReferenceDataRepository repository)
        {
            _repository = repository;
        }

        [Route("api/currencies")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _repository.GetCurrencies());
        }

        [Route("api/currencies/{currencyCode:length(3)}", Name="GetCurrencyByCode")]
        public HttpResponseMessage Get(string currencyCode)
        {
            var currency = _repository.GetCurrency(currencyCode);

            if (currency != null)
                return Request.CreateResponse(HttpStatusCode.OK, currency);

            var notFoundMessage = string.Format("A currency with code '{0}' was not found.", currencyCode);
            _logger.Info(notFoundMessage);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, notFoundMessage);
        }

        [Route("api/currencies")]
        public HttpResponseMessage Post([FromBody] Currency currency)
        {
            _repository.AddCurrency(currency);

            var response = Request.CreateResponse(HttpStatusCode.Created, currency);
            var uri = Url.Link("GetCurrencyByCode", new {currencyCode = currency.CurrencyChar3Code});
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [Route("api/currencies/{currencyCode:length(3)}")]
        public HttpResponseMessage Put(string currencyCode, [FromBody] Currency currency)
        {
            _repository.UpdateCurrency(currency);

            return new HttpResponseMessage {StatusCode = HttpStatusCode.OK};
        }

        [Route("api/currencies/{currencyCode:length(3)}")]
        public void Delete(string currencyCode)
        {
            _repository.DeleteCurrency(currencyCode);
        }
    }
}
