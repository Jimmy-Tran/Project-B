namespace Project_B.Logic;

static class Menu
{
    // start menu, niet ingelogd.
    static public void Start()
    {
        Console.Clear();
        int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, "Login", "Menu Kaart", "Reserveren", "Locatie",
        "Registreren", "Stoppen");
        if (selectedClass == 0)
        {
            Restaurant restaurant = new Restaurant();
            restaurant.InitializeTables();
        }
        else if (selectedClass == 1)
        {
            MenuPresentation.Menu();
            Start();
        }
        else if (selectedClass == 2)
        {
            ReservationConsole res = new ReservationConsole();
            res.Reserveren();
        }
        else if (selectedClass == 3)
        {
            // start de locatie class en show detail
            Location location = Location.CreateLocation();
            LocationPresentation.ShowLocation(location);
            Start();
        }
        else if (selectedClass == 4)
        {
            // start de registratieproces
            UserRegistration.Start();
        }
        else if (selectedClass == 5)
        {
            // Stop de programma
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }

    }
    static public void Continue(int id, string username)
    {
        // je bent ingelogd
        Console.WriteLine($"Welkom {username}, je id is: {id}");
        CustomerMenu.Start(username, id);
    }
    // admin gedeelte ------------------------------------------------------------------------- admin gedeelte
    static public void Admin(int id, string username)
    {
        Console.WriteLine($"Welkom admin : {username}");
        ManagerMenu.Admin_menu(username, id);
    }
}