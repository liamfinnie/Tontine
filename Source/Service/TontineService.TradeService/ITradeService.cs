using System;
using System.ServiceModel;
using TontineModel.DataLayer;

namespace TontineService.TradeService
{
    [ServiceContract]
    public interface ITradeService
    {
        [FaultContract(typeof(InvalidTradeSubmission), Action="http://www.tontine.com/InvalidTradeSubmission")]
        [OperationContract]
        CreateTradeResult CreateTrade(string tradeRepresentation, string sourceApplicationCode);
    }
}