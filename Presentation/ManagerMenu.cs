namespace Project_B.Logic;
public class ManagerMenu
{

    static public void Admin_menu(string username, int id)
    {
        Console.WriteLine("[1] Menu aanpassen");
        Console.WriteLine("[2] Menu bekijken");
        Console.WriteLine("[3] Mederwerker toevoegen");
        Console.WriteLine("[4] Reserveringen bekijken");
        Console.WriteLine("[5] Reservering maken");
        Console.WriteLine("[6] Reservering aanpassen");
        Console.WriteLine("[7] Reservering verwijderen");
        Console.WriteLine("[L] Loguit");

        string input = Console.ReadLine();
        if (input == "1")
        {
            // Menu aanpassen (word nog lang niet aangemaakt)
            MenuAanpassen.EditMenu(username, id);
        }
        else if (input == "2")
        {
            MenuPresentation.Menu(username, id);
            Admin_menu(username, id);
        }
        else if (input == "3")
        {
            Mederwerker.Toevoeg_Mederwerker_Menu(username, id);
        }
        else if (input == "4")
        {
            Reservation.DisplayReservation();
            Admin_menu(username, id);
        }
        else if (input == "5")
        {
            Reservation.MakeReservation();
            Admin_menu(username, id);
        }
        else if (input == "6")
        {
            Console.WriteLine("Nog niet beschikbaar");
            // Reservation.ChangeReservation();
            Admin_menu(username, id);
        }
        else if (input == "7")
        {
            Reservation.DeleteReservationWithID();
            Admin_menu(username, id);
        }
        else if (input.ToLower() == "l")
        {
            // roep welkom aan sinds je word uitgelogd
            Welkom.welkom();
            // je word gestuurd naar start aka je bent uitgelogd'
            Menu.Start();
        }
        else
        {
            Console.WriteLine("Invalid input");
            Admin_menu(username, id);
        }
    }
}