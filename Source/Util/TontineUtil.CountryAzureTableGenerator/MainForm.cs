using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
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
                BeginInvoke((MethodInvoker)(delegate { btnGenerateCountries.Enabled = true; MessageBox.Show(@"Completed."); }));
            }
            else
            {
                btnGenerateCountries.Enabled = true;
                MessageBox.Show(@"Completed.");
            }
        }

        private void GetCountriesInRegion()
        {
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");

            TableQuery<Country> query =
                new TableQuery<Country>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,
                    txtBoxRegion.Text));

            foreach (Country entity in table.ExecuteQuery(query))
            {
                if (InvokeRequired)
                {
                    Country country = entity;
                    BeginInvoke((MethodInvoker) (() => listBoxCountriesInRegion.Items.Add(country.RowKey)));
                }
                else
                    listBoxCountriesInRegion.Items.Add(entity.RowKey);
            }
        }
        
        private void btnGenerateCountries_Click(object sender, EventArgs e)
        {
            btnGenerateCountries.Enabled = false;
            var thread = new Thread(GenerateCountries);
            thread.Start();
        }

        private void btnGetCountriesInRegion_Click(object sender, EventArgs e)
        {
            listBoxCountriesInRegion.Items.Clear();
            var thread = new Thread(GetCountriesInRegion);
            thread.Start();
        }

        private void btnGetCountry_Click(object sender, EventArgs e)
        {
            CloudStorageAccount storageAccount =
            CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");

            TableQuery<Country> query =
                new TableQuery<Country>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal,
                    txtBoxCountryName.Text));

            var country = table.ExecuteQuery(query).FirstOrDefault();

            if (country != null)
            {
                lblCountryName.Text = country.RowKey;
                lblRegion.Text = country.PartitionKey;
                lblNumberCode.Text = country.NumberCode.ToString(CultureInfo.InvariantCulture);
                lblAlpha2Code.Text = country.Alpha2Code;
                lblAlpha3Code.Text = country.Alpha3Code;
                lblCapital.Text = country.Capital;
                lblCurrencyAlpha3Code.Text = country.CurrencyAlpha3Code;
                picBoxFlag.Image = (Bitmap)new ImageConverter().ConvertFrom(country.Flag);
            }
        }

        private void btnAddCountry_Click(object sender, EventArgs e)
        {
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("country");
            
            try
            {
                var country = new Country
                {
                    RowKey = txtBoxNewCountryName.Text,
                    PartitionKey = txtBoxNewRegion.Text,
                    NumberCode = int.Parse(txtBoxNewNumberCode.Text),
                    Alpha2Code = txtBoxNewAlpha2Code.Text,
                    Alpha3Code = txtBoxNewAlpha3Code.Text,
                    Capital = txtBoxNewCapital.Text,
                    CurrencyAlpha3Code = txtBoxNewCurrecyAlpha3Code.Text,
                    Flag = File.Exists(@"Resources\flags\" + txtBoxNewAlpha3Code.Text + ".png")
                        ? File.ReadAllBytes(@"Resources\flags\" + txtBoxNewAlpha3Code.Text + ".png")
                        : null
                };

                // Create the TableOperation that inserts the customer entity.
                TableOperation insertOperation = TableOperation.InsertOrMerge(country);

                // Execute the insert operation.
                table.Execute(insertOperation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            MessageBox.Show(txtBoxNewCountryName.Text + @" added.");
        }
    }
}