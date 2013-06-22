using System.ServiceModel;
using TontineModel.DataLayer;

namespace TontineService.TradeService
{
    [ServiceContract]
    public interface ITradeService
    {
        [OperationContract]
        CreateTradeResult CreateTrade(string tradeRepresentation, string sourceApplicationCode);
    }
}