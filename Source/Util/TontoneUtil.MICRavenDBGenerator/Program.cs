using System;
using System.IO;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;

namespace TontoneUtil.MICRavenDBGenerator
{
    public class MarketIdentifier
    {
        public string ISOCountryCode { get; set; }
        public string MIC { get; set; }
        public string OperatingOrSegment { get; set; }
        public string Name { get; set; }
        public string OperatingMIC { get; set; }
        public string Acronym { get; set; }
        public string City { get; set; }
        public string Website { get; set; }
        public string CreationDate { get; set; }
        public string StatusDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
    }

    class Program
    {
        static void Main()
        {
            using (IDocumentStore store = new DocumentStore
            {
                  Url = "http://localhost:8080/"
                , DefaultDatabase = "ReferenceData"
            })
            {
                store.Initialize();
                
                using (IDocumentSession session = store.OpenSession())
                {
                    var records = File.ReadAllLines(@"..\..\ISO10383_MIC.dat").Skip(1);
                    
                    foreach (var record in records)
                    {
                        string[] splitRecord = record.Split('|');

                        var marketIdentifierCode = new MarketIdentifier
                        {
                              ISOCountryCode = splitRecord[1]
                            , MIC = splitRecord[2]
                            , OperatingOrSegment = splitRecord[4]
                            , Name = splitRecord[5]
                            , OperatingMIC = splitRecord[3]
                            , Acronym = splitRecord[6]
                            , City = splitRecord[7]
                            , Website = splitRecord[8]
                            , CreationDate = splitRecord[11]
                            , StatusDate = splitRecord[9]
                            , Status = splitRecord[10]
                            , Comments = splitRecord[12]
                        };

                        session.Store(marketIdentifierCode);
                    }
                    
                    session.SaveChanges();
                }
            }

            Console.WriteLine("Completed.");
        }
    }
}
