using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace DgBar.Domain.Entities
{
    public class SheetOrder
    {
        public BigInteger Id { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalDiscount { get; set; }


        public SheetOrder()
        {
            //  var random = Guid.NewGuid();
            // Id = new BigInteger(random.ToByteArray());
        }

    }
}
