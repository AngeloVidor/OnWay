using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONW_API.Domain.ValueObjects;
using OnWay.Domain.Transporters.ValueObjects;

namespace ONW_API.Application.Packages
{
    public sealed record AddPackageCommand(
        Guid ShipmentId,
        string RecipientName,
        Email RecipientEmail,
        PhoneNumber RecipientPhone,
        Address RecipientAddress
    );

}