using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.Shipments
{
    public sealed record GetShipmentDetailsCommand(Guid ShipmentId);

}