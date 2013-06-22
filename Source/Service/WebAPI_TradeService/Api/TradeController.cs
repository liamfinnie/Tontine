using System.Collections.Generic;
using System.Web.Http;
using Castle.Windsor;
using TontineModel.DataLayer;
using TontineModel.DomainClasses;
using Component = Castle.MicroKernel.Registration.Component;

namespace WebAPI_TradeService.Api
{
    public class TradeController : ApiController
    {
        private readonly ITradeDataAccess _tradeDataAccess;

        public TradeController()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<ITradeDataAccess>().ImplementedBy<TradeDataAccess>());
            _tradeDataAccess = container.Resolve<ITradeDataAccess>();
        }

        // GET api/trade
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/trade/5
        public TradeInfo Get(string id)
        {
            return new TradeInfo();
        }

        // POST api/trade
        public void Post([FromBody]string value)
        {
        }

        // PUT api/trade/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/trade/5
        public void Delete(int id)
        {
        }
    }
}
