using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Project_B.Logic;
class ReservationConsole
{

    public int id { get; set; }
    public int clientnumber { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public string? date { get; set; }
    public string? reservationcode { get; set; }
    public string? timeslot { get; set; }
    public List<int>? tables { get; set; }
    public int amt_people { get; set; }

    static string unavailable = "\u001b[31m";
    static string available = "\u001b[32m";
    static string cyan = "\u001b[36m";

    static string A6 = available;
    static string B6 = available;
    static string A4 = available;
    static string B4 = available;
    static string C4 = available;
    static string D4 = available;
    static string E4 = available;
    static string A = available;
    static string B = available;
    static string C = available;
    static string D = available;
    static string E = available;
    static string F = available;
    static string G = available;
    static string H = available;

    string[] timeslot1 = new string[] { "A6", "B6", "A4", "B4", "C4", "D4", "E4", "A", "B", "C", "D", "E", "F", "G", "H" };
    string[] timeslot2 = new string[] { "A6", "B6", "A4", "B4", "C4", "D4", "E4", "A", "B", "C", "D", "E", "F", "G", "H" };
    string[] timeslot3 = new string[] { "A6", "B6", "A4", "B4", "C4", "D4", "E4", "A", "B", "C", "D", "E", "F", "G", "H" };

    bool reserveValid = true;



    public void Reserveren()
    {
        bool fieldValid = false;
        while (fieldValid is false)
        {
            Console.WriteLine("[1] Reservering maken met een account.");
            Console.WriteLine("[2] Reservering maken zonder een account.");
            string option = Console.ReadLine();
            if (option == "1" || option == "2")
            {
                fieldValid = true;
                if (option == "1")
                {

                }
                else
                {
                    bool field2Valid = false;
                    while (field2Valid is false)
                    {
                        Console.WriteLine("Wat is uw email adres? (bijv. John.Doe@gmail.com)");
                        string emailCheck = Console.ReadLine();
                        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                        if (emailCheck.Length > 4 && Regex.IsMatch(emailCheck, pattern))
                        {
                            email = emailCheck;
                            field2Valid = true;
                            bool field3Valid = false;
                            while (field3Valid is false)
                            {
                                Console.WriteLine("Hoeveel mensen zullen er zijn inclusief u?");
                                int amountpeopleCheck = Convert.ToInt32(Console.ReadLine());
                                if (amountpeopleCheck > 0)
                                {
                                    field3Valid = true;
                                    amt_people = amountpeopleCheck;
                                    bool field4Valid = false;
                                    while (field4Valid is false)
                                    {
                                        Console.WriteLine("Selecteer een tijdslot:");
                                        Console.WriteLine("[1] 16:00 - 18:00");
                                        Console.WriteLine("[2] 18:00 - 20:00");
                                        Console.WriteLine("[3] 20:00 - 22:00");
                                        int timeslotCheck = Convert.ToInt32(Console.ReadLine());
                                        if (timeslotCheck == 1)
                                        {
                                            field4Valid = true;
                                            timeslot = "16:00 - 18:00";
                                        }
                                        else if (timeslotCheck == 2)
                                        {
                                            field4Valid = true;
                                            timeslot = "18:00 - 20:00";
                                        }
                                        else if (timeslotCheck == 3)
                                        {
                                            field4Valid = true;
                                            timeslot = "20:00 - 22:00";
                                        }
                                    }
                                }
                                else if (amountpeopleCheck == 1 || amountpeopleCheck > 6)
                                {
                                    Console.WriteLine("Graag contact met ons opnemen via telefoon 063828192");
                                }
                            }
                        }
                    }
                }
            }
        }


        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║                Entrance                ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine("");
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║          ╭──────────────────╮          ║");
        Console.WriteLine("║          │     Bar Area     │          ║");
        Console.WriteLine("║          ╰──────────────────╯          ║");
        Console.WriteLine("║            ○ ○ ○ ○  ○ ○ ○ ○            ║");
        Console.WriteLine("║                                        ║");
        Console.WriteLine($"║      {A6}  ○ ○ ○  {B6}            ○ ○ ○        {cyan}║");
        Console.WriteLine($"║      {A6}╭───────╮{B6}          ╭───────╮      {cyan}║");
        Console.WriteLine($"║      {A6}│   6   │{B6}          │   6   │      {cyan}║");
        Console.WriteLine($"║      {A6}│   A   │{B6}          │   B   │      {cyan}║");
        Console.WriteLine($"║      {A6}╰───────╯{B6}          ╰───────╯      {cyan}║");
        Console.WriteLine($"║      {A6}  ○ ○ ○  {B6}            ○ ○ ○        {cyan}║");
        Console.WriteLine("║                                        ║");
        Console.WriteLine($"║{A4}   ○  ○  {B4}     ○  ○  {C4}     ○  ○  {A}    ○    {cyan}║");
        Console.WriteLine($"║{A4} ╭──────╮{B4}   ╭──────╮{C4}   ╭──────╮{A}  ╭───╮  {cyan}║");
        Console.WriteLine($"║{A4} │  4A  │{B4}   │  4B  │{C4}   │  4C  │{A}  │ 1 │  {cyan}║");
        Console.WriteLine($"║{A4} ╰──────╯{B4}   ╰──────╯{C4}   ╰──────╯{A}  ╰───╯  {cyan}║");
        Console.WriteLine($"║{A4}   ○  ○  {B4}     ○  ○  {C4}     ○  ○  {A}    ○    {cyan}║");
        Console.WriteLine("║                                        ║");
        Console.WriteLine($"║{D4}   ○  ○  {E4}    ○  ○  {B}    ○  {C}   ○  {D}   ○    {cyan}║");
        Console.WriteLine($"║{D4} ╭──────╮{E4}  ╭──────╮{B}  ╭───╮{C} ╭───╮{D} ╭───╮  {cyan}║");
        Console.WriteLine($"║{D4} │  4D  │{E4}  │  4E  │{B}  │ 2 │{C} │ 3 │{D} │ 4 │  {cyan}║");
        Console.WriteLine($"║{D4} ╰──────╯{E4}  ╰──────╯{B}  ╰───╯{C} ╰───╯{D} ╰───╯  {cyan}║");
        Console.WriteLine($"║{D4}   ○  ○  {E4}    ○  ○  {B}    ○  {C}   ○  {D}   ○    {cyan}║");
        Console.WriteLine("║                                        ║");
        Console.WriteLine($"║{E}      ○  {F}      ○  {G}      ○  {H}      ○      {cyan}║");
        Console.WriteLine($"║{E}    ╭───╮{F}    ╭───╮{G}    ╭───╮{H}    ╭───╮    {cyan}║");
        Console.WriteLine($"║{E}    │ 5 │{F}    │ 6 │{G}    │ 7 │{H}    │ 8 │    {cyan}║");
        Console.WriteLine($"║{E}    ╰───╯{F}    ╰───╯{G}    ╰───╯{H}    ╰───╯    {cyan}║");
        Console.WriteLine($"║{E}      ○  {F}      ○  {G}      ○  {H}      ○      {cyan}║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();



        Console.WriteLine("email: " + email);
        string json = File.ReadAllText("./DataSources/reservations.json");
        ;        // Deserialize the JSON into a list of Reservation objects
        List<ReservationConsole> reservations = JsonConvert.DeserializeObject<List<ReservationConsole>>(json);
        bool codeExists = true;
        while (codeExists)
        {
            string text = ReservationLogic.CodeGenerator();
            if (!reservations.Any(r => r.reservationcode == text))
            {
                reservationcode = text;
                codeExists = false;
            }
        }
        int highestId = reservations.Max(reservation => reservation.id);

        // Create a new Reservation object to add to the list
        ReservationConsole newReservation = new ReservationConsole
        {

            id = highestId + 1,
            clientnumber = 0,
            name = name,
            email = email,
            date = "0",
            reservationcode = reservationcode,
            timeslot = "",
            tables = new List<int> { 3, 4 },
            amt_people = amt_people
        };

        // Add the new Reservation object to the list
        reservations.Add(newReservation);

        // Serialize the list of Reservation objects  back into JSON format
        string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);

        // Write the updated JSON back to the file
        File.WriteAllText("./DataSources/reservations.json", updatedJson);
    }



}
