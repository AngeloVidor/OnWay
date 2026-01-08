using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.Shipment
{
    public sealed record AssignDriverAndVehicleCommand(Guid ShipmentId, Guid DriverId, Guid VehicleId);

}