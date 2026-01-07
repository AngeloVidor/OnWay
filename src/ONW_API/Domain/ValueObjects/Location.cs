namespace OnWay.Domain.ValueObjects;

public sealed class Location
{
    public string Address { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }

    protected Location() { }

    public Location(string address, string city, string state)
    {
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Endereço obrigatório");
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("Cidade obrigatória");
        if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("Estado obrigatório");

        Address = address.Trim();
        City = city.Trim();
        State = state.Trim();
    }
}