using System;
using System.Collections.Generic;

namespace ODataTrades.Models
{
    public partial class Country
    {
        public string country_numeric_code { get; set; }
        public string country_alpha2_code { get; set; }
        public string country_alpha3_code { get; set; }
        public string country_name { get; set; }
    }
}
