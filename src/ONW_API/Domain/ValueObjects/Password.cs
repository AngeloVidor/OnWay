namespace OnWay.Domain.Transporters.ValueObjects;

public sealed class Password
{
    public string Hash { get; }

    public Password(string hash)
    {
        Hash = hash;
    }

    protected Password() { }

    public static Password Create(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
            throw new ArgumentException("Senha inválida");

        if (plainText.Length < 8)
            throw new ArgumentException("Senha deve ter no mínimo 8 caracteres");

        var hash = BCrypt.Net.BCrypt.HashPassword(plainText);

        return new Password(hash);
    }

    public bool Verify(string plainText)
        => BCrypt.Net.BCrypt.Verify(plainText, Hash);
}
