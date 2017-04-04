using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsGoMangoDb.Rentals
{
    public class RentalListViewModel
    {
        public IEnumerable<Rental> Rentals { get; set; }
        public RentalFilter Filters { get; set; }
    }
}
