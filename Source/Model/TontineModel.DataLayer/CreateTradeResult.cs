using System.Collections.Generic;

namespace TontineModel.DataLayer
{
    public sealed class CreateTradeResult
    {
        public List<string> Errors { get; set; }

        public CreateTradeResult()
        {
            Errors = new List<string>();
        }
    }
}
