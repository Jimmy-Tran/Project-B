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
    public int timeslot { get; set; }
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
        Console.WriteLine("[1] Reservering maken met een account.");
        Console.WriteLine("[2] Reservering maken zonder een account.");
        int option = GetValidOption("Kies een optie (1-2): ", new List<int> {1, 2});

        if (option == 1)
        {
            // Handle reservation with account
        }
        else
        {
            Email = GetValidEmail();
            AmountOfPeople = GetValidAmountOfPeople();
            Date = GetValidDate();
            TimeSlot = GetValidTimeSlot();
            ReservationLogic.ShowTablesAvailability(Date, TimeSlot, AmountOfPeople);
            Tables.Add(GetValidTable());
            ReservationCode = ReservationLogic.CodeGenerator();
            ReservationLogic.AddReservation(Id, ClientNumber, Name, Email, Date, ReservationCode, TimeSlot, Tables, AmountOfPeople);
        }
    }

    private int GetValidOption(string prompt, List<int> validOptions)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int option) && validOptions.Contains(option))
            {
                return option;
            }
            Console.WriteLine("Ongeldige invoer. Probeer het opnieuw.");
        }
    }

    private string GetValidEmail()
    {
        while (true)
        {
            Console.WriteLine("Wat is uw e-mailadres? (bijv. John.Doe@gmail.com)");
            string email = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                Console.WriteLine("Ongeldige invoer. Voer een geldig e-mailadres in.");
            }
            else
            {
                return email;
            }
        }
    }

    private bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    private int GetValidAmountOfPeople()
        {
        while (true)
            {
            Console.WriteLine("Hoeveel mensen zullen er zijn inclusief u?");
            if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0 && amount <= 6)
            {
                return amount;
            }
            Console.WriteLine("Ongeldige invoer. Voer een getal tussen 1 en 6 in.");
            }
        }

        private string GetValidDate()
{
    while (true)
    {
        Console.WriteLine("Welke datum wilt u reserveren? (DD/MM/YYYY)");
        string dateStr = Console.ReadLine()?.Trim();
        if (DateTime.TryParseExact(dateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
        {
            if (date.Date < DateTime.Today)
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


