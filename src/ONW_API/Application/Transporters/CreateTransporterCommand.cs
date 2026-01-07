using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.Transporters
{
    public sealed record CreateTransporterCommand(
        string Name,
        string Email,
        string Phone,
        string Password
    );
}