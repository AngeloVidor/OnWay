using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Application.OpenRoute
{
    public class GeocodeSearchCommand
    {
        public Address Address { get; set; }

    }
}