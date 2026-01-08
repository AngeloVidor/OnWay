using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.API.DTOs
{
    public class RecentShipmentDto
    {
        public string TrackingCode { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Route { get; set; } = null!; 
        public List<ProductDto> Products { get; set; } = new();
        public List<TrackingEventDto> TrackingHistory { get; set; } = new();
    }
}