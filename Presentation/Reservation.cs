class Reservation
{

    public string name;
    public string email;

    public string month;

    public string date;

    public void Reserveren()
    {
        Console.WriteLine("ʜᴇᴇғᴛ ᴜ ᴇᴇɴ ᴀᴄᴄᴏᴜɴᴛ");
        Console.WriteLine("[1] Ja [ Work in progress ]");
        Console.WriteLine("[2] Nee");
        string input = Console.ReadLine();

        if (input == "2") {

            Console.WriteLine("Wat is uw naam?");
            string? nameInput = Console.ReadLine();
            name = nameInput;

            Console.WriteLine($"{name}, ᴡᴀᴛ ɪs ᴜᴡ ᴇᴍᴀɪʟ?");
            bool validEmail = true;
            while(validEmail is true) {
                string ?emailInput = Console.ReadLine();
                email = emailInput;
                if(emailInput.Contains("@")) {
                    validEmail = false;
                } else {
                    Console.WriteLine("Graag een email met een @ invoeren.");
                }
            }

            ReserveTable();
            
            Console.WriteLine("email: " + email);
        }



    }

    public static void ReserveTable()
{
    DateTime reservationDate;
    bool isValidDate;

    do
    {
        // Prompt user to enter a reservation date
        Console.Write("Please enter a reservation date (MM/DD/YYYY): ");

        // Attempt to parse the user's input as a DateTime
        isValidDate = DateTime.TryParse(Console.ReadLine(), out reservationDate);

        // Check if the date is valid and display an error message if it's not
        if (!isValidDate || reservationDate < DateTime.Today)
        {
            Console.WriteLine("Invalid date. Please enter a valid date (MM/DD/YYYY).");
        }
        else if (reservationDate.Day > DateTime.DaysInMonth(reservationDate.Year, reservationDate.Month))
        {
            Console.WriteLine("Invalid date. {0} does not have {1} days.", reservationDate.ToString("MMMM"), reservationDate.Day);
            isValidDate = false;
        }
    } while (!isValidDate);

    Console.WriteLine("Saved date.");
}

}