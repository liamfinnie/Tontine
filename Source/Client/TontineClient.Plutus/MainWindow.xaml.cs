using System;
using System.ServiceModel;
using System.Windows;
using TontineClient.Plutus.TradeService;

namespace TontineClient.Plutus
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            TxtBoxTradeRepresentation.Text = @"<?xml version=""1.0"" encoding=""utf-16""?>
<dataDocument xmlns=""http://www.fpml.org/FpML-5/confirmation"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" fpmlVersion=""5-5"" xsi:schemaLocation=""http://www.fpml.org/FpML-5/confirmation ../../fpml-main-5-5.xsd http://www.w3.org/2000/09/xmldsig# ../../xmldsig-core-schema.xsd"">
  <trade>
    <tradeHeader>
      <partyTradeIdentifier>
        <partyReference href=""party2"" />
        <tradeId tradeIdScheme=""http://www.barclays.com/swaps/trade-id"">SW2000</tradeId>
      </partyTradeIdentifier>
      <tradeDate>1994-12-12</tradeDate>
    </tradeHeader>
    <swap>
      <!-- Party A pays the floating rate every 6 months, based on 6M EUR-LIBOR-BBA, on an ACT/360 basis -->
      <swapStream>
        <payerPartyReference href=""party1"" />
        <receiverPartyReference href=""party2"" />
        <calculationPeriodDates id=""floatingCalcPeriodDates"">
          <effectiveDate>
            <unadjustedDate>1994-12-14</unadjustedDate>
            <dateAdjustments>
              <businessDayConvention>NONE</businessDayConvention>
            </dateAdjustments>
          </effectiveDate>
          <terminationDate>
            <unadjustedDate>1999-12-14</unadjustedDate>
            <dateAdjustments>
              <businessDayConvention>MODFOLLOWING</businessDayConvention>
              <businessCenters id=""primaryBusinessCenters"">
                <businessCenter>DEFR</businessCenter>
              </businessCenters>
            </dateAdjustments>
          </terminationDate>
          <calculationPeriodDatesAdjustments>
            <businessDayConvention>MODFOLLOWING</businessDayConvention>
            <businessCentersReference href=""primaryBusinessCenters"" />
          </calculationPeriodDatesAdjustments>
          <calculationPeriodFrequency>
            <periodMultiplier>6</periodMultiplier>
            <period>M</period>
            <rollConvention>14</rollConvention>
          </calculationPeriodFrequency>
        </calculationPeriodDates>
        <paymentDates>
          <calculationPeriodDatesReference href=""floatingCalcPeriodDates"" />
          <paymentFrequency>
            <periodMultiplier>6</periodMultiplier>
            <period>M</period>
          </paymentFrequency>
          <payRelativeTo>CalculationPeriodEndDate</payRelativeTo>
          <paymentDatesAdjustments>
            <businessDayConvention>MODFOLLOWING</businessDayConvention>
            <businessCentersReference href=""primaryBusinessCenters"" />
          </paymentDatesAdjustments>
        </paymentDates>
        <resetDates id=""resetDates"">
          <calculationPeriodDatesReference href=""floatingCalcPeriodDates"" />
          <resetRelativeTo>CalculationPeriodStartDate</resetRelativeTo>
          <fixingDates>
            <periodMultiplier>-2</periodMultiplier>
            <period>D</period>
            <dayType>Business</dayType>
            <businessDayConvention>NONE</businessDayConvention>
            <businessCenters>
              <businessCenter>GBLO</businessCenter>
            </businessCenters>
            <dateRelativeTo href=""resetDates"" />
          </fixingDates>
          <resetFrequency>
            <periodMultiplier>6</periodMultiplier>
            <period>M</period>
          </resetFrequency>
          <resetDatesAdjustments>
            <businessDayConvention>MODFOLLOWING</businessDayConvention>
            <businessCentersReference href=""primaryBusinessCenters"" />
          </resetDatesAdjustments>
        </resetDates>
        <calculationPeriodAmount>
          <calculation>
            <notionalSchedule>
              <notionalStepSchedule>
                <initialValue>50000000.00</initialValue>
                <currency currencyScheme=""http://www.fpml.org/ext/iso4217"">EUR</currency>
              </notionalStepSchedule>
            </notionalSchedule>
            <floatingRateCalculation>
              <floatingRateIndex>EUR-LIBOR-BBA</floatingRateIndex>
              <indexTenor>
                <periodMultiplier>6</periodMultiplier>
                <period>M</period>
              </indexTenor>
            </floatingRateCalculation>
            <dayCountFraction>ACT/360</dayCountFraction>
          </calculation>
        </calculationPeriodAmount>
      </swapStream>
      <!-- Barclays pays the 6% fixed rate every year on a 30E/360 basis -->
      <swapStream>
        <payerPartyReference href=""party2"" />
        <receiverPartyReference href=""party1"" />
        <calculationPeriodDates id=""fixedCalcPeriodDates"">
          <effectiveDate>
            <unadjustedDate>1994-12-14</unadjustedDate>
            <dateAdjustments>
              <businessDayConvention>NONE</businessDayConvention>
            </dateAdjustments>
          </effectiveDate>
          <terminationDate>
            <unadjustedDate>1999-12-14</unadjustedDate>
            <dateAdjustments>
              <businessDayConvention>MODFOLLOWING</businessDayConvention>
              <businessCentersReference href=""primaryBusinessCenters"" />
            </dateAdjustments>
          </terminationDate>
          <calculationPeriodDatesAdjustments>
            <businessDayConvention>MODFOLLOWING</businessDayConvention>
            <businessCentersReference href=""primaryBusinessCenters"" />
          </calculationPeriodDatesAdjustments>
          <calculationPeriodFrequency>
            <periodMultiplier>1</periodMultiplier>
            <period>Y</period>
            <rollConvention>14</rollConvention>
          </calculationPeriodFrequency>
        </calculationPeriodDates>
        <paymentDates>
          <calculationPeriodDatesReference href=""fixedCalcPeriodDates"" />
          <paymentFrequency>
            <periodMultiplier>1</periodMultiplier>
            <period>Y</period>
          </paymentFrequency>
          <payRelativeTo>CalculationPeriodEndDate</payRelativeTo>
          <paymentDatesAdjustments>
            <businessDayConvention>MODFOLLOWING</businessDayConvention>
            <businessCentersReference href=""primaryBusinessCenters"" />
          </paymentDatesAdjustments>
        </paymentDates>
        <calculationPeriodAmount>
          <calculation>
            <notionalSchedule>
              <notionalStepSchedule>
                <initialValue>50000000.00</initialValue>
                <currency currencyScheme=""http://www.fpml.org/ext/iso4217"">EUR</currency>
              </notionalStepSchedule>
            </notionalSchedule>
            <fixedRateSchedule>
              <initialValue>0.06</initialValue>
            </fixedRateSchedule>
            <dayCountFraction>30E/360</dayCountFraction>
          </calculation>
        </calculationPeriodAmount>
      </swapStream>
    </swap>
  </trade>
  <party id=""party1"">
    <partyId>PARTYAUS33</partyId>
    <partyName>Party A</partyName>
  </party>
  <party id=""party2"">
    <partyId>BARCGB2L</partyId>
  </party>
</dataDocument>";
        }

        private void BtnCreateTradeClick(object sender, RoutedEventArgs e)
        {
            var client = new TradeServiceClient();

            try
            {
                var createTradeResult = client.CreateTrade(TxtBoxTradeRepresentation.Text,
                                                           TxtBoxSourceApplicationId.Text);
                TxtBoxResults.Text = createTradeResult.TradeCreated ? "trade created" : "trade not created.";
            }
            catch (FaultException<InvalidTradeSubmission> invalidTradeSubmission)
            {
                TxtBoxResults.Text = "FaultException<InvalidTradeSubmission> : " + invalidTradeSubmission.Detail.Message;
            }
            catch (FaultException fe)
            {
                TxtBoxResults.Text = "Fault Exception : " + fe.Message;
            }
            catch (CommunicationException communicationException)
            {
                TxtBoxResults.Text = "Communication Exception : " + communicationException.Message;
            }
            catch (TimeoutException timeoutException)
            {
                TxtBoxResults.Text = "Timeout Exception : " + timeoutException.Message;
            }
            catch (Exception ex)
            {
                TxtBoxResults.Text = "Exception : " + ex.Message;
            }
        }
    }
}
