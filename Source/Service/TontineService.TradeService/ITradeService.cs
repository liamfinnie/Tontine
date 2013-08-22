using System.ServiceModel;
using TontineModel.DataLayer;

namespace TontineService.TradeService
{
    [ServiceContract]
    public interface ITradeService
    {
        [FaultContract(typeof(InvalidTradeSubmission))]
        [OperationContract]
        CreateTradeResult CreateTrade(string tradeRepresentation, string sourceApplicationCode);
    }
}