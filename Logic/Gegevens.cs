public class Gegevens
{
    // maak een lijst met de mensen
    List<Person> personen = new List<Person>();

    public List<Person> Gegevens_krijgen(int aantal)
    {
        int i = 1;
        // maak een for each loop voor de aantal mensen die er zijn
        do
        {
            // print de menu eruit en sla het via andere method's op
            Console.WriteLine($"Vul hier de gegevens in voor persoon {i}");
            string name = GetValidNameInput("Naam: ");
            int age = GetValidIntegerInput("Age: ");
            int menuSelection = GetValidMenuSelection("Gangen Menu (2/3/4): ");

            // aka door naar de volgende en maak een person object aan en add het in de lijst
            Person persoon = new Person(name, age, menuSelection);
            personen.Add(persoon);
            i++;
        } while (i <= aantal);
        return personen;
    }

    // checks om te kijken of de gegevens kloppen
    static string GetValidNameInput(string prompt)
    {
        bool isValidInput = false;
        string input = string.Empty;

        while (!isValidInput)
        {
            input = GetInput(prompt);

            if (!string.IsNullOrWhiteSpace(input) && !ContainsNumber(input))
            {
                isValidInput = true;
            }
            else
            {
                Console.WriteLine("Ongeldige invoer. Voer een geldige naam in zonder getallen.");
            }
        }

        return input;
    }

    static bool ContainsNumber(string input)
    {
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                return true;
            }
        }

        return false;
    }
    static string GetInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
    static int GetValidIntegerInput(string prompt)
    {
        bool isValidInput = false;
        int value = 0;

        while (!isValidInput)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (int.TryParse(input, out value))
            {
                isValidInput = true;
            }
            else
            {
                Console.WriteLine("Ongeldige invoer. Voer een geheel getal in.");
            }
        }

        return value;
    }
    static int GetValidMenuSelection(string prompt)
    {
        bool isValidInput = false;
        int value = 0;

        while (!isValidInput)
        {
            value = GetValidIntegerInput(prompt);

            if (value == 2 || value == 3 || value == 4)
            {
                isValidInput = true;
            }
            else
            {
                Console.WriteLine("Ongeldige invoer. Kies een waarde van 2, 3 of 4.");
            }
        }

        return value;
    }
}