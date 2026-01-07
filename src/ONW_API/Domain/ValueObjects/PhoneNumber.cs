using System.Text.RegularExpressions;

namespace OnWay.Domain.Transporters.ValueObjects;

public sealed class PhoneNumber
{
    public string Value { get; }

    public PhoneNumber(string value)
    {
        Value = value;
    }

    protected PhoneNumber() { }

    public static PhoneNumber Create(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Telefone inválido");

        var normalized = Regex.Replace(phone, @"\D", "");

        if (normalized.Length < 10 || normalized.Length > 11)
            throw new ArgumentException("Telefone inválido");

        return new PhoneNumber(normalized);
    }
}
