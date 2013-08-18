using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using TontineModel.DataLayer;

namespace TontineService.TradeService
{
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
            {
                result.Errors.Add("A Source Appplication Code must be provided.");
                // Fail fast. It may be better to determine all of the possible errors with the given parameters but for 
                // now we will return to the client as soon as we know the trade will not be created.
                return result;
            }

            if (string.IsNullOrEmpty(tradeRepresentation))
            {
                result.Errors.Add("The trade representation cannot be empty.");
                return result;
            }

            string tradeReference = GetTradeReference(tradeRepresentation);
            if (string.IsNullOrEmpty(tradeReference))
            {
                result.Errors.Add("The Trade Id cannot be empty.");
                return result;
            }

            if (_tradeDataAccess.IsDuplicateTrade(tradeReference, sourceApplicationCode))
            {
                result.Errors.Add("Trade with same trade reference and source application id already exists.");
                return result;
            }

            string validateTradeRepresentationResult = ValidateTradeML(tradeRepresentation);
            if (!string.IsNullOrEmpty(validateTradeRepresentationResult))
            {
                result.Errors.Add("Trade representation is invalid XML: " + validateTradeRepresentationResult);
                return result;
            }

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