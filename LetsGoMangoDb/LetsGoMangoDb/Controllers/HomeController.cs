using LetsGoMangoDb.App_Data;
using LetsGoMangoDb.Properties;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LetsGoMangoDb.Controllers
{
    public class HomeController : Controller
    {
        private IMongoDatabase _database= new RealEstateContext().Database;
        public HomeController()
        {
            var client = new MongoClient(Settings.Default.RealEstateConnection);
            _database = client.GetDatabase(Settings.Default.RealEstateDatabaseName);
          
        }
        public ActionResult Index()
        {
            //_database.GetCollection("realEstate")
            return Json(_database.Client.Settings.Server,JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}