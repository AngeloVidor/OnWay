using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.OpenRoute
{
    public class Geometry
    {
        public string Type { get; set; } = string.Empty;
        public double[] Coordinates { get; set; } = Array.Empty<double>();
    }
}