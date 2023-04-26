using Newtonsoft.Json;
using Project_B.Logic;
using Project_B.DataModels;

public class Reservation {

    static public void DisplayReservation() {
        List<ReservationModel> reservations = ReservationLogic.GetReservations();
        Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-15} {4,-15} {5,-10}", "ID", "Naam", "Datum", "Tijd", "Tafel nummers", "Aantal Pers.");
        
        foreach (ReservationModel reservation in reservations) {
            Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-15} {4,-15} {5,-10}",
                reservation.ID, reservation.Name, reservation.Date, reservation.TimeSlot, string.Join(", ", reservation.Tables), reservation.Amt_People);
        }
    }

    static public void ChangeReservation() {
        // ReservationModel reservation = ReservationLogic.GetReservation(_Searchterm);

        // Console.WriteLine(reservation);
        Console.WriteLine("Zoeken (Naam / Email): ");
        string Searchterm = Console.ReadLine();

        if (ReservationLogic.GetReservation(Searchterm) != null) {
            Console.WriteLine("Naam");
            string Name = Console.ReadLine();

            Console.WriteLine("Email");
            string Email = Console.ReadLine();

            Console.WriteLine("Date (DD-MM-JJJJ)");
            string Date = Console.ReadLine();

            Console.WriteLine("Time");
            string TimeSlot = Console.ReadLine();

            Console.WriteLine("Tafel nummers");
            List<int> Tables = new List<int> ();
            while (true) {
                string Table_number = Console.ReadLine();
                if (Table_number != "") {
                    try {
                        Tables.Add(Convert.ToInt32(Table_number));
                    } catch {
                        Console.WriteLine("Toevoegen mislukt, voer tafel nummers in!");
                    }
                } else {
                    break;
                }

            }

            Console.WriteLine("Aantal Personen");
            int Amt_People = Convert.ToInt32(Console.ReadLine());

            bool ChangedValue = ReservationLogic.ChangeReservation(Searchterm, Name, Email, Date, TimeSlot, Tables, Amt_People);

            if (ChangedValue != true) {
                Console.WriteLine("Er is iets fouts gegaan!");
            } else {
                Console.WriteLine("Gelukt!");
            }
        } 
        else {
            Console.WriteLine("Niet gevonden");
        }
    }

    public static void DeleteReservationWithID() {
        Console.WriteLine("ID");
        int ID = Convert.ToInt32(Console.ReadLine());

        ReservationModel ReservationObject = ReservationLogic.GetReservation(Convert.ToString(ID));

        if (ReservationObject == null) {
            Console.WriteLine("Reservatie niet gevonden!");
            return;
        }

        Console.WriteLine($"\nID: {ReservationObject.ID}");
        Console.WriteLine($"Name: {ReservationObject.Name}");
        Console.WriteLine($"Email: {ReservationObject.Email}");
        Console.WriteLine($"\nWeet u zeker dat deze reservering wordt verwijderd?"); 

        string DeleteObjectbool = ""; 
        do {
            Console.WriteLine($"(J/N)");
            DeleteObjectbool = Console.ReadLine().ToUpper();
        }
        while (DeleteObjectbool != "J" && DeleteObjectbool != "N");

        if (DeleteObjectbool == "J") {
            bool DeletedObject = ReservationLogic.DeleteReservation(ID);

            if (DeletedObject != true) {
                Console.WriteLine("Er is iets fouts gegaan!");
            } 
            else {
                Console.WriteLine("Gelukt!");
            }
        } else if (DeleteObjectbool == "N") {
            Console.WriteLine("Geannuleerd.");
        }
        

        
    }
}