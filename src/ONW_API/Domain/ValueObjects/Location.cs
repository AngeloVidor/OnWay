public sealed class Location
{
    public string Address { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }


    private Location() { }

    public Location(string address, string city, string state)
    {
        Address = address;
        City = city;
        State = state;
    }
}
