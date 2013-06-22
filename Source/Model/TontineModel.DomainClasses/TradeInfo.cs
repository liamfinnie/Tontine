using System;
using System.Runtime.Serialization;

namespace TontineModel.DomainClasses
{
    [DataContract]
    public class TradeInfo
    {
        string _tradeReference = string.Empty;
        DateTime _lastupdated = DateTime.MinValue;
        string _clientApplicationCode = string.Empty;
        private string _tradeMarkup = string.Empty;

        [DataMember]
        public int TradeId { get; set; }

        [DataMember]
        public string TradeReference
        {
            get { return _tradeReference; }
            set { _tradeReference = value; }
        }

        [DataMember]
        public string ClientApplicationCode
        {
            get { return _clientApplicationCode; }
            set { _clientApplicationCode = value; }
        }
        
        [DataMember]
        public DateTime LastUpdated
        {
            get { return _lastupdated; }
            set { _lastupdated = value; }
        }

        [DataMember]
        public string TradeMarkup
        {
            get { return _tradeMarkup; }
            set { _tradeMarkup = value; }
        }
    }
}