using System;
using System.Collections.Generic;
using ONW_API.API.Requests;

namespace ONW_API.API.DTOs
{
    public sealed class RecentShipmentDto
    {
        public string TrackingCode { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Route { get; set; } = null!;
        public List<PackageDto> Packages { get; set; } = new();
    }


}
