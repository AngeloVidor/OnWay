using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnWay.API.Domain.Entities;

namespace ONW_API.Application.Tokens
{
    public interface ITokenService
    {
        string GenerateToken(Transporter transporter);
    }
}