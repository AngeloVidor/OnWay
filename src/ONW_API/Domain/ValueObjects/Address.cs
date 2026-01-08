namespace ONW_API.Domain.ValueObjects;

public sealed record Address(
    string Street,
    string Number,
    string District,
    string City,
    string State,
    string ZipCode
);
