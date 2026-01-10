using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.Rersponses
{
    public sealed class PackageResponse
    {
        public Guid Id { get; set; }
        public Guid ShipmentId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientStreet { get; set; }
        public string RecipientNumber { get; set; }
        public string RecipientDistrict { get; set; }
        public string RecipientCity { get; set; }
        public string RecipientZipCode { get; set; }
    }
}