using OnWay.Domain.Transporters.ValueObjects;

namespace OnWay.API.Domain.Entities;

public sealed class Transporter
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber Phone { get; private set; }
    public Password Password { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected Transporter() { }

    private Transporter(
        string name,
        Email email,
        PhoneNumber phone,
        Password password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Phone = phone;
        Password = password;
        CreatedAt = DateTime.UtcNow;
    }

    public static Transporter Create(
        string name,
        Email email,
        PhoneNumber phone,
        Password password)
    {
        return new Transporter(name, email, phone, password);
    }
}
