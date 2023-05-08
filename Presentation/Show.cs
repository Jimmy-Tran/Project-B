public class MenuPresentation
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
public class LocationPresentation
{
    public static void ShowLocation(Location location)
    {
        Console.WriteLine("Locatie: " + location.Name);
        Console.WriteLine("Adres: " + location.Address);
        Console.WriteLine("Telefoon: " + location.Phone);
        Console.WriteLine("E-mail: " + location.Email);
        Console.WriteLine("Openingstijden:");
        Console.WriteLine("zondag\t\t" + location.OpeningHours["zondag"]);
        Console.WriteLine("maandag\t\t" + location.OpeningHours["maandag"]);
        Console.WriteLine("dinsdag\t\t" + location.OpeningHours["dinsdag"]);
        Console.WriteLine("woensdag\t" + location.OpeningHours["woensdag"]);
        Console.WriteLine("donderdag\t" + location.OpeningHours["donderdag"]);
        Console.WriteLine("vrijdag\t\t" + location.OpeningHours["vrijdag"]);
        Console.WriteLine("zaterdag\t" + location.OpeningHours["zaterdag"]);
    }
}



