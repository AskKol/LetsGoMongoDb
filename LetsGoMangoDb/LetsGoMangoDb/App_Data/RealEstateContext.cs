using LetsGoMangoDb.Properties;
using LetsGoMangoDb.Rentals;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsGoMangoDb.App_Data
{
    public class RealEstateContext
    {
        public IMongoDatabase Database;
        public IMongoCollection<Rental> Rentals
        {
            get
            {
                return Database.GetCollection<Rental>("rentals");
            }
        }
        public RealEstateContext()
        {
            var client = new MongoClient(Settings.Default.RealEstateConnection);
            Database = client.GetDatabase(Settings.Default.RealEstateDatabaseName);
        }
    }
}
