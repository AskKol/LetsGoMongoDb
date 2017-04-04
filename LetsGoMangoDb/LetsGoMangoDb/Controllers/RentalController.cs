using LetsGoMangoDb.App_Data;
using LetsGoMangoDb.Rentals;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LetsGoMangoDb.Controllers
{
    public class RentalController : Controller
    {
        public readonly RealEstateContext context = new RealEstateContext();

        public ActionResult Index(RentalFilter filters)
        {
            List<Rental> rentals;
            if (filters != null && filters.PriceLimit.HasValue)
            {

                rentals = context.Rentals.Find(r => r.Price >= filters.PriceLimit).ToList();

            }
            else
            {
                rentals = context.Rentals.Find(_ => true).ToList();
            }

            var model = new RentalListViewModel()
            {
                Rentals = rentals.OrderBy(r => r.Price),
                Filters = filters
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult PostARental()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostARental(PostRental postRental)
        {
            var rental = new Rental(postRental);
            context.Rentals.InsertOne(rental);
            return RedirectToAction("Index");

        }

        public ActionResult AdjustPrice(string id)
        {
            Rental rentalToEdit = GetRental(id);
            return View(rentalToEdit);
        }

        private Rental GetRental(string id)
        {
            return context.Rentals.Find(i => i.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public ActionResult AdjustPrice(string id, AdjustPrice adjustPrice)
        {
            Rental rentalToEdit = GetRental(id);
            rentalToEdit.AdjustPrice(adjustPrice);
            var filter = Builders<Rental>.Filter.Eq(s => s.Id, id);
            context.Rentals.ReplaceOne(filter, rentalToEdit);

            return RedirectToAction("Index", "Rental");
        }


        [HttpGet]
        public ActionResult AdjustDetails(string id)
        {
            Rental rentalToEdit = GetRental(id);
            AdjustRentalDetailsViewModel vm = new AdjustRentalDetailsViewModel()
            {
                Id = rentalToEdit.Id,
                Description = rentalToEdit.Description,
                NumberOfRooms = rentalToEdit.NumberOfRooms
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult AdjustDetails(AdjustRentalDetailsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Rental rentalToEdit = GetRental(vm.Id);
                    rentalToEdit.Description = vm.Description;
                    rentalToEdit.NumberOfRooms = vm.NumberOfRooms;

                    context.Rentals.ReplaceOne<Rental>(f => f.Id == vm.Id, rentalToEdit);
                    return RedirectToAction("Index", "Rental");
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }

            return View(vm);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                context.Rentals.FindOneAndDelete(r => r.Id == id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Rental");

        }

        [HttpGet]
        public ActionResult AttachImage(string id)
        {
            var rental = GetRental(id);
            return View(rental);

        }

        [HttpPost]
        public ActionResult AttachImage(string id, HttpPostedFileBase file)
        {
            try
            {

                var rental = GetRental(id);

                if (rental.HasImage())
                {
                    DeleteImage(rental);
                }

                StoreImage(id, file);

                return RedirectToAction("Index", "Rental");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void DeleteImage(Rental rental)
        {
            IGridFSBucket bucket = new GridFSBucket(context.Database);
            bucket.Delete(new ObjectId(rental.ImageId));

            context.Rentals.UpdateOne<Rental>(f => f.Id == rental.Id, Builders<Rental>.Update.Set(d => d.ImageId, null));
        }

        private void StoreImage(string id, HttpPostedFileBase file)
        {
            var bucket = new GridFSBucket(context.Database);
            var fileId = bucket.UploadFromStream(file.FileName, file.InputStream, new GridFSUploadOptions()
            {
                Metadata = new MongoDB.Bson.BsonDocument
                {
                    { "ContentType",file.ContentType}
                }

            });
            if (fileId != null && !String.IsNullOrEmpty(fileId.ToString()))
            {
                var updateImage = Builders<Rental>.Update.Set(u => u.ImageId, fileId.ToString()).Set(u => u.ImageType, file.ContentType);

                context.Rentals.UpdateOne<Rental>(f => f.Id == id, updateImage);
            }
            var checkItUpdateImageId = context.Rentals.Find(f => f.Id == id).FirstOrDefault();
            if (checkItUpdateImageId != null && String.IsNullOrEmpty(checkItUpdateImageId.ImageId))
            {
                var update = Builders<Rental>.Update.Set(u => u.ImageId, fileId.ToString());
                context.Rentals.UpdateOne<Rental>(f => f.Id == id, update);
            }
        }

        public ActionResult GetImage(string id)
        {
            try
            {
                IGridFSBucket bucket = new GridFSBucket(context.Database);
                // Stream destination= new FileStream(@"C:\image.png",FileMode.OpenOrCreate);




                var bytes = bucket.DownloadAsBytes(new ObjectId(id));
                var filter = Builders<Rental>.Filter.Where(f => f.ImageId == new ObjectId(id).ToString());
                var aRental = context.Rentals.Find(f => f.ImageId == id).FirstOrDefault();

                return File(bytes, aRental != null && !String.IsNullOrEmpty(aRental.ImageType) ? aRental.ImageType:"png");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
