using Project_B.Logic;

static class Menu
{
    // start menu, niet ingelogd.
    static public void Start()
    {
        Console.Clear();

        int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] { }, "Login", "Menu Kaart", "Reserveren", "Reservering ophalen", "Restaurant Informatie",
        "Registreren", "Stoppen");
        if (selectedClass == 0)
        {
            UserLogin.Start();
        }
        else if (selectedClass == 1)
        {
            MenuPresentation.Menu();
            Console.ReadKey();
            Start();
        }
        else if (selectedClass == 2)
        {
            ReservationConsole res = new ReservationConsole();
            res.Reserveren();
            Start();
        }
        else if (selectedClass == 3)
        {
            MyReservation.ShowReservationInfo(MyReservation.GetReservationCode());
        }
        else if (selectedClass == 4)
        {
            // start de locatie class en show detail
            Location location = Location.CreateLocation();
            LocationPresentation.ShowLocation(location);
            Console.ReadKey();
            Start();
        }
        else if (selectedClass == 5)
        {
            // start de registratieproces
            UserRegistration.Start();
        }
        else if (selectedClass == 6)
        {
            // Stop program
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }

    }
    static public void Continue(AccountModel persoon)
    {
        // je bent ingelogd
        if (persoon is Manager)
        {
            // ga naar managers menu
            ManagerMenu.Admin_menu(persoon.FullName, persoon.Id);
        }
        else if (persoon is Employee)
        {
            // ga naarmederwerkers menu
            WorkerMenu.Start(persoon.FullName, persoon.Id);
        }
        else if (persoon is Customer)
        {
            // ga naar customers menu
            CustomerMenu.Start(persoon.FullName, persoon.Id);
        }
    }
}