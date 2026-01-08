using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Domain.ValueObjects
{
    public enum DriverStatus
    {
        Available = 1,
        OnRoute = 2,
        Unavailable = 3,
        Waiting = 4
    }
}