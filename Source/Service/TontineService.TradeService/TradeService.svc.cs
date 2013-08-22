using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using TontineModel.DataLayer;

namespace TontineService.TradeService
{
    [DataContract]
    public class InvalidTradeSubmission
    {
        [DataMember]
        public string Message;
    }

    [TradeServiceErrorHandler]
    public class TradeService : ITradeService
    {
        private readonly ITradeDataAccess _tradeDataAccess;

        public TradeService()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<ITradeDataAccess>().ImplementedBy<TradeDataAccess>());
            _tradeDataAccess = container.Resolve<ITradeDataAccess>(new { nameOrConnectionString = "HermesContext" });
        }

        public TradeService(ITradeDataAccess tradeDataAccess)
        {
            _tradeDataAccess = tradeDataAccess;
        }
        
        public CreateTradeResult CreateTrade(string tradeRepresentation, string sourceApplicationCode)
        {
            var result = new CreateTradeResult();

            if (string.IsNullOrEmpty(sourceApplicationCode))
                throw new FaultException<InvalidTradeSubmission>(new InvalidTradeSubmission { Message = "A Source Appplication Code must be provided." }
                    , new FaultReason("A Source Appplication Code must be provided."));

            if (string.IsNullOrEmpty(tradeRepresentation))
                throw new FaultException<InvalidTradeSubmission>(new InvalidTradeSubmission { Message = "The trade representation cannot be empty." }
                    , new FaultReason("The trade representation cannot be empty."));

            string tradeReference = GetTradeReference(tradeRepresentation);
            if (string.IsNullOrEmpty(tradeReference))
                throw new FaultException<InvalidTradeSubmission>(new InvalidTradeSubmission { Message = "The Trade Id cannot be empty." }
                    , new FaultReason("The Trade Id cannot be empty."));

            if (_tradeDataAccess.IsDuplicateTrade(tradeReference, sourceApplicationCode))
                throw new FaultException<InvalidTradeSubmission>(new InvalidTradeSubmission { Message = "Trade with same trade reference and source application id already exists." }
                    , new FaultReason("Trade with same trade reference and source application id already exists."));

            string validateTradeRepresentationResult = ValidateTradeML(tradeRepresentation);
            if (!string.IsNullOrEmpty(validateTradeRepresentationResult))
                throw new FaultException<InvalidTradeSubmission>(new InvalidTradeSubmission { Message = "Trade representation is invalid XML: " + validateTradeRepresentationResult }
                    , new FaultReason("Trade representation is invalid XML: " + validateTradeRepresentationResult));

            _tradeDataAccess.CreateTrade(tradeReference, tradeRepresentation, sourceApplicationCode);

            return result;
        }   

        private static string GetTradeReference(string tradeRepresentation)
        {
            var document = XDocument.Parse(tradeRepresentation);
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("fpml-5", "http://www.fpml.org/FpML-5/confirmation");

            var tradeReference = document.XPathSelectElement("/fpml-5:dataDocument/fpml-5:trade/fpml-5:tradeHeader/fpml-5:partyTradeIdentifier/fpml-5:tradeId", namespaceManager);
            return tradeReference == null ? string.Empty : tradeReference.Value;
        }

        // Assumptions : The only trade type currently valid is vanilla ird swap
        /// <returns>A string with any raised error message. An empty string indicates validation was succesfull</returns>
        private static string ValidateTradeML(string tradeRepresentation)
        {
            var schema = new XmlSchemaSet();
            schema.Add(null, "Schemas/fpml-main-5-5.xsd");
            var error = string.Empty;

            var document = XDocument.Parse(tradeRepresentation);
            document.Validate(schema, (o, e) => error = e.Message, true);
      
            return error;
        }   
    }
}