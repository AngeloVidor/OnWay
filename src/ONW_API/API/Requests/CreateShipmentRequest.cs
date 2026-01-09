using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.API.DTOs;

namespace ONW_API.Application.DTOs
{
    public sealed class CreateShipmentRequest
    {
        public LocationDto Origin { get; set; } = null!;
        public LocationDto Destination { get; set; } = null!;
    }
}