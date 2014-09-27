using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TontineService.ReferenceData.Models;

namespace TontineService.ReferenceData.Controllers
{
    public class CurrenciesController : ApiController
    {
        [Route("api/currencies")]
        public IEnumerable<Currency> Get()
        {
            return new List<Currency>();
        }

        [Route("api/currencies/{currencyCode}")]
        public HttpResponseMessage Get(string currencyCode)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Currency { CurrencyName = "Pound Sterling", CurrencyCode = currencyCode, CurrencyCodeNumber = "826" });
        }

        // POST: api/Currencies
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Currencies/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Currencies/5
        public void Delete(int id)
        {
        }
    }
}
