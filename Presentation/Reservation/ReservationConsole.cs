using Newtonsoft.Json;

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


    public void Reserveren()
    {
        Console.WriteLine("Heeft u een account?");
        Console.WriteLine("[1] Ja [ Nog niet beschikbaar ]");
        Console.WriteLine("[2] Nee");
        string input = Console.ReadLine();

        if (input == "2") {

            Console.WriteLine("Wat is uw naam?");
            string? nameInput = Console.ReadLine();
            name = nameInput;

            Console.WriteLine($"{name}, wat is uw email?");
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

            Console.WriteLine($"{name}, hoeveel mensen zullen er zijn?");
            bool validAmount = true;
            while (validAmount)
            {
                if (!int.TryParse(Console.ReadLine(), out int amountPeople) || amountPeople <= 0)
                {
                    Console.WriteLine("Graag een aantal groter dan 0 invullen.");
                }
                else
                {
                    amt_people = amountPeople;
                    validAmount = false;
                }
            }

            Console.WriteLine("email: " + email);
            string json = File.ReadAllText("./DataSources/reservations.json");

        // Deserialize the JSON into a list of Reservation objects
            List<ReservationConsole> reservations = JsonConvert.DeserializeObject<List<ReservationConsole>>(json);
                        bool codeExists = true;
            string newCode = "";
            while (codeExists)
            {
                CodeGenerator();
                newCode = reservationcode;
                if (!reservations.Any(r => r.reservationcode == newCode))
                {
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

    public void CodeGenerator()
    {
        // Creating object of random class
        Random rand = new Random();

        int randValue;
        string str = "";
        char letter;
        for (int i = 0; i < 6; i++)
        {
    
            // Generating a random number.
            randValue = rand.Next(0, 26);
    
            // Generating random character by converting
            // the random number into character.
            letter = Convert.ToChar(randValue + 65);
    
            // Appending the letter to string.
            str = str + letter;

            reservationcode = str;
        }
        Console.WriteLine("Random String: " + reservationcode);
    }

}