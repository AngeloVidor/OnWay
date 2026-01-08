using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Domain.ValueObjects
{
    public enum ShipmentStatus
    {
        Created = 1,
        InTransit = 2,
        Delivered = 3,
        Delayed = 4
    }
}