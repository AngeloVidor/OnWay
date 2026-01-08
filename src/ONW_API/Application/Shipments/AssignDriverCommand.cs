using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.Shipment
{
    public sealed record AssignDriverCommand(Guid ShipmentId, Guid DriverId);

}