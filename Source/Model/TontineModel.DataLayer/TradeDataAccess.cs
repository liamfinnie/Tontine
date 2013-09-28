using System.Linq;
using TontineModel.DomainClasses;

namespace TontineModel.DataLayer
{
    public sealed class TradeDataAccess : ITradeDataAccess
    {
        private readonly string _nameOrConnectionString;

        public TradeDataAccess(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public bool CreateTrade(string tradeReference, string tradeRepresentation, string sourceApplicationCode)
        {
            var context = new HermesContext(_nameOrConnectionString);
            context.StagingTrades.Add(new StagingTrade
            {
                TradeReference = tradeReference,
                TradeRepresentation = tradeRepresentation,
                SourceApplicationCode = sourceApplicationCode
            });

            context.SaveChanges();

            return true;
        }

        public bool IsDuplicateTrade(string tradeReference, string sourceApplicationCode)
        {
            var context = new HermesContext(_nameOrConnectionString);
            var trade = (from s in context.StagingTrades
                         where s.TradeReference == tradeReference
                         && s.SourceApplicationCode == sourceApplicationCode
                         select s).FirstOrDefault();

            return trade != null;
        }
    }
}
