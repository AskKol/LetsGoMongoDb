using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsGoMangoDb.Tests
{
    public class BsonDocumentTests
    {
        [Test]
        public void EmptyDocument()
        {
            var document = new BsonDocument();

            Console.WriteLine(document.ToJson());
        }


    }

    [TestClass]
    public class BsonDocumentTestsMsTest
    {
        public BsonDocumentTestsMsTest()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }
        [TestMethod]
        public void EmptyDocument_MsTest()
        {
            var document = new BsonDocument();
           
            Console.WriteLine(document.ToJson());
        }

        [TestMethod]
        public void AddElements_MsTest()
        {
            var person = new BsonDocument() {
                {"Age",new BsonInt32(23) },
                {"IsAlive",true }
            };
            person.Add("FirstName", new BsonString("bob"));

            Console.WriteLine(person);
        }

        [TestMethod]
        public void AddingArrays()
        {
            var person = new BsonDocument()
            {
                {"Address", new BsonArray(new []
                {
                    "19 Crompton street",
                    "Derby",
                    "DE1 1NY"
                })}
            };

            Console.WriteLine(person);
        }

        [TestMethod]
        public void EmbeddedDocument()
        {
            var person = new BsonDocument()
            {
                {
                    "Contact", new BsonDocument()
                    {
                        {"Phone","123-233-2323"},
                        {"Email","happypeople@happypeople.com" }
                    }
                }
            };

            Console.WriteLine(person);
        }

        [TestMethod]
        public void BsonValueConversions()
        {
            var person = new BsonDocument()
            {
                {"Age",31 }
            };

            Console.WriteLine(person["Age"].AsInt32 + 10);

            Console.WriteLine(person["Age"].IsInt32);
            Console.WriteLine(person["Age"].IsDouble);
        }

        [TestMethod]
        public void ToBson()
        {
            var person = new BsonDocument()
            {
                {"FirstName","Laughter" },
                {"LastName","Cheery" }
            };

            var bson = person.ToBson();
            Console.WriteLine(BitConverter.ToString(bson));

            var deserializedPerson = BsonSerializer.Deserialize<BsonDocument>(bson);
            Console.WriteLine(deserializedPerson);
        }
    }
}
