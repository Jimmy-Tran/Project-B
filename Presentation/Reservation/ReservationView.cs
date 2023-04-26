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
        Console.Write("Zoeken (Naam / Email): ");
        string Searchterm = Console.ReadLine();

        if (ReservationLogic.GetReservation(Searchterm) != null) {
            string Name;
            do {
                Console.Write("Naam: ");
                Name = Console.ReadLine();
            } while (Name.Length <= 3);
            
            string Email;
            do {
                Console.Write("Email: ");
                Email = Console.ReadLine();
            } while (ValidationLogic.IsValidEmail(Email) != true);

            string Date;
            do {
                Console.Write("Date (DD-MM-JJJJ): ");
                Date = Console.ReadLine();
            } while (ValidationLogic.IsValidDate(Date) != true);

            string TimeSlot;
            do {
                Console.Write("Time (00:00): ");
                TimeSlot = Console.ReadLine();
            } while (ValidationLogic.IsValidTime(TimeSlot) != true);

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

            string Amt_People;
            do {
                Console.Write("Aantal Personen: ");
                Amt_People = Console.ReadLine();
            } while (ValidationLogic.IsNumeric(Amt_People) != true);

            // All values has been checked and ready to be changed
            bool ChangedValue = ReservationLogic.ChangeReservation(Searchterm, Name, Email, Date, TimeSlot, Tables, Convert.ToInt32(Amt_People));

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