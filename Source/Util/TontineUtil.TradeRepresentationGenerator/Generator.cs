using System.Data;
using System.Data.SqlClient;
using System.IO;
using NUnit.Framework;

namespace TontineUtil.TradeRepresentationGenerator
{
    [TestFixture]
    public class Generator
    {
        [Test]
        [Ignore]
        public void Generate()
        {
            int tradeId = 1;

            while (tradeId < 100002)
            {
                // get trade from database
                string counterpartyId;
                string counterpartyName;
                string tradeReference;
                using (
                    var connection =
                        new SqlConnection(@"Data Source=FERRO;Initial Catalog=Hermes;Integrated Security=True;"))
                {
                    using (var command = new SqlCommand("select t.trade_reference, t.counterparty_id, c.company_name from trade t join company c on t.counterparty_id = c.company_id where t.trade_id = " + tradeId, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        var reader = command.ExecuteReader();
                        reader.Read();
                        // extract required columns
                        counterpartyId = reader["counterparty_id"].ToString();
                        counterpartyName = reader["company_name"].ToString();
                        tradeReference = reader["trade_reference"].ToString();
                    }
                }

                // replace required values in template
                string template = File.ReadAllText(@"C:\Users\liam\programming\Tontine\Source\Tests\TradeRepresentationGenerator\Resources\ird_template.xml");
                template = template.Replace("TRADEID", tradeReference.TrimEnd());
                template = template.Replace("COUNTERPARTYID", counterpartyId);
                template = template.Replace("COUNTERPARTYNAME", counterpartyName.Replace("'", "''").Replace("&", "&amp;"));

                // save back to database
                using (
                    var connection =
                        new SqlConnection(@"Data Source=FERRO;Initial Catalog=Hermes;Integrated Security=True;"))
                {
                    using (var command = new SqlCommand("update trade set trade_representation = '" + template + "' where trade_id = " + tradeId, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                tradeId++;
            }
        }
    }
}
