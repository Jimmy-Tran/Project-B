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
    if (selectedClass == 0)
    {
        Restaurant restaurant = new Restaurant();
        restaurant.InitializeTables();
    }
    else if (selectedClass == 1)
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
    Start(username, id);
  }
  static public void Start(string username, int id) // ingelogd geef parameter's mee om aan te geven dat de persoon is ingelogd
  {

    int selectedClass = MenuLogic.MultipleChoice(true, "", 1, "Menu bekijken", "Reserveren", "Locatie bekijken", "Log uit");
    if (selectedClass == 0)
    {
      // voor stellen om een foto te laten up poppen van een menu kaart, anders vraag wat precies geshowed moet worden
      Show.Menu();
      Start(username, id);
    }
    else if (selectedClass == 1)
    {
      // ga naar reserveren waar je een paar optie's weer krijgt
      // Reservatie res = new Reservatie();
      // res.Res_menu(username, id);
    }
    else if (selectedClass == 2)
    {
      // start de locatie class en show detail
      CreateLocation().Show();
      Start();
    }
    else if (selectedClass == 3)
    {
      // roep welkom aan sinds je word uitgelogd
      Welkom.welkom();
      // je word gestuurd naar start aka je bent uitgelogd'
      Start();
    }

  }
  static public void Reserveren(string username, int id)
  {
    // navragen wat gepushed moet worden naar console
  }

  // admin gedeelte ------------------------------------------------------------------------- admin gedeelte
  static public void Admin(int id, string username)
  {
    Console.WriteLine($"Welkom admin : {username}");
    Admin_menu(username, id);
  }
  static public void Admin_menu(string username, int id)
  {
    Console.WriteLine("[1] Menu aanpassen");
    Console.WriteLine("[2] Reserveren");
    Console.WriteLine("[3] Locatie bekijken");
    Console.WriteLine("[L] Loguit");

    string input = Console.ReadLine();
    if (input == "1")
    {
      // Menu aanpassen (word nog lang niet aangemaakt)
      Console.WriteLine("This feature has not been made yet.");
    }
    else if (input == "2")
    {
      // Bekijk alle reserveringen
    }
    else if (input.ToLower() == "l")
    {
      // roep welkom aan sinds je word uitgelogd
      Welkom.welkom();
      // je word gestuurd naar start aka je bent uitgelogd'
      Start();
    }
    else
    {
      Console.WriteLine("Invalid input");
      Admin_menu(username, id);
    }
  }
}