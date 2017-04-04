using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rs = LetsGoMangoDb.Rentals;

namespace LetsGoMangoDb.Tests.Rental
{
    [TestClass]
    public class RentalTests
    {
        public RentalTests()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }
        
        [TestMethod]
        public void ToDocument_RentalWithPrice_PriceRepresntedAsDouble()
        {
            var rental = new Rs.Rental()
            {
                Price = 1
            };

            var document = rental.ToBsonDocument();

            Assert.IsTrue(document["Price"].BsonType == BsonType.Double);
        }

        public void ToDocument_RentalWithId_IdIsRepresntedAsObjectId()
        {
            var rental = new Rs.Rental()
            {
               Id= ObjectId.GenerateNewId().ToString()
            };

            var document = rental.ToBsonDocument();

            Assert.IsTrue(document["_id"].BsonType == BsonType.ObjectId);
        }
    }
}
