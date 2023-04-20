using System.IO;
using Newtonsoft.Json;
public class MenuItem // losse item class
{
    public int ID { get; set; }
    public string Category { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
// test
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
    public static int GetNextId(Foodmenu menu)
    {
        // Get the last item in the last non-empty menu category
        MenuItem lastItem = menu.Drinks.LastOrDefault() ?? menu.Desserts.LastOrDefault() ?? menu.Mains.LastOrDefault() ?? menu.Starters.LastOrDefault();

        // If there are no items in the menu, start with ID 1
        if (lastItem == null)
        {
            return 1;
        }

        // Otherwise, return the ID of the last item + 1
        return lastItem.ID + 1;
    }


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
            menu.Starters.Add(new MenuItem { ID = item.ID, Name = item.Name, Price = item.Price });
        }

        foreach (var item in data.Mains)
        {
            menu.Mains.Add(new MenuItem { ID = item.ID, Name = item.Name, Price = item.Price });
        }

        foreach (var item in data.Desserts)
        {
            menu.Desserts.Add(new MenuItem { ID = item.ID, Name = item.Name, Price = item.Price });
        }

        foreach (var item in data.Drinks)
        {
            menu.Drinks.Add(new MenuItem { ID = item.ID, Name = item.Name, Price = item.Price });
        }

        return menu;
    }
    public static void AddMenuItem(Foodmenu menu, MenuItem item, string filename)
    {
        // Determine which list to add the item to based on its category
        switch (item.Category)
        {
            case "Starters":
                item.ID = menu.Starters.Count > 0 ? menu.Starters.Max(i => i.ID) + 1 : 1;
                item.Category = null; // set the category property to null
                menu.Starters.Add(item);
                break;
            case "Mains":
                item.ID = menu.Mains.Count > 0 ? menu.Mains.Max(i => i.ID) + 1 : 1;
                item.Category = null; // set the category property to null
                menu.Mains.Add(item);
                break;
            case "Desserts":
                item.ID = menu.Desserts.Count > 0 ? menu.Desserts.Max(i => i.ID) + 1 : 1;
                item.Category = null; // set the category property to null
                menu.Desserts.Add(item);
                break;
            case "Drinks":
                item.ID = menu.Drinks.Count > 0 ? menu.Drinks.Max(i => i.ID) + 1 : 1;
                item.Category = null; // set the category property to null
                menu.Drinks.Add(item);
                break;
            default:
                throw new Exception("Invalid category specified");
        }

        // Serialize the updated menu to JSON and write it to the file
        string json = JsonConvert.SerializeObject(menu);
        File.WriteAllText(filename, json);
    }
    public static void RemoveItem(Foodmenu menu, string category, int id, string filename)
    {
        // Determine which list to search for the item based on its category
        List<MenuItem> itemList;
        switch (category)
        {
            case "Starters":
                itemList = menu.Starters;
                break;
            case "Mains":
                itemList = menu.Mains;
                break;
            case "Desserts":
                itemList = menu.Desserts;
                break;
            case "Drinks":
                itemList = menu.Drinks;
                break;
            default:
                throw new Exception("Invalid category specified");
        }

        // Find the item with the specified ID in the list
        MenuItem item = itemList.FirstOrDefault(i => i.ID == id);

        // If the item was not found, display an error message and return
        if (item == null)
        {
            Console.WriteLine("Item not found");
            return;
        }

        // Remove the item from the list
        itemList.Remove(item);

        // Serialize the updated menu to JSON and write it to the file
        string json = JsonConvert.SerializeObject(menu);
        File.WriteAllText(filename, json);
    }
    public static void SaveMenuToJson(Foodmenu menu, string filename)
    {
        // Serialize the menu to JSON and write it to the file
        string json = JsonConvert.SerializeObject(menu);
        File.WriteAllText(filename, json);
    }

}

