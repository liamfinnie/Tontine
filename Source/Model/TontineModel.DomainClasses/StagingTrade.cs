using System;

namespace TontineModel.DomainClasses
{
    public sealed class StagingTrade
    {
        public long StagingTradeId { get; set; }
        public string TradeReference { get; set; }
        public string TradeRepresentation { get; set; }
        public string SourceApplicationCode { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
