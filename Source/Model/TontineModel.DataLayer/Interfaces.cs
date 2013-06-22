namespace TontineModel.DataLayer
{
    public interface ITradeDataAccess
    {
        bool CreateTrade(string tradeReference, string tradeRepresentation, string sourceApplicationId);
        bool IsDuplicateTrade(string tradeReference, string sourceApplicationCode);
    }
}
