using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.Geolocation
{
    public sealed record WazeRouteRequest(
        string Street,
        string? Number,
        string District,
        string City,
        string State,
        string? ZipCode
    );


}