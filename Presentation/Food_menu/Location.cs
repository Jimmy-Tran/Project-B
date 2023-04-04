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

    public void Show()
    {
        Console.WriteLine("Locatie: " + Name);
        Console.WriteLine("Adres: " + Address);
        Console.WriteLine("Telefoon: " + Phone);
        Console.WriteLine("E-mail: " + Email);
        Console.WriteLine("Openingstijden:");
        Console.WriteLine("zondag\t\t" + OpeningHours["zondag"]);
        Console.WriteLine("maandag\t\t" + OpeningHours["maandag"]);
        Console.WriteLine("dinsdag\t\t" + OpeningHours["dinsdag"]);
        Console.WriteLine("woensdag\t" + OpeningHours["woensdag"]);
        Console.WriteLine("donderdag\t" + OpeningHours["donderdag"]);
        Console.WriteLine("vrijdag\t\t" + OpeningHours["vrijdag"]);
        Console.WriteLine("zaterdag\t" + OpeningHours["zaterdag"]);
    }
}
