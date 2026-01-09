using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.API.Requests
{
    public sealed class ActiveShipmentDto
    {
        public string TrackingCode { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Route { get; set; } = null!;
        public List<PackageDto> Packages { get; set; } = new();
    }
}