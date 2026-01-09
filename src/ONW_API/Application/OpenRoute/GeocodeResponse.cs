using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.OpenRoute
{
    public class GeocodeResponse
    {
        public Feature[] Features { get; set; } = Array.Empty<Feature>();
    }   
}