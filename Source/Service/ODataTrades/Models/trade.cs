using System;
using System.Collections.Generic;

namespace ODataTrades.Models
{
    public partial class Trade
    {
        public int trade_id { get; set; }
        public string trade_reference { get; set; }
        public System.DateTime last_updated { get; set; }
        public string client_application_code { get; set; }
        public string trade_markup { get; set; }
    }
}
