using Newtonsoft.Json;
using Project_B.Logic;
using Project_B.DataModels;
using System.Threading;
public class MyReservation
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static string GetReservationCode()
    {
        string ReservationCode;
        do
        {
            Console.WriteLine($"Vul uw reserveringscode in. (6 Karakters)");
            ReservationCode = Console.ReadLine()!;
        } while (ReservationCode.Length != 6);
        return ReservationCode;
    }

    public static string GetReservationCode(string username, int id)
    {
        try
        {
            //Try to find the reservation code by the id of the user. When it does not excist it will return a empty string
            List<ReservationModel> reservations = ReservationLogic.GetReservations(id);
            string ReservationCode = "";

            if (reservations.Count > 1)
            {
                List<string> r_choices = new List<string> { };
                foreach (ReservationModel reservation in reservations)
                {
                    r_choices.Add($"[{reservation.ID}] {reservation.ToString()}");
                }

                string[] Array_r_choices = r_choices.ToArray();

                int selectedClass = MenuLogic.MultipleChoice(true, "", 1, new string[] { $"Er zijn meerdere reserveringen gevonden onder uw naam", "Kies de juiste reservering" }, Array_r_choices);

                int r_id = Convert.ToInt32(Array_r_choices[selectedClass].Split("]")[0].Remove(0, 1));
                Console.WriteLine(r_id);
                ReservationModel? foundReservation = reservations.Find(x => x.ID == r_id);
                ReservationCode = foundReservation?.ReservationCode ?? "";
            }
            else
            {
                ReservationModel? foundReservation = reservations.Find(x => x.ClientNumber == id);
                ReservationCode = foundReservation?.ReservationCode ?? "";
            }
            Console.WriteLine(ReservationCode);
            return ReservationCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "";
        }

    }

    static public void ShowReservationInfo(string _ReservationCode)
    {

        List<ReservationModel> reservations = ReservationLogic.GetReservations();

        bool Found = false;
        foreach (ReservationModel reservation in reservations)
        {
            if (reservation.ReservationCode == _ReservationCode)
            { //Search in the list if the Code excist
                Found = true; // set Found to true so it wont show NotFound

                int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] { "Reservatie Informatie:", $"Datum: {reservation.Date.ToString("dddd, dd MMMM yyyy")}", $"Tijd: {reservation.TimeSlot.ToString(@"hh\:mm")}", $"Reserverings Code: {reservation.ReservationCode}" }, "Bevestig reservering", "Annuleer reservering", "Terug");

                switch (selectedClass)
                {
                    case 0:
                        if (reservation.Verified == true)
                        { //Check if the field is true
                            Console.WriteLine("Reservering is al bevestigd! \nDruk op iets om door te gaan...");
                            Console.ReadKey();

                            ShowReservationInfo(_ReservationCode);
                            break;
                        }
                        ConsoleKey Confirmed;
                        do
                        {
                            Console.WriteLine($"Druk op 'ENTER' om de reservering te bevestigen... (Terug? druk op 'backspace')"); //Let the user press ENTER to confirm the reservation or backspace to cancel
                            Confirmed = Console.ReadKey(true).Key;
                        } while (Confirmed != ConsoleKey.Enter && Confirmed != ConsoleKey.Backspace);

                        switch (Confirmed)
                        {
                            case ConsoleKey.Enter:
                                reservation.Verified = true;

                                ReservationLogic.VerifyingReservation(reservation.ID);
                                Console.WriteLine("Bevestiging gelukt! \nDruk op iets om door te gaan...");
                                Console.ReadKey();

                                ShowReservationInfo(_ReservationCode);
                                Menu.Start();
                                break;
                            case ConsoleKey.Backspace:
                                Console.WriteLine("Bevestiging geannuleerd! \nDruk op iets om door te gaan...");
                                Console.ReadKey();

                                ShowReservationInfo(_ReservationCode);
                                Menu.Start();
                                break;
                        }
                        break;
                    case 1:

                        do
                        {
                            Console.WriteLine($"Weet u zeker dat de reservering geannuleerd wordt? \nDruk op 'ENTER' om de reservering te annuleren... (Terug? druk op 'backspace')");
                            Confirmed = Console.ReadKey(true).Key;
                        } while (Confirmed != ConsoleKey.Enter && Confirmed != ConsoleKey.Backspace);

                        switch (Confirmed)
                        {
                            case ConsoleKey.Enter:
                                try
                                {
                                    ReservationLogic.DeleteReservation(reservation.ID);
                                    Console.WriteLine("Annuleren gelukt! \nDruk op iets om door te gaan...");
                                    Console.ReadKey();

                                    Menu.Start();
                                }
                                catch
                                {

                                }
                                break;
                            case ConsoleKey.Backspace:
                                ShowReservationInfo(_ReservationCode);
                                Menu.Start();
                                break;
                        }

                        break;
                    case 2:
                        Menu.Start();
                        break;
                    default:
                        Menu.Start();
                        break;
                }
            }
        }

        if (Found != true)
        {
            Console.WriteLine("\nGeen reservering gevonden, heeft u de juiste code ingevoerd?\nDruk op iets om door te gaan...");
            Console.ReadKey();
            Menu.Start();

        }

    }

    //Overloading the functions for logged in users
    static public void ShowReservationInfo(string username, int id, string _ReservationCode)
    {

        List<ReservationModel> reservations = ReservationLogic.GetReservations();

        bool Found = false;
        foreach (ReservationModel reservation in reservations)
        {
            if (reservation.ReservationCode == _ReservationCode)
            { //Search in the list if the Code excist
                Found = true; // set Found to true so it wont show NotFound

                int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] { "Reservatie Informatie:", $"Datum: {reservation.Date.ToString("dddd, dd MMMM yyyy")}", $"Tijd: {reservation.TimeSlot.ToString(@"hh\:mm")}", $"Reserverings Code: {reservation.ReservationCode}" }, "Bevestig reservering", "Annuleer reservering", "Terug");

                switch (selectedClass)
                {
                    case 0:
                        if (reservation.Verified == true)
                        { //Check if the field is true
                            Console.WriteLine("Reservering is al bevestigd! \nDruk op iets om door te gaan...");
                            Console.ReadKey();

                            ShowReservationInfo(username, id, _ReservationCode);
                            break;
                        }
                        ConsoleKey Confirmed;
                        do
                        {
                            Console.WriteLine($"Druk op 'ENTER' om de reservering te bevestigen... (Terug? druk op 'backspace')"); //Let the user press ENTER to confirm the reservation or backspace to cancel
                            Confirmed = Console.ReadKey(true).Key;
                        } while (Confirmed != ConsoleKey.Enter && Confirmed != ConsoleKey.Backspace);

                        switch (Confirmed)
                        {
                            case ConsoleKey.Enter:
                                reservation.Verified = true;

                                ReservationLogic.VerifyingReservation(reservation.ID);
                                Console.WriteLine("Bevestiging gelukt! \nDruk op iets om door te gaan...");
                                Console.ReadKey();

                                ShowReservationInfo(username, id, _ReservationCode);
                                CustomerMenu.Start(username, id);
                                break;
                            case ConsoleKey.Backspace:
                                Console.WriteLine("Bevestiging geannuleerd! \nDruk op iets om door te gaan...");
                                Console.ReadKey();

                                ShowReservationInfo(username, id, _ReservationCode);
                                CustomerMenu.Start(username, id);
                                break;
                        }
                        break;
                    case 1:

                        do
                        {
                            Console.WriteLine($"Weet u zeker dat de reservering geannuleerd wordt? \nDruk op 'ENTER' om de reservering te annuleren... (Terug? druk op 'backspace')");
                            Confirmed = Console.ReadKey(true).Key;
                        } while (Confirmed != ConsoleKey.Enter && Confirmed != ConsoleKey.Backspace);

                        switch (Confirmed)
                        {
                            case ConsoleKey.Enter:
                                try
                                {
                                    ReservationLogic.DeleteReservation(reservation.ID);
                                    Console.WriteLine("Annuleren gelukt! \nDruk op iets om door te gaan...");
                                    Console.ReadKey();

                                    CustomerMenu.Start(username, id);
                                }
                                catch
                                {

                                }
                                break;
                            case ConsoleKey.Backspace:
                                ShowReservationInfo(username, id, _ReservationCode);
                                CustomerMenu.Start(username, id);
                                break;
                        }

                        break;
                    case 2:
                        CustomerMenu.Start(username, id);
                        break;
                    default:
                        CustomerMenu.Start(username, id);
                        break;
                }
            }
        }

        if (Found != true)
        {
            Console.WriteLine("\nGeen reservering gevonden\nDruk op iets om door te gaan...");
            Console.ReadKey();
            CustomerMenu.Start(username, id);

        }

    }
}