using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONW_API.Infrastructure.Auth
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = null!;
        public int ExpirationMinutes { get; set; }
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
    }
}