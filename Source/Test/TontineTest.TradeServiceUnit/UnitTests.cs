using System.IO;
using System.Reflection;
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
        public void cannot_create_trade_with_empty_sourceApplicationId()
        {
            var service = new TradeService(MockRepository.GenerateMock<ITradeDataAccess>());
            const string tradeML = "<>";
            const int expectedNumberOfErrors = 1;
            const string expectedErrorMessage = "A Source Appplication Id must be provided.";

            CreateTradeResult createTradeResult = service.CreateTrade(tradeML, "");
            Assert.AreEqual(expectedNumberOfErrors, createTradeResult.Errors.Count);
            Assert.AreEqual(expectedErrorMessage, createTradeResult.Errors[0]);
        }

        [Test]
        public void cannot_create_trade_with_empty_tradeML()
        {
            var service = new TradeService(MockRepository.GenerateMock<ITradeDataAccess>());
            const string sourceApplicationId = "UnitTest";
            const int expectedNumberOfErrors = 1;
            const string expectedErrorMessage = "The trade representation cannot be empty.";

            CreateTradeResult createTradeResult = service.CreateTrade("", sourceApplicationId);
            Assert.AreEqual(expectedNumberOfErrors, createTradeResult.Errors.Count);
            Assert.AreEqual(expectedErrorMessage, createTradeResult.Errors[0]);
        }

        [Test]
        public void cannot_create_trade_with_invalid_xml()
        {
            const string sourceApplicationId = "UnitTest";
            const int expectedNumberOfErrors = 1;
            const string expectedErrorMessage = "Trade representation is invalid XML: The element 'tradeHeader' in namespace 'http://www.fpml.org/FpML-5/confirmation' has invalid child element 'tradeDate1' in namespace 'http://www.fpml.org/FpML-5/confirmation'. List of possible elements expected: 'partyTradeIdentifier, partyTradeInformation, tradeDate' in namespace 'http://www.fpml.org/FpML-5/confirmation'.";
            var tradeML = GetVanillaIRDSwap().Replace("tradeDate", "tradeDate1");
            var service = new TradeService(MockRepository.GenerateMock<ITradeDataAccess>());

            CreateTradeResult createTradeResult = service.CreateTrade(tradeML, sourceApplicationId);
            Assert.AreEqual(expectedNumberOfErrors, createTradeResult.Errors.Count);
            Assert.AreEqual(expectedErrorMessage, createTradeResult.Errors[0]);
        }

        [Test]
        public void cannot_create_trade_with_duplicate_trade_reference_and_source_application_id()
        {
            var mockTradeDataAccess = MockRepository.GenerateMock<ITradeDataAccess>();
            const string sourceApplicationId = "UnitTest";
            const int expectedNumberOfErrors = 1;
            const string expectedErrorMessage = "Trade with same trade reference and source application id already exists.";
            mockTradeDataAccess.Expect(x => x.IsDuplicateTrade("SW2000", sourceApplicationId)).Return(true);
            
            var service = new TradeService(mockTradeDataAccess);
            CreateTradeResult createTradeResult = service.CreateTrade(GetVanillaIRDSwap(), sourceApplicationId);

            Assert.AreEqual(expectedNumberOfErrors, createTradeResult.Errors.Count);
            Assert.AreEqual(expectedErrorMessage, createTradeResult.Errors[0]);
        }

        [Test]
        public void cannot_create_trade_with_empty_trade_id()
        {
            const string sourceApplicationId = "UnitTest";
            const int expectedNumberOfErrors = 1;
            const string expectedErrorMessage = "The Trade Id cannot be empty.";
            var tradeML = GetVanillaIRDSwap().Replace("SW2000", "");
            var service = new TradeService(MockRepository.GenerateMock<ITradeDataAccess>());

            CreateTradeResult createTradeResult = service.CreateTrade(tradeML, sourceApplicationId);
            Assert.AreEqual(expectedNumberOfErrors, createTradeResult.Errors.Count);
            Assert.AreEqual(expectedErrorMessage, createTradeResult.Errors[0]);
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