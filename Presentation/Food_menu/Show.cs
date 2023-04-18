public class Show
{
    public static void Menu()
    {
        Foodmenu menu = MenuImporter.ImportFromJson(@"DataSources/menu.json");
        Console.WriteLine("Voorgerecht:");
        DisplayMenuItems(menu.Starters);

        Console.WriteLine("\nHoofdgerecht:");
        DisplayMenuItems(menu.Mains);

        Console.WriteLine("\nDessert:");
        DisplayMenuItems(menu.Desserts);

        Console.WriteLine("\nDrinks:");
        DisplayMenuItems(menu.Drinks);
    }

    public static void Menu(string username, int id)
    {
        Foodmenu menu = MenuImporter.ImportFromJson(@"DataSources/menu.json");
        Console.WriteLine("Voorgerecht:");
        DisplayMenuItems(menu.Starters, username, id);

        Console.WriteLine("\nHoofdgerecht:");
        DisplayMenuItems(menu.Mains, username, id);

        Console.WriteLine("\nDessert:");
        DisplayMenuItems(menu.Desserts, username, id);

        Console.WriteLine("\nDrinks:");
        DisplayMenuItems(menu.Drinks, username, id);
    }

    private static void DisplayMenuItems(List<MenuItem> items)
    {
        foreach (MenuItem item in items)
        {
            Console.WriteLine(item.Name.PadRight(30, '.') + " " + item.Price.ToString("F2"));
        }
    }
    private static void DisplayMenuItems(List<MenuItem> items, string naam, int id)
    {
        foreach (MenuItem item in items)
        {
            Console.WriteLine(item.Name.PadRight(30, '.') + " " + item.Price.ToString("F2") + " ID: " + item.ID);
        }
    }
}

