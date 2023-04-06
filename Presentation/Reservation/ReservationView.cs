using Newtonsoft.Json;
using Project_B.Logic;
using Project_B.DataModels;

public class Reservation {

    static public void DisplayReservation() {
        List<ReservationModel> reservations = ReservationLogic.GetReservation();
        Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-15} {4,-15} {5,-10}", "ID", "Naam", "Datum", "Tijd", "Tafel nummers", "Aantal Pers.");
        
        foreach (ReservationModel reservation in reservations) {
            Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-15} {4,-15} {5,-10}",
                reservation.ID, reservation.Name, reservation.Date, reservation.TimeSlot, string.Join(", ", reservation.Tables), reservation.Amt_People);
        }
    }

    static public void ChangeReservation(string _Searchterm) {
        // try {
        //     string jsonContent = File.ReadAllText("DataSources/reservations.json");
        //     List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonContent);

        //     // List<string> UpdatableFields = new() {"name", "email", "date", "timeslot", "tables", "amt_people"};

        //     foreach (Reservation reservation in reservations) {
        //         if (reservation.ID == Convert.ToInt32(_Searchterm) || reservation.Name == _Searchterm || reservation.Email == _Searchterm) {
        //             Console.WriteLine("Vul in de nieuwe waarde (Enter om over te slaan.)");
        //             Console.WriteLine($"Naam: {reservation.Name}");
        //             Console.WriteLine($"Email: {reservation.Email}");
        //             Console.WriteLine($"Date: {reservation.Date}");
        //             Console.WriteLine($"Naam: {reservation.TimeSlot}");
        //             // Console.WriteLine($"Naam: {.Join(reservation.Tables)}");
        //             Console.WriteLine($"Naam: {reservation.Amt_People}");
        //         }            
        //     }

            
        //     // string output = Newtonsoft.Json.JsonConvert.SerializeObject(reservations, Newtonsoft.Json.Formatting.Indented);
        //     // File.WriteAllText("DataSources/reservations.json", output);
        // }
        // catch (Exception ex) {
        //     Console.WriteLine($"Error: {ex.Message}"); 
        // }
    }
}