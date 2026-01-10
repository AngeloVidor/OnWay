using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.ValueObjects;

namespace ONW_API.Infrastructure.Responses
{
    public sealed class RecipientResponse
    {
        public string name { get; init; } = null!;
        public string email { get; init; } = null!;
        public string phone { get; init; } = null!;
        public Address address { get; init; } = null!;
    }

}