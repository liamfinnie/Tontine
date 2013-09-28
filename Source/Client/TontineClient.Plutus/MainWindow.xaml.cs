using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
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
// ReSharper disable once AssignNullToNotNullAttribute
            PopulateTradeML(new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("TontineClient.Plutus.Resources.vanilla_ird_swap.xml")).ReadToEnd());
        }

        private void PopulateTradeML(string tradeML)
        {
            TxtBoxTradeRepresentation.Text = tradeML;
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

        private async Task SubmitTrade(string sourceApplicationCode, string tradeRepresentation)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceApplicationCode) || string.IsNullOrEmpty(tradeRepresentation))
                    UpdateResult("Please provide a Source Application Code and Trade Representation.");
                else
                {
                    Task<CreateTradeResult> task = _client.CreateTradeAsync(tradeRepresentation, sourceApplicationCode);
                    CreateTradeResult result = await task;
                    UpdateResult(result.TradeCreated ? "Trade created." : "Trade not created.");
                }
            }
            catch (FaultException<InvalidTradeSubmission> invalidTradeSubmission)
            {
                UpdateResult("FaultException<InvalidTradeSubmission> : " + invalidTradeSubmission.Detail.Message);
            }
            catch (FaultException fe)
            {
                UpdateResult("Fault Exception : " + fe.Message);
            }
            catch (CommunicationException communicationException)
            {
                UpdateResult("Communication Exception : " + communicationException.Message);
            }
            catch (TimeoutException timeoutException)
            {
                UpdateResult("Timeout Exception : " + timeoutException.Message);
            }
            catch (Exception ex)
            {
                UpdateResult("Exception : " + ex.Message);
            }
            finally
            {
                if (_client.State == CommunicationState.Faulted)
                {
                    _client.Abort();
                    RecreateClient();
                }
            }
        }

        private void UpdateResult(string result)
        {
            TxtBoxResults.Text = result;
        }

        private void RecreateClient()
        {
            _client = new TradeServiceClient(CmbBoxBindings.SelectedItem.ToString());
        }

        private async void BtnCreateTradeClick(object sender, RoutedEventArgs e)
        {
            TxtBoxResults.Clear();
            RecreateClient();
            await SubmitTrade(TxtBoxSourceApplicationCode.Text, TxtBoxTradeRepresentation.Text);
        }

        private void OpenTradeML(object sender, ExecutedRoutedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.CurrentDirectory;
            dialog.Filter = "Trade ML files (*.xml)|*.xml";
            
            if(dialog.ShowDialog().Value)
            {
                string fileContents = File.ReadAllText(dialog.FileName);
                PopulateTradeML(fileContents);
            }
        }
    }
}
