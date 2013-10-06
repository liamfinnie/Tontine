using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TontineService.CountryReferenceData.Models;

namespace TontineUtil.CountryAzureTableGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void GenerateCountries()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");

            var countries = File.ReadAllLines(@"Resources\countries.txt", Encoding.UTF8);
            var columns = new string[0];
            foreach (var country in countries)
            {
                try
                {
                    columns = country.Split('\t');

                    var countryEntity1 = new Country(columns[6], columns[3])
                    {
                        NumberCode = int.Parse(columns[0]),
                        Alpha2Code = columns[1],
                        Alpha3Code = columns[2],
                        Capital = columns[4],
                        CurrencyAlpha3Code = columns[5],
                        Flag = File.Exists(@"Resources\flags\" + columns[2] + ".png")
                            ? File.ReadAllBytes(@"Resources\flags\" + columns[2] + ".png")
                            : null
                    };

                    // Create the TableOperation that inserts the customer entity.
                    TableOperation insertOperation = TableOperation.InsertOrMerge(countryEntity1);

                    // Execute the insert operation.
                    table.Execute(insertOperation);
                }
                catch (Exception ex)
                {
                    if (InvokeRequired)
                    {
                        BeginInvoke((MethodInvoker) (() => MessageBox.Show(string.Format("{0}:{1}", columns[0], ex.Message))));
                        return;
                    }

                    MessageBox.Show(string.Format("{0}:{1}", columns[0], ex.Message));
                }
            }

            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker) (() => MessageBox.Show(@"Completed.")));
                return;
            }

            MessageBox.Show(@"Completed.");
        }

        private void btnGenerateCountries_Click(object sender, EventArgs e)
        {
            var t = new Thread(GenerateCountries);
            t.Start();
        }

        private void btnGetCountriesInRegion_Click(object sender, EventArgs e)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");

            TableQuery<Country> query = new TableQuery<Country>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, txtBoxRegion.Text));

            listBoxCountriesInRegion.Items.Clear();
            foreach (Country entity in table.ExecuteQuery(query))
                listBoxCountriesInRegion.Items.Add(entity.RowKey);
        }
    }
}
