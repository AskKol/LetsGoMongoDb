using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsGoMangoDb.Rentals
{
    public class AdjustRentalDetailsViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        //[MaxLength(20,ErrorMessage ="The maxium number of rooms is 20")]
        public int NumberOfRooms { get; set; }
    }
}
