namespace Project_B.Logic;

static class Menu
{
  // start menu, niet ingelogd.
  // maak de locatie aan
  static Location CreateLocation() // maak de locatie aan
  {
    return new Location("Restaurant XYZ", "Main Street 123", "123456789", "info@restaurantxyz.com", new Dictionary<string, string>
        {
            { "zondag", "12:00–17:00" },
            { "maandag", "12:00–18:00" },
            { "dinsdag", "10:00–18:00" },
            { "woensdag", "10:00–18:00" },
            { "donderdag", "10:00–20:00" },
            { "vrijdag", "10:00–18:00" },
            { "zaterdag", "10:00–18:00" }
        });
  }
  static public void Start()
  {
    Console.Clear();
    int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, "Login", "Menu Kaart", "Reserveren", "Locatie", 
    "Registreren", "Stoppen");
    
    if (selectedClass == 1)
    {
      Show.Menu();
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
      CreateLocation().Show();
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