using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Windows;
using TontineClient.Plutus.TradeService;

namespace TontineClient.Plutus
{
    public partial class MainWindow
    {
        TradeServiceClient _client;

        public MainWindow()
        {
            InitializeComponent();
            PopulateControls();
        }

        private void PopulateControls()
        {
            PopulateBindings();
            PopulateTradeML();
        }

        private void PopulateTradeML()
        {
// ReSharper disable AssignNullToNotNullAttribute
            var vanillaIRDSwap = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("TontineClient.Plutus.Resources.vanilla_ird_swap.xml")).ReadToEnd();
// ReSharper restore AssignNullToNotNullAttribute
            TxtBoxTradeRepresentation.Text = vanillaIRDSwap;
        }

        private void PopulateBindings()
        {
            Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ServiceModelSectionGroup serviceModel = ServiceModelSectionGroup.GetSectionGroup(appConfig);

            if (serviceModel != null)
                foreach (ChannelEndpointElement endpoint in serviceModel.Client.Endpoints)
                    CmbBoxBindings.Items.Add(endpoint.Name);

            CmbBoxBindings.SelectedIndex = 0;
        }

        private void BtnCreateTradeClick(object sender, RoutedEventArgs e)
        {
            _client = new TradeServiceClient(CmbBoxBindings.SelectedItem.ToString());

            try
            {
                var createTradeResult = _client.CreateTrade(TxtBoxTradeRepresentation.Text,
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
            finally
            {
                if (_client.State == CommunicationState.Faulted)
                {
                    _client.Abort();
                    _client = new TradeServiceClient(CmbBoxBindings.SelectedItem.ToString());
                }
            }
        }
    }
}
