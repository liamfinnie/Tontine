using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using Raven.Client;
using TontineService.ReferenceData.Models;

namespace TontineService.ReferenceData.Controllers
{
    public class MarketIdentifiersController : RavenDbController
    {
        private static readonly Logger _logger = LogManager.GetLogger("MarketIdentifiersRefDataService");

        [Route("api/marketIdentifiers")]
        public async Task<HttpResponseMessage> GetMarketIdentifiers()
        {
            var marketIdentifiers = await Session.Query<MarketIdentifier>().Take(5000).ToListAsync();

            return Request.CreateResponse(HttpStatusCode.OK, marketIdentifiers);
        }

        [Route("api/marketIdentifiers/{code:length(4)}", Name = "GetMarketIdentifierByCode")]
        public async Task<HttpResponseMessage> Get(string code)
        {
            var marketIdentifier = await (from identifier in Session.Query<MarketIdentifier>()
                where identifier.MIC == code
                select identifier).FirstOrDefaultAsync();

            if (marketIdentifier != null)
                return Request.CreateResponse(HttpStatusCode.OK, marketIdentifier);

            var notFoundMessage = string.Format("A market identifier with code '{0}' was not found.", code);
            _logger.Info(notFoundMessage);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, notFoundMessage);
        }

        [Route("api/marketIdentifiers")]
        public async Task<HttpResponseMessage> Post([FromBody] MarketIdentifier marketIdentifier)
        {
            await Session.StoreAsync(marketIdentifier);

            var response = Request.CreateResponse(HttpStatusCode.Created, marketIdentifier);
            var uri = Url.Link("GetMarketIdentifierByCode", new { code = marketIdentifier.MIC });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [Route("api/marketIdentifiers/{code:length(4)}")]
        public async Task<HttpResponseMessage> Put(string code, [FromBody] MarketIdentifier marketIdentifier)
        {
            var currentMarketIdentifier = await (from identifier in Session.Query<MarketIdentifier>()
                                          where identifier.MIC == code
                                          select identifier).FirstOrDefaultAsync();

            currentMarketIdentifier.ISOCountryCode = marketIdentifier.ISOCountryCode;
            currentMarketIdentifier.OperatingOrSegment = marketIdentifier.OperatingOrSegment;
            currentMarketIdentifier.Name = marketIdentifier.Name;
            currentMarketIdentifier.OperatingMIC = marketIdentifier.OperatingMIC;
            currentMarketIdentifier.Acronym = marketIdentifier.Acronym;
            currentMarketIdentifier.City = marketIdentifier.City;
            currentMarketIdentifier.Website = marketIdentifier.Website;
            currentMarketIdentifier.CreationDate = marketIdentifier.CreationDate;
            currentMarketIdentifier.StatusDate = marketIdentifier.StatusDate;
            currentMarketIdentifier.Status = marketIdentifier.Status;
            currentMarketIdentifier.Comments = marketIdentifier.Comments;

            await Session.StoreAsync(currentMarketIdentifier);

            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [Route("api/marketIdentifiers/{code:length(4)}")]
        public async Task<HttpResponseMessage> Delete(string code)
        {
            var marketIdentifier = await (from identifier in Session.Query<MarketIdentifier>()
                where identifier.MIC == code
                select identifier).FirstOrDefaultAsync();

            if (marketIdentifier != null)
                Session.Delete(marketIdentifier);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}