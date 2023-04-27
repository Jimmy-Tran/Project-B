namespace Project_B.Logic;
public class MenuAanpassen
{
    public static void EditMenu(string username, int id)
    {
        Console.WriteLine("[1] Item toevoegen");
        Console.WriteLine("[2] Item verwijderen");
        Console.WriteLine("[T] Terug");

        string input = Console.ReadLine();
        switch (input.ToUpper())
        {
            case "1":
                AddItem(username, id);
                break;
            case "2":
                RemoveItem(username, id);
                break;
            case "T":
                Menu.Admin_menu(username, id);// zodat je terug gaat
                break;
            default:
                Console.WriteLine("Ongeldige optie. Kies opnieuw.");
                EditMenu(username, id);
                break;
        }
    }
    public static void AddItem(string username, int id)
    {
        Console.WriteLine("Voer de categorie in:(Starters/Mains/Desserts/Drinks)");
        Console.WriteLine("*Hoofdletter gevoelig");
        string category = Console.ReadLine();

        // Check if the category is valid
        switch (category)
        {
            case "Starters":
            case "Mains":
            case "Desserts":
            case "Drinks":
                break;
            default:
                Console.WriteLine("Ongeldige categorie. Kies opnieuw.");
                AddItem(username, id);
                return;
        }

        Console.WriteLine("Voer de naam in:");
        string name = Console.ReadLine();

        // Check if the name is not empty
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Naam mag niet leeg zijn. Kies opnieuw.");
            AddItem(username, id);
            return;
        }

        Console.WriteLine("Voer de prijs in:");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Ongeldige prijs. Kies opnieuw.");
            AddItem(username, id);
            return;
        }
        // Retrieve the menu object from the file
        string filenaam = @"DataSources/menu.json";
        Foodmenu menu = MenuImporter.ImportFromJson(filenaam);
        MenuItem newItem = new MenuItem
        {
            ID = MenuImporter.GetNextId(menu),
            Category = category,
            Name = name,
            Price = price
        };

        // Add the item to the menu and update the file
        MenuImporter.AddMenuItem(menu, newItem, filenaam);

        Console.WriteLine("Item succesvol toegevoegd!");
        Menu.Admin_menu(username, id);// zodat je terug gaat
    }

    public static void DisplayMenuItems(List<MenuItem> items)
    {
        foreach (MenuItem item in items)
        {
            Console.WriteLine($"ID: {item.ID} | Name: {item.Name} | Price: {item.Price:C}");
        }
    }

    public static void RemoveItem(string username, int user_id)
    {
        // ask for the category from which the user wants to remove an item
        Console.WriteLine("Voer de categorie in: (Starters/Mains/Desserts/Drinks)");
        Console.WriteLine("*Hoofdletter gevoelig");
        string category = Console.ReadLine();
        string filenaam = @"DataSources/menu.json";
        // check if the category is valid
        switch (category)
        {
            case "Starters":
            case "Mains":
            case "Desserts":
            case "Drinks":
                break;
            default:
                Console.WriteLine("Ongeldige categorie. Kies opnieuw.");
                RemoveItem(username, user_id);
                return;
        }

        // ask for the ID of the item the user wants to remove
        Console.WriteLine("Voer de ID in van het item dat je wilt verwijderen:");
        int id = int.Parse(Console.ReadLine());

        // get the menu from the JSON file
        Foodmenu menu = MenuImporter.ImportFromJson(filenaam);

        // find the item in the menu based on the category and ID
        MenuItem itemToRemove = null;
        switch (category)
        {
            case "Starters":
                itemToRemove = menu.Starters.FirstOrDefault(i => i.ID == id);
                break;
            case "Mains":
                itemToRemove = menu.Mains.FirstOrDefault(i => i.ID == id);
                break;
            case "Desserts":
                itemToRemove = menu.Desserts.FirstOrDefault(i => i.ID == id);
                break;
            case "Drinks":
                itemToRemove = menu.Drinks.FirstOrDefault(i => i.ID == id);
                break;
        }

        // if the item was not found, notify the user and return
        if (itemToRemove == null)
        {
            Console.WriteLine($"Item met ID {id} kon niet worden gevonden in de categorie {category}.");
            return;
        }

        // remove the item from the menu and update the JSON file
        switch (category)
        {
            case "Starters":
                menu.Starters.Remove(itemToRemove);
                break;
            case "Mains":
                menu.Mains.Remove(itemToRemove);
                break;
            case "Desserts":
                menu.Desserts.Remove(itemToRemove);
                break;
            case "Drinks":
                menu.Drinks.Remove(itemToRemove);
                break;
        }
        MenuImporter.SaveMenuToJson(menu, filenaam);

        Console.WriteLine($"Item {itemToRemove.Name} met ID {itemToRemove.ID} is succesvol verwijderd uit de categorie {category}.");
        Menu.Admin_menu(username, user_id);// zodat je terug gaat
    }
}