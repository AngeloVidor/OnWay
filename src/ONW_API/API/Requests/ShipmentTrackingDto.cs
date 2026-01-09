using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.API.DTOs
{
    public class ShipmentTrackingDto
    {
        public string TrackingCode { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public Guid? DriverId { get; set; }
        public List<TrackingEventDto> Events { get; set; } = new();
    }

}