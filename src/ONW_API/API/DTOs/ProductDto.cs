using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.API.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public double Weight { get; set; } // kg
        public string Status { get; set; } = null!;
    }
}