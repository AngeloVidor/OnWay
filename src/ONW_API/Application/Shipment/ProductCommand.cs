using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.Shipment
{
    public sealed class ProductCommand
    {
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
    }
}