using System.IO;
using Newtonsoft.Json;
public class MenuItem // losse item class
{
    public int ID { get; set; }
    public string Category { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
public class Foodmenu
{
    // maak nu een lijst voor iedere gang en drinken
    public List<MenuItem> Starters { get; set; }
    public List<MenuItem> Mains { get; set; }
    public List<MenuItem> Desserts { get; set; }
    public List<MenuItem> Drinks { get; set; }

    public Foodmenu()
    {
        // stel ze vast door een nieuwe lijst te maken
        Starters = new List<MenuItem>();
        Mains = new List<MenuItem>();
        Desserts = new List<MenuItem>();
        Drinks = new List<MenuItem>();
    }
}
// maak de menu importer
public class MenuImporter
{
    public static Foodmenu ImportFromJson(string filename)
    {
        // read the JSON data from file into a string
        string json = File.ReadAllText(filename);

        // deserialize the JSON data into a dynamic object
        dynamic data = JsonConvert.DeserializeObject(json);

        // create a new instance of the Foodmenu class
        Foodmenu menu = new Foodmenu();

        // add the menu items to the corresponding lists
        foreach (var item in data.Starters)
        {
            menu.Starters.Add(new MenuItem { ID = item.Id, Name = item.Name, Price = item.Price });
        }

        foreach (var item in data.Mains)
        {
            menu.Mains.Add(new MenuItem { ID = item.Id, Name = item.Name, Price = item.Price });
        }

        foreach (var item in data.Desserts)
        {
            menu.Desserts.Add(new MenuItem { ID = item.Id, Name = item.Name, Price = item.Price });
        }

        foreach (var item in data.Drinks)
        {
            menu.Drinks.Add(new MenuItem { ID = item.Id, Name = item.Name, Price = item.Price });
        }

        return menu;
    }
}

