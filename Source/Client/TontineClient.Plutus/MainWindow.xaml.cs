using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
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

        private void SubmitTrade(object state)
        {
            try
            {
                var details = state as Tuple<string, string>;
                if (details == null || string.IsNullOrEmpty(details.Item1) || string.IsNullOrEmpty(details.Item2))
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(UpdateResults), "Please provide a Source Application Code and Trade Representation.");
                else
                {
                    var createTradeResult = _client.CreateTrade(details.Item2, details.Item1);

                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(UpdateResults)
                        , createTradeResult.TradeCreated ? "Trade created" : "Trade not created.");
                }
            }
            catch (FaultException<InvalidTradeSubmission> invalidTradeSubmission)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(UpdateResults)
                    , "FaultException<InvalidTradeSubmission> : " + invalidTradeSubmission.Detail.Message);
            }
            catch (FaultException fe)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(UpdateResults)
                    , "Fault Exception : " + fe.Message);
            }
            catch (CommunicationException communicationException)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(UpdateResults)
                    , "Communication Exception : " + communicationException.Message);
            }
            catch (TimeoutException timeoutException)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(UpdateResults)
                    , "Timeout Exception : " + timeoutException.Message);
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(UpdateResults)
                    , "Exception : " + ex.Message);
            }
            finally
            {
                if (_client.State == CommunicationState.Faulted)
                {
                    _client.Abort();
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(RecreateClient));
                }
            }
        }

        private void RecreateClient()
        {
            _client = new TradeServiceClient(CmbBoxBindings.SelectedItem.ToString());
        }

        private void UpdateResults(string results)
        {
            TxtBoxResults.Text = results;
        }

        private void BtnCreateTradeClick(object sender, RoutedEventArgs e)
        {
            RecreateClient();
            var details = new Tuple<string, string>(TxtBoxSourceApplicationId.Text, TxtBoxTradeRepresentation.Text);
            ThreadPool.QueueUserWorkItem(SubmitTrade, details);
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
