using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.API.DTOs
{
    public class CreateVehicleDto
    {
        public string Plate { get; set; } = null!;
        public string Model { get; set; } = null!;
    
    }
}