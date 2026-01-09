using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.OpenRoute
{
    public class Feature
    {
        public Geometry Geometry { get; set; } = new Geometry();
        public Properties Properties { get; set; } = new Properties();
    }
}