using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.API.Requests
{
    public sealed class PackageDto
    {
        public string TrackingCode { get; set; } = null!;
        public string RecipientName { get; set; } = null!;
        public string RecipientEmail { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}