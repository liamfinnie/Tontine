using System.IO;
using System.Reflection;
using System.ServiceModel;
using NUnit.Framework;
using Rhino.Mocks;
using TontineModel.DataLayer;
using TontineService.TradeService;

namespace TontineTest.TradeServiceUnit
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void can_create_trade()
        {
            var mockTradeDataAccess = MockRepository.GenerateMock<ITradeDataAccess>();
            var tradeML = GetVanillaIRDSwap();
            const string sourceApplicationId = "UnitTest";
            const int expectedNumberOfErrors = 0;

            mockTradeDataAccess.Expect(x => x.CreateTrade("SW2000", tradeML, sourceApplicationId)).Return(true);
            var service = new TradeService(mockTradeDataAccess);

            var createTradeResult = service.CreateTrade(tradeML, sourceApplicationId);
            Assert.AreEqual(expectedNumberOfErrors, createTradeResult.Errors.Count);
        }

        [Test]
        [ExpectedException(typeof(FaultException<InvalidTradeSubmission>), ExpectedMessage = "A Source Appplication Code must be provided.")]
        public void cannot_create_trade_with_empty_sourceApplicationId()
        {
            var service = new TradeService(MockRepository.GenerateMock<ITradeDataAccess>());
            const string tradeML = "<>";
            service.CreateTrade(tradeML, "");
        }

        [Test]
        [ExpectedException(typeof(FaultException<InvalidTradeSubmission>), ExpectedMessage = "The trade representation cannot be empty.")]
        public void cannot_create_trade_with_empty_tradeML()
        {
            var service = new TradeService(MockRepository.GenerateMock<ITradeDataAccess>());
            const string sourceApplicationId = "UnitTest";

            service.CreateTrade("", sourceApplicationId);
        }

        [Test]
        [ExpectedException(typeof(FaultException<InvalidTradeSubmission>), ExpectedMessage = "Trade representation is invalid XML: The element 'tradeHeader' in namespace 'http://www.fpml.org/FpML-5/confirmation' has invalid child element 'tradeDate1' in namespace 'http://www.fpml.org/FpML-5/confirmation'. List of possible elements expected: 'partyTradeIdentifier, partyTradeInformation, tradeDate' in namespace 'http://www.fpml.org/FpML-5/confirmation'.")]
        public void cannot_create_trade_with_invalid_xml()
        {
            const string sourceApplicationId = "UnitTest";
            var tradeML = GetVanillaIRDSwap().Replace("tradeDate", "tradeDate1");
            var service = new TradeService(MockRepository.GenerateMock<ITradeDataAccess>());

            service.CreateTrade(tradeML, sourceApplicationId);
        }

        [Test]
        [ExpectedException(typeof(FaultException<InvalidTradeSubmission>), ExpectedMessage = "Trade with same trade reference and source application id already exists.")]
        public void cannot_create_trade_with_duplicate_trade_reference_and_source_application_id()
        {
            var mockTradeDataAccess = MockRepository.GenerateMock<ITradeDataAccess>();
            const string sourceApplicationId = "UnitTest";
            mockTradeDataAccess.Expect(x => x.IsDuplicateTrade("SW2000", sourceApplicationId)).Return(true);
            
            var service = new TradeService(mockTradeDataAccess);
            service.CreateTrade(GetVanillaIRDSwap(), sourceApplicationId);
        }

        [Test]
        [ExpectedException(typeof(FaultException<InvalidTradeSubmission>), ExpectedMessage = "The Trade Id cannot be empty.")]
        public void cannot_create_trade_with_empty_trade_id()
        {
            const string sourceApplicationId = "UnitTest";
            var tradeML = GetVanillaIRDSwap().Replace("SW2000", "");
            var service = new TradeService(MockRepository.GenerateMock<ITradeDataAccess>());

            service.CreateTrade(tradeML, sourceApplicationId);
        }

        private static string GetVanillaIRDSwap()
        {
// ReSharper disable AssignNullToNotNullAttribute
            var vanillaIRDSwap = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("TontineTest.TradeServiceUnit.Resources.vanilla_ird_swap.xml")).ReadToEnd();
// ReSharper restore AssignNullToNotNullAttribute
            return vanillaIRDSwap;
        }
    }
}