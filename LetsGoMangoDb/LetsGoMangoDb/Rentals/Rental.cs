using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace LetsGoMangoDb.Rentals
{
    public class Rental
    {
        //private PostRental postRental;

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }
        public List<string> Address { get; set; }
        [BsonRepresentation(BsonType.Double)]
        public decimal Price { get; set; }

        public List<PriceAdjustment> Adjustments { get; set; }

        public string ImageId { get; set; }
        public string ImageType { get; set; }
        public Rental()
        {
            this.Address = new List<string>();
            this.Adjustments = new List<PriceAdjustment>();
        }

        public Rental(PostRental postRental)
        {
            this.Description = postRental.Description;
            this.Price = postRental.Price;
            this.NumberOfRooms = postRental.NumberOfRooms;
            this.Address = (postRental.Address ?? String.Empty).Split('\n').ToList();
        }

        public void AdjustPrice(AdjustPrice adjustPrice)
        {
            var adjustment = new PriceAdjustment(adjustPrice, this.Price);
            Adjustments.Add(adjustment);
            Price = adjustPrice.NewPrice;
            
        }

        public bool HasImage()
        {
            if (true)
            {
                return !String.IsNullOrEmpty(ImageId);
            }
        }
    }
}
