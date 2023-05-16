using Newtonsoft.Json;
using Project_B.Logic;
using Project_B.DataModels;

public class MyReservation {
    static private AccountsLogic accountsLogic = new AccountsLogic();

    static public void ReservationInfo(string _ReservationCode) {
        
        List<ReservationModel> reservations = ReservationLogic.GetReservations();

        bool Found = false;
        foreach (ReservationModel reservation in reservations) {
            if (reservation.ReservationCode == _ReservationCode) { //Search in the list if the Code excist
                Found = true; // set Found to true so it wont show NotFound
                int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] {"Reservatie Informatie:", $"Datum: {reservation.Date.ToString("dddd, dd MMMM yyyy")}", $"Tijd: {reservation.TimeSlot.ToString(@"hh\:mm")}", $"Reserverings Code: {reservation.ReservationCode}"}, "Bevestig reservering", "Annuleer reservering", "Terug");
                //Todo: Verified bool na eerste keer open switchen, opties maken [1] Bevestig reservering [2] annuleren [3] terug

                switch (selectedClass) {
                    case 0:
                        if (reservation.Verified == true) {
                            Console.WriteLine("Reservering is al bevestigd!");
                            break;
                        }
                        ConsoleKey Confirmed;
                        do {
                            Console.WriteLine($"Druk op 'ENTER' om de reservering te bevestigen... (Terug? druk op 'backspace')");
                            Confirmed = Console.ReadKey(true).Key;
                        } while (Confirmed != ConsoleKey.Enter && Confirmed != ConsoleKey.Backspace);

                        switch (Confirmed) {
                            case ConsoleKey.Enter:
                                reservation.Verified = true;

                                ReservationLogic.VerifyingReservation(reservation.ID);

                                Console.WriteLine("Bevestiging gelukt!");
                                break;
                            case ConsoleKey.Backspace:
                                Console.WriteLine("Bevestiging geannuleerd!");
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            
        }

        if (Found != true) {
            Console.WriteLine("Geen reservering gevonden, heeft u de juiste code ingevoerd?");
        }
        
    }
}