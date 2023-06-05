namespace Project_B.Logic;
public class MenuAanpassen
{
    public static void EditMenu(string username, int id)
    {
        Console.WriteLine("[1] Item toevoegen");
        Console.WriteLine("[2] Item verwijderen");
        Console.WriteLine("[3] Gangen menu prijs aanpassen");
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
            case "3":
                ChangePrice(username, id);
                break;
            // case "3":
            //     Console.WriteLine("Functie momenteel buiten werking");
            //     Console.ReadKey();
            //     ManagerMenu.Admin_menu(username, id);// zodat je terug gaat
            //     // ChangeItem(username, id);
            //     break;
            case "T":
                ManagerMenu.Admin_menu(username, id);// zodat je terug gaat
                break;
            default:
                Console.WriteLine("Ongeldige optie. Kies opnieuw.");
                EditMenu(username, id);
                break;
        }
    }
    public static void AddItem(string username, int id)
    {
        Console.WriteLine("Voer de categorie in: (Starters/Soups/Mains/Desserts/Drinks/Wijn)");
        Console.WriteLine("*Hoofdletter gevoelig");
        string category = Console.ReadLine();

        // Check if the category is valid
        switch (category)
        {
            case "Starters":
            case "Soups":
            case "Mains":
            case "Desserts":
            case "Drinks":
            case "Wijn":
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
        ManagerMenu.Admin_menu(username, id);// zodat je terug gaat
    }

    public static void RemoveItem(string username, int user_id)
    {
        Console.WriteLine("Voer de categorie in: (Starters/Soups/Mains/Desserts/Drinks/Wijn)");
        Console.WriteLine("*Hoofdletter gevoelig");
        string category = Console.ReadLine();
        string filenaam = @"DataSources/menu.json";
        // Check if the category is valid
        switch (category)
        {
            case "Starters":
            case "Soups":
            case "Mains":
            case "Desserts":
            case "Drinks":
            case "Wijn":
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
            case "Soups":
                itemToRemove = menu.Soups.FirstOrDefault(i => i.ID == id);
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
            case "Wijn":
                itemToRemove = menu.Wijn.FirstOrDefault(i => i.ID == id);
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
            case "Soups":
                menu.Soups.Remove(itemToRemove);
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
            case "Wijn":
                menu.Wijn.Remove(itemToRemove);
                break;
        }
        MenuImporter.SaveMenuToJson(menu, filenaam);

        Console.WriteLine($"Item {itemToRemove.Name} met ID {itemToRemove.ID} is succesvol verwijderd uit de categorie {category}.");
        ManagerMenu.Admin_menu(username, user_id);// zodat je terug gaat
    }

    // make method that makes sure that u can edit an "gangen menu's" price
    public static void ChangePrice(string username, int user_id)
    {
        // get the menu stuff
        string filenaam = @"DataSources/menu.json";
        Foodmenu menu = MenuImporter.ImportFromJson(filenaam);

        // let the admin choose what menu price he wants to change 
        int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] { }, "2 Gangen menu", "3 Gangen menu", "4 Gangen menu");
        MenuItem GangenMenu = null;
        switch (selectedClass)
        {
            case 0:
                // it's the 2 gangen menu
                GangenMenu = GetGangenMenu(1, menu.Gangen);
                break;
            case 1:
                // it's the 3 gangen menu
                GangenMenu = GetGangenMenu(2, menu.Gangen);
                break;
            case 2:
                // it's the 4 gangen menu
                GangenMenu = GetGangenMenu(3, menu.Gangen);
                break;
        }

        // check if the menu item is found
        if (GangenMenu != null)
        {
            // get the new price from the admin
            Console.WriteLine($"Vul de nieuwe prijs voor {GangenMenu}:");
            decimal newPrice = Convert.ToDecimal(Console.ReadLine());

            // update the price of the menu item
            GangenMenu.Price = newPrice;

            // save the updated menu to the JSON file
            MenuImporter.SaveMenuToJson(menu, filenaam);

            Console.WriteLine($"De prijs van {GangenMenu.Name} is veranderd naar: {GangenMenu.Price}.");
        }
        else
        {
            Console.WriteLine("Er gaat iets fout!.");
        }

        ManagerMenu.Admin_menu(username, user_id); // return to the admin menu
    }

    private static MenuItem GetGangenMenu(int id, List<MenuItem> items)
    {
        foreach (MenuItem item in items)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }
    // na vragen bij PO of item aanpas wel moest, zo ja pas hem ook aan voor wijn
    // public static void ChangeItem(string username, int user_id)
    // {
    //     // ask for the category of the item to be changed
    //     Console.WriteLine("Voer de categorie in: (Starters/Mains/Desserts/Drinks)");
    //     Console.WriteLine("*Hoofdletter gevoelig");
    //     string category = Console.ReadLine();

    //     // check if the category is valid
    //     switch (category)
    //     {
    //         case "Starters":
    //         case "Mains":
    //         case "Desserts":
    //         case "Drinks":
    //             break;
    //         default:
    //             Console.WriteLine("Ongeldige categorie. Kies opnieuw.");
    //             ChangeItem(username, user_id);
    //             return;
    //     }

    //     // ask for the ID of the item to be changed
    //     Console.WriteLine("Voer de ID in van het item dat je wilt aanpassen:");
    //     int id = int.Parse(Console.ReadLine());

    //     // get the menu from the JSON file
    //     string filenaam = @"DataSources/menu.json";
    //     Foodmenu menu = MenuImporter.ImportFromJson(filenaam);

    //     // find the item in the menu based on the category and ID
    //     MenuItem itemToChange = null;
    //     switch (category)
    //     {
    //         case "Starters":
    //             itemToChange = menu.Starters.FirstOrDefault(i => i.ID == id);
    //             break;
    //         case "Mains":
    //             itemToChange = menu.Mains.FirstOrDefault(i => i.ID == id);
    //             break;
    //         case "Desserts":
    //             itemToChange = menu.Desserts.FirstOrDefault(i => i.ID == id);
    //             break;
    //         case "Drinks":
    //             itemToChange = menu.Drinks.FirstOrDefault(i => i.ID == id);
    //             break;
    //     }

    //     // if the item was not found, notify the user and return
    //     if (itemToChange == null)
    //     {
    //         Console.WriteLine($"Item met ID {id} kon niet worden gevonden in de categorie {category}.");
    //         return;
    //     }

    //     // ask for the new name
    //     Console.WriteLine($"Huidige naam: {itemToChange.Name}");
    //     Console.WriteLine("Voer de nieuwe naam in:");
    //     string name = Console.ReadLine();

    //     // check if the new name is not empty
    //     if (string.IsNullOrWhiteSpace(name))
    //     {
    //         Console.WriteLine("Naam mag niet leeg zijn. Kies opnieuw.");
    //         ChangeItem(username, user_id);
    //         return;
    //     }

    //     // ask for the new price
    //     Console.WriteLine($"Huidige prijs: {itemToChange.Price}");
    //     Console.WriteLine("Voer de nieuwe prijs in:");
    //     if (!decimal.TryParse(Console.ReadLine(), out decimal price))
    //     {
    //         Console.WriteLine("Ongeldige prijs. Kies opnieuw.");
    //         ChangeItem(username, user_id);
    //         return;
    //     }

    //     // update the item in the menu and save the changes to the JSON file
    //     itemToChange.Name = name;
    //     itemToChange.Price = price;
    //     MenuImporter.SaveMenuToJson(menu, filenaam);

    //     Console.WriteLine($"Item met ID {itemToChange.ID} is succesvol aangepast in de categorie {category}.");
    //     ManagerMenu.Admin_menu(username, user_id);// zodat je terug gaat
    // }

}