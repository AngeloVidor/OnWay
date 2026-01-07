using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Application.Auth
{
    public sealed record LoginCommand(
      string Email,
      string Password
  );
}