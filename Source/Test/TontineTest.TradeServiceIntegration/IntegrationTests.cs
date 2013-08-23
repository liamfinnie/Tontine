using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using NUnit.Framework;
using TontineModel.DataLayer;
using TontineService.TradeService;

namespace TontineTest.TradeServiceIntegration
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void can_create_new_trade_in_database()
        {
            const string sourceApplicationId = "IntegrationTest";
            string tradeML = GetVanillaIRDSwap();
            tradeML = tradeML.Replace("SW2000", Guid.NewGuid().ToString());
            var service = new TradeService(new TradeDataAccess("HermesContext"));

            var createTradeResult = service.CreateTrade(tradeML, sourceApplicationId);
            Assert.IsTrue(createTradeResult.TradeCreated);
        }

        [Test]
        [ExpectedException(typeof(FaultException<InvalidTradeSubmission>), ExpectedMessage = "Trade with same trade reference and source application id already exists.")]
        public void cannot_create_trade_with_duplicate_trade_reference_and_source_application_id()
        {
            const string sourceApplicationId = "IntegrationTest";
            string tradeML = GetVanillaIRDSwap();
            tradeML = tradeML.Replace("SW2000", Guid.NewGuid().ToString());
            var service = new TradeService(new TradeDataAccess("HermesContext"));

            var createTradeResult = service.CreateTrade(tradeML, sourceApplicationId);
            Assert.IsTrue(createTradeResult.TradeCreated);

            service.CreateTrade(tradeML, sourceApplicationId);
        } 

        [TearDown]
        public void DeleteIntegrationTestsFromDatabase()
        {
            var context = new HermesContext("HermesContext");
            context.Database.ExecuteSqlCommand("DELETE from staging_trade WHERE source_application_id = 'IntegrationTest'");
        }

        private static string GetVanillaIRDSwap()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            var vanillaIRDSwap = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("TontineTest.TradeServiceIntegration.Resources.vanilla_ird_swap.xml")).ReadToEnd();
            // ReSharper restore AssignNullToNotNullAttribute
            return vanillaIRDSwap;
        }

        [Ignore]
        [Test]
        public void create_staging_table()
        {
            using (var context = new HermesContext("HermesContext"))
            {
                var stagingTrades = context.StagingTrades.ToList();
                Assert.IsEmpty(stagingTrades);
            }
        } 
    }
}
