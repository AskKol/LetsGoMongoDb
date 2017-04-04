using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsGoMangoDb.Rentals
{
    public class PriceAdjustment
    {
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string Reason { get; set; }

        public PriceAdjustment(AdjustPrice adjustPrice, decimal oldPrice)
        {
            this.OldPrice = oldPrice;
            this.NewPrice = adjustPrice.NewPrice;
            this.Reason = adjustPrice.Reason;

        }

       public string Describe()
        {
            return String.Format("{0} -> {1}: {2}", this.OldPrice, this.NewPrice, this.Reason);
        }
    }
}
