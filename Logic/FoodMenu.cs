using System.IO;
using Newtonsoft.Json;
// maak de menu importer
public class MenuImporter
{
    public static int GetNextId(Foodmenu menu)
    {
        // Get the last item in the last non-empty menu category
        MenuItem? lastItem = menu.Drinks.LastOrDefault() ?? menu.Desserts.LastOrDefault() ?? menu.Mains.LastOrDefault() ?? menu.Starters.LastOrDefault();

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
        dynamic? data = JsonConvert.DeserializeObject(json);

        // create a new instance of the Foodmenu class
        Foodmenu menu = new Foodmenu();

        // add the menu items to the corresponding lists
        if (data != null)
        {
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
            foreach (var item in data.Wijn)
            {
                menu.Wijn.Add(new MenuItem { ID = item.ID, Name = item.Name, Price = item.Price });
            }
            foreach (var item in data.Soups)
            {
                menu.Soups.Add(new MenuItem { ID = item.ID, Name = item.Name, Price = item.Price });
            }
            foreach (var item in data.Gangen)
            {
                menu.Gangen.Add(new MenuItem { ID = item.ID, Name = item.Name, Price = item.Price });
            }
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
            case "Soups":
                item.ID = menu.Soups.Count > 0 ? menu.Soups.Max(i => i.ID) + 1 : 1;
                item.Category = null; // set the category property to null
                menu.Soups.Add(item);
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
            case "Wijn":
                item.ID = menu.Wijn.Count > 0 ? menu.Wijn.Max(i => i.ID) + 1 : 1;
                item.Category = null; // set the category property to null
                menu.Wijn.Add(item);
                break;
            case "Gangen":
                item.ID = menu.Gangen.Count > 0 ? menu.Gangen.Max(i => i.ID) + 1 : 1;
                item.Category = null; // set the category property to null
                menu.Gangen.Add(item);
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
            case "Wijn":
                itemList = menu.Wijn;
                break;
            case "Soups":
                itemList = menu.Wijn;
                break;
            case "Gangen":
                itemList = menu.Gangen;
                break;
            default:
                throw new Exception("Invalid category specified");
        }

        // Find the item with the specified ID in the list
        MenuItem? item = itemList.FirstOrDefault(i => i.ID == id);

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
        string json = JsonConvert.SerializeObject(menu, Formatting.Indented);
        File.WriteAllText(filename, json);
    }

}

