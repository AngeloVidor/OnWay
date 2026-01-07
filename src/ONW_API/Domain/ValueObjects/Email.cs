using System.Text.RegularExpressions;

namespace OnWay.Domain.Transporters.ValueObjects;

public sealed class Email
{
    public string Value { get; }

    public Email(string value)
    {
        Value = value;
    }

    protected Email() { }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email inválido");

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Formato de email inválido");

        return new Email(email.Trim().ToLowerInvariant());
    }
}
