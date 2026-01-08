using ONW_API.Domain.ValueObjects;
using OnWay.Domain.Transporters.ValueObjects;

namespace ONW_API.Domain.ValueObjects;

public sealed record Recipient
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber Phone { get; private set; }
    public Address Address { get; private set; }

    private Recipient() { } 

    public Recipient(string name, Email email, PhoneNumber phone, Address address)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
    }
}
