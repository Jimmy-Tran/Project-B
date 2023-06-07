using System.Text.Json;
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

    public static Location CreateLocation()
    {
        string json = File.ReadAllText(@"DataSources/Informatie.json");
        Location location = JsonSerializer.Deserialize<Location>(json);

        return location;
    }

    public void SaveToJsonFile(string filePath)
    {
        string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }
    public static Location ReadLocationFromJson(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Location>(json);
    }
}