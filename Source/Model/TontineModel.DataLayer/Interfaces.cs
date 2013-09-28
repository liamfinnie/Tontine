namespace TontineModel.DataLayer
{
    public interface ITradeDataAccess
    {
        bool CreateTrade(string tradeReference, string tradeRepresentation, string sourceApplicationCode);
        bool IsDuplicateTrade(string tradeReference, string sourceApplicationCode);
    }
}
