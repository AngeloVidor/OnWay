public sealed class Location
{
    public string Address { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    private Location() { }

    public Location(string address, string city, string state, double lat, double lng)
    {
        Address = address;
        City = city;
        State = state;
        Latitude = lat;
        Longitude = lng;
    }
}
