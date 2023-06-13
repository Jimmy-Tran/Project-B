namespace Project_B.Logic;
public class ManagerMenu
{

    static public void Admin_menu(string username, int id)
    {
        int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] { }, "Menu", "Reserveringen ", "Medewerkers", "Restaurant informatie", "Log uit");

        switch (selectedClass)
        {
            case 0:
                selectedClass = MenuLogic.MultipleChoice(true, "", 1, new string[] { }, "Menu aanpassen", "Menu bekijken", "Terug");

                switch (selectedClass)
                {
                    case 0:
                        MenuAanpassen.EditMenu(username, id);
                        break;
                    case 1:
                        MenuPresentation.Menu(username, id);
                        Admin_menu(username, id);
                        break;
                    case 2:
                        Admin_menu(username, id);
                        break;
                }
                break;

            case 1:
                selectedClass = MenuLogic.MultipleChoice(true, "", 1, new string[] { }, "Reservering bekijken", "Reservering maken", "Reservering aanpassen", "Reservering verwijderen", "Terug");

                switch (selectedClass)
                {
                    case 0:
                        Console.Clear();
                        Reservation.DisplayReservation();
                        Console.WriteLine("Druk op iets om verder te gaan...");
                        Console.ReadKey();
                        Admin_menu(username, id);
                        break;
                    case 1:
                        Reservation.MakeReservation();
                        Console.WriteLine("Druk op iets om verder te gaan...");
                        Console.ReadKey();
                        Admin_menu(username, id);
                        break;
                    case 2:
                        // Console.WriteLine("Nog niet beschikbaar");
                        Reservation.ChangeReservation();
                        Console.ReadKey();
                        Admin_menu(username, id);
                        break;
                    case 3:
                        Reservation.DeleteReservationWithID();
                        Admin_menu(username, id);
                        break;
                    case 4:
                        Admin_menu(username, id);
                        break;
                }
                break;

            case 2:
                selectedClass = MenuLogic.MultipleChoice(true, "", 1, new string[] { }, "Medewerkers toevoegen", "Terug");

                switch (selectedClass)
                {
                    case 0:
                        Mederwerker.Toevoeg_Mederwerker_Menu(username, id);
                        break;
                    case 1:
                        Admin_menu(username, id);
                        break;
                }
                break;
            case 3:
                selectedClass = MenuLogic.MultipleChoice(true, "", 1, new string[] { }, "Locatie bekijken", "Locatie aanpassen", "Terug");

                switch (selectedClass)
                {
                    case 0:
                        // start de locatie class en show detail
                        Location? location = Location.CreateLocation();
                        if (location != null)
                        {
                            LocationPresentation.ShowLocation(location);
                        }
                        Console.ReadKey();
                        Admin_menu(username, id);
                        break;
                    case 1:
                        RestaurantInformatie.UpdateLocation(username, id);
                        break;
                    case 2:
                        Admin_menu(username, id);
                        break;
                }
                break;
            case 4:
                // uitloggen
                Menu.Start();
                break;
            default:
                // iets ging fout
                Admin_menu(username, id);
                break;
        }
    }
}