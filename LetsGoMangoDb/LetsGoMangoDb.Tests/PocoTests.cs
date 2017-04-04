using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsGoMangoDb.Tests
{
    [TestClass]
    public class PocoTests
    {
        public PocoTests()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }


        public class Contact
        {
            public string EmailAddress { get;  set; }
            public string PhoneNumber { get; set; }
        }
        public class Person
        {
            public string FirstName { get; set; }
            public int Age { get; set; }

           public List<string> Address { get; set; }

            public Contact ContactDetails { get; set; }
            public Person()
            {
                this.Address = new List<string>();
                this.ContactDetails = new Contact();
            }

           
        }

        [TestMethod]
        public void Automatic()
        {
            var person = new Person()
            {
                FirstName = "James",
                Age = 21
               
            };
            person.Address.Add("212 Franklin street");
            person.Address.Add("Derby");

            person.ContactDetails.EmailAddress = "happypeople@happy.com";
            person.ContactDetails.PhoneNumber = "0808778989834";

            Console.WriteLine(person.ToJson());
        }
    }

}
