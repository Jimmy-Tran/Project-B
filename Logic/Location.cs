public class Location
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public Dictionary<string, string> OpeningHours { get; set; }

    public Location(string name, string address, string phone, string email, Dictionary<string, string> openingHours)
    {
        Name = name;
        Address = address;
        Phone = phone;
        Email = email;
        OpeningHours = openingHours;
    }
    public static Location CreateLocation() // maak de locatie aan
    {
        return new Location("Restaurant XYZ", "Main Street 123", "123456789", "info@restaurantxyz.com", new Dictionary<string, string>
        {
            { "zondag", "12:00–17:00" },
            { "maandag", "12:00–18:00" },
            { "dinsdag", "10:00–18:00" },
            { "woensdag", "10:00–18:00" },
            { "donderdag", "10:00–20:00" },
            { "vrijdag", "10:00–18:00" },
            { "zaterdag", "10:00–18:00" }
        });
    }
}
