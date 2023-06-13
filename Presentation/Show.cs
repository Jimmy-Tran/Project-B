using System.Text;
using Project_B.Logic;
public class MenuPresentation
{
    public static void Menu()
    {
        Foodmenu menu = MenuImporter.ImportFromJson(@"DataSources/menu.json");
        // maak functie om te kiezen welke menu de klant wilt
        int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] { }, "2 Gangen menu", "3 Gangen menu", "4 Gangen menu");
        switch (selectedClass)
        {
            case 0:
                // print the information
                informatie(menu.Gangen);
                // 2 gangen menu
                Console.WriteLine("\nVoorgerecht:");
                DisplayMenuItems(menu.Starters, includePrice: true);

                Console.WriteLine("\nHoofdgerecht:");
                DisplayMenuItems(menu.Mains, includePrice: true);

                Console.WriteLine("\nWijn:");
                DisplayMenuItems(menu.Wijn, includePrice: false);

                Console.WriteLine("\nDrinks:");
                DisplayMenuItems(menu.Drinks, includePrice: true);
                break;
            case 1:
                // print the information
                informatie(menu.Gangen);
                // 3 gangen menu
                Console.WriteLine("\nVoorgerecht:");
                DisplayMenuItems(menu.Starters, includePrice: true);

                Console.WriteLine("\nHoofdgerecht:");
                DisplayMenuItems(menu.Mains, includePrice: true);

                Console.WriteLine("\nNagerecht:");
                DisplayMenuItems(menu.Desserts, includePrice: true);

                Console.WriteLine("\nWijn:");
                DisplayMenuItems(menu.Wijn, includePrice: false);

                Console.WriteLine("\nDrinks:");
                DisplayMenuItems(menu.Drinks, includePrice: true);
                break;
            default:
                // print the information
                informatie(menu.Gangen);
                // 4 gangen menu
                Console.WriteLine("\nVoorgerecht:");
                DisplayMenuItems(menu.Starters, includePrice: true);

                Console.WriteLine("\nSoepen:");
                DisplayMenuItems(menu.Soups, includePrice: true);

                Console.WriteLine("\nHoofdgerecht:");
                DisplayMenuItems(menu.Mains, includePrice: true);

                Console.WriteLine("\nNagerecht:");
                DisplayMenuItems(menu.Desserts, includePrice: true);

                Console.WriteLine("\nWijn:");
                DisplayMenuItems(menu.Wijn, includePrice: false);

                Console.WriteLine("\nDrinks:");
                DisplayMenuItems(menu.Drinks, includePrice: true);
                break;
        }
    }


    public static void Menu(string username, int id)
    {
        Foodmenu menu = MenuImporter.ImportFromJson(@"DataSources/menu.json");
        // maak functie om te kiezen welke menu de klant wilt
        int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] { }, "2 Gangen menu", "3 Gangen menu", "4 Gangen menu");
        switch (selectedClass)
        {
            case 0:
                // print the information
                informatie(menu.Gangen);
                // 2 gangen menu
                Console.WriteLine("\nVoorgerecht:");
                DisplayMenuItems(menu.Starters, username, id, includePrice: true);

                Console.WriteLine("\nHoofdgerecht:");
                DisplayMenuItems(menu.Mains, username, id, includePrice: true);

                Console.WriteLine("\nWijn:");
                DisplayMenuItems(menu.Wijn, username, id, includePrice: false);

                Console.WriteLine("\nDrinks:");
                DisplayMenuItems(menu.Drinks, username, id, includePrice: true);
                break;
            case 1:
                // print the information
                informatie(menu.Gangen);
                // 3 gangen menu
                Console.WriteLine("\nVoorgerecht:");
                DisplayMenuItems(menu.Starters, username, id, includePrice: true);

                Console.WriteLine("\nHoofdgerecht:");
                DisplayMenuItems(menu.Mains, username, id, includePrice: true);

                Console.WriteLine("\nNagerecht:");
                DisplayMenuItems(menu.Desserts, username, id, includePrice: true);

                Console.WriteLine("\nWijn:");
                DisplayMenuItems(menu.Wijn, username, id, includePrice: false);

                Console.WriteLine("\nDrinks:");
                DisplayMenuItems(menu.Drinks, username, id, includePrice: true);
                break;
            default:
                // print the information
                informatie(menu.Gangen);
                // 4 gangen menu
                Console.WriteLine("\nVoorgerecht:");
                DisplayMenuItems(menu.Starters, username, id, includePrice: true);

                Console.WriteLine("\nSoepen:");
                DisplayMenuItems(menu.Soups, username, id, includePrice: true);

                Console.WriteLine("\nHoofdgerecht:");
                DisplayMenuItems(menu.Mains, username, id, includePrice: true);

                Console.WriteLine("\nNagerecht:");
                DisplayMenuItems(menu.Desserts, username, id, includePrice: true);

                Console.WriteLine("\nWijn:");
                DisplayMenuItems(menu.Wijn, username, id, includePrice: false);

                Console.WriteLine("\nDrinks:");
                DisplayMenuItems(menu.Drinks, username, id, includePrice: true);
                break;
        }
        Console.ReadKey();
    }

    private static void informatie(List<MenuItem> items)
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║            Restaurant Menu             ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        foreach (MenuItem menu in items)
        {
            if (menu.Name == "2 Gangen menu")
            {
                Console.WriteLine($"{menu.Name}: 1x voorgerecht en 1x hoofdgerecht".PadRight(80, '.') + " " + menu.Price.ToString("F2"));
            }
            else if (menu.Name == "3 Gangen menu")
            {
                Console.WriteLine($"{menu.Name}: 1x voorgerecht, 1x hoofdgerecht en 1x nagerecht".PadRight(80, '.') + " " + menu.Price.ToString("F2"));
            }
            else if (menu.Name == "4 Gangen menu")
            {
                Console.WriteLine($"{menu.Name}: 1x voorgerecht, 1x soep, 1x hoofdgerecht en 1x nagerecht".PadRight(80, '.') + " " + menu.Price.ToString("F2"));
            }
            else if (menu.Name == "Wijn arrangement")
            {
                Console.WriteLine($"{menu.Name} kost {menu.Price.ToString("F2")}");
            }
        }
    }
    private static void DisplayMenuItems(List<MenuItem> items, bool includePrice)
    {
        foreach (MenuItem item in items)
        {
            if (includePrice)
            {
                Console.WriteLine(item.Name?.PadRight(30, '.') + " " + item.Price.ToString("F2"));
            }
            else
            {
                Console.WriteLine(item.Name?.PadRight(30, '.'));
            }
        }
    }
    private static void DisplayMenuItems(List<MenuItem> items, string naam, int id, bool includePrice)
    {
        foreach (MenuItem item in items)
        {
            if (includePrice)
            {
                Console.WriteLine(item.Name?.PadRight(30, '.') + " " + item.Price.ToString("F2") + " ID: " + item.ID);
            }
            else
            {
                Console.WriteLine(item.Name?.PadRight(30, '.') + " ID: " + item.ID);
            }
        }
    }
    // // mehtod return's Gangen menu in list <Item>
    // private static List<MenuItem> GetGangenMenu(List<MenuItem> items, bool includePrice)
    // {

    // }
}

public class LocationPresentation
{
    public static void ShowLocation(Location location)
    {
        Console.WriteLine("Naam: " + location.Name);
        Console.WriteLine("Adres: " + location.Address);
        Console.WriteLine("Telefoon: " + location.Phone);
        Console.WriteLine("E-mail: " + location.Email);
        Console.WriteLine("\nOpeningstijden:");
        Console.WriteLine("zondag\t\t" + location.OpeningHours["zondag"]);
        Console.WriteLine("maandag\t\t" + location.OpeningHours["maandag"]);
        Console.WriteLine("dinsdag\t\t" + location.OpeningHours["dinsdag"]);
        Console.WriteLine("woensdag\t" + location.OpeningHours["woensdag"]);
        Console.WriteLine("donderdag\t" + location.OpeningHours["donderdag"]);
        Console.WriteLine("vrijdag\t\t" + location.OpeningHours["vrijdag"]);
        Console.WriteLine("zaterdag\t" + location.OpeningHours["zaterdag"]);
    }
}



