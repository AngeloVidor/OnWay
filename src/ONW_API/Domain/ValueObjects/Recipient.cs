using ONW_API.Domain.ValueObjects;
using OnWay.Domain.Transporters.ValueObjects;

namespace ONW_API.Domain.ValueObjects;

public sealed record Recipient(
    string Name,
    Email Email,
    PhoneNumber Phone,
    Address Address
);
