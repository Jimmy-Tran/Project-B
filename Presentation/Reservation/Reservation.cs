// public class Reservatie
// {
//     // maak de menu van de reservatie systeem
//     public void Res_menu(string username, int id)
//     {
//         // aka de persoon is ingelogd
//         // vraag de persoon eerst voor hoeveel personenen hij/zij wilt reserveren
//         Console.WriteLine("Voor hoeveel personen wilt u reserveren");
//         int input = Console.ReadLine();

//     }
// }
using Newtonsoft.Json;

public class Reservation {
    public int ID {get; set;}
    public int ClientNumber {get; set;}
    public string Name {get; set;}
    public string Email {get; set;}
    public string Date {get; set;}
    public string ReservationCode {get; set;}
    public string TimeSlot {get; set;}
    public List<int> Tables {get; set;}
    public int Amt_People {get; set;}

    static public void GetReservation() {
        try {
            string jsonContent = File.ReadAllText("DataSources/reservations.json");
            List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonContent);
            
            Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-15} {4,-10} {5,-10}", "ID", "Naam", "Datum", "Tijd", "Tafels", "Aantal Personen");
            foreach (Reservation reservation in reservations) {
                Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-15} {4,-10} {5,-10}",
                    reservation.ID, reservation.Name, reservation.Date, reservation.TimeSlot, string.Join(", ", reservation.Tables), reservation.Amt_People);
            }
        }
        catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}"); 
        }
    }
}