using ONW_API.Domain.Entities;
using ONW_API.Domain.Repositories;
using OnWay.Domain.Transporters.ValueObjects;

namespace ONW_API.Application.Drivers;

public sealed class CreateDriverCommand
{
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Vehicle { get; set; } = "";
    public string VehiclePlate { get; set; } = "";
}

