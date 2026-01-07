using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.API.DTOs
{
    public class TrackingEventDto
    {
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}