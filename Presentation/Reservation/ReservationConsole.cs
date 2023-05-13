using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Project_B.Logic;

class ReservationConsole
{

    public int id { get; set; }
    public int clientnumber { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public DateTime date { get; set; }
    public string? reservationcode { get; set; }
    public TimeSpan timeslot { get; set; }
    public int amt_people {get; set;}
    public List<string> tables = new List<string>();



    bool reserveValid = true;
    


    public int Id { get; set; }
    public int ClientNumber { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Date { get; set; }
    public string? ReservationCode { get; set; }
    public int TimeSlot { get; set; }
    public int AmountOfPeople { get; set; }
    public List<string> Tables { get; set; } = new List<string>();

    public void Reserve()
    {
        bool fieldValid = false;
        while(fieldValid is false) {
            Console.WriteLine("[1] Reservering maken met een account.");
            Console.WriteLine("[2] Reservering maken zonder een account.");
            string option = Console.ReadLine();
            
            if (option == "1" || option == "2") {
                fieldValid = true;
                if (option == "1") {
                    
                } else {
                    bool field2Valid = false;
                    while(field2Valid is false) {
                        Console.WriteLine("Wat is uw email adres? (bijv. John.Doe@gmail.com)");
                        string emailCheck = Console.ReadLine();
                        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                        if(emailCheck.Length > 4 && Regex.IsMatch(emailCheck, pattern)){
                            email = emailCheck; 
                            field2Valid = true;
                            bool field3Valid = false;
                            while(field3Valid is false) {
                                Console.WriteLine("Hoeveel mensen zullen er zijn inclusief u?");
                                int amountpeopleCheck = Convert.ToInt32(Console.ReadLine());
                                if(amountpeopleCheck > 0) {
                                    field3Valid = true;
                                    amt_people = amountpeopleCheck;
                                    bool field4Valid = false;
                                    while(field4Valid is false) {
                                        string DateCheck;
                                        do {
                                            Console.WriteLine("Welke datum wilt u reserveren? (DD-MM-JJJJ):");
                                            DateCheck = Console.ReadLine();
                                        } while (ValidationLogic.IsValidDate(DateCheck) != true);                                    
                            
                                        if (DateTime.Parse(DateCheck) < DateTime.Today) {
                                            Console.WriteLine("The date entered is before the current date.");
                                        }

                                        else {
                                            field4Valid = true;
                                            Console.WriteLine("The input is a valid date.");

                                            date = DateTime.Parse(DateCheck);

                                            string TimeSlotCheck;
                                            do {
                                                Console.WriteLine("Selecteer een tijdslot:");
                                                int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, "16:00 - 18:00", "18:00 - 20:00", "20:00 - 22:00");
                                                
                                                switch (selectedClass) {
                                                    case 0:
                                                        TimeSlotCheck = "16:00";
                                                        break;
                                                    case 1:
                                                        TimeSlotCheck = "18:00";
                                                        break;
                                                    case 2:
                                                        TimeSlotCheck = "20:00";
                                                        break;
                                                    default:
                                                        TimeSlotCheck = null;
                                                        break;
                                                }
                                            } while (ValidationLogic.IsValidTime(TimeSlotCheck) != true);
                                                timeslot = TimeSpan.Parse(TimeSlotCheck);
                                        }
                                    }
                                } else if(amountpeopleCheck > 6) {
                                    Console.WriteLine("Graag contact met ons opnemen via telefoon 063828192");
                                }
                            }
                        }
                    }
                }
            }
        }

            Console.WriteLine(date.ToString("dddd, dd MMMM yyyy"));
            Console.WriteLine(timeslot);
            ReservationLogic.ShowTablesAvailability(date, timeslot, amt_people);
            Console.WriteLine("email: " + email);
            string json = File.ReadAllText("./DataSources/reservations.json");
            // Deserialize the JSON into a list of Reservation objects
            List<ReservationConsole> reservations = JsonConvert.DeserializeObject<List<ReservationConsole>>(json);
            bool codeExists = true;
            while (codeExists)
            {
                Console.WriteLine("De ingevoerde datum is voor de huidige datum.");
            }
            else
            {
                return dateStr;
            }
        }
        Console.WriteLine("Ongeldige invoer. Voer een geldige datum in (DD/MM/YYYY).");
    }
}

private int GetValidTimeSlot()
{
    while (true)
    {
        Console.WriteLine("Selecteer een tijdslot:");
        Console.WriteLine("[1] 16:00 - 18:00");
        Console.WriteLine("[2] 18:00 - 20:00");
        Console.WriteLine("[3] 20:00 - 22:00)");

        if (int.TryParse(Console.ReadLine(), out int timeSlot) && timeSlot >= 1 && timeSlot <= 3)
        {
            return timeSlot;
        }
        Console.WriteLine("Ongeldige invoer. Voer een getal tussen 1 en 3 in.");
    }
}

private string GetValidTable()
{
    while (true)
    {
        Console.WriteLine("Welke tafel wilt u? (bijv. 4E of 2)");
        string table = Console.ReadLine()?.Trim();
        if (table?.Length == 2)
        {
            table = $"_{table.ToUpper()}";
            if (TableLogic.TableChecker(table))
            {
                return table;
            }
            Console.WriteLine("De tafel is al bezet. Kies een andere tafel.");
        }
        Console.WriteLine("Ongeldige invoer. Voer een geldige tafel in.");
    }
}

}


