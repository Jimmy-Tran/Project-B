namespace Project_B.Logic;
using System.Text;
using System.Text.RegularExpressions;
public class RestaurantInformatie
{
    public static void UpdateLocation(string username, int id)
    {
        Location? location = Location.CreateLocation();

        bool isModified = false; // Flag to track if any changes were made
        bool menu = true;
        if (location != null)
        {
            do
            {
                int selectedOption = MenuLogic.MultipleChoice(true, "",
                    1,
                    new string[] { },
                    "Save Changes",
                    "Annuleren",
                    $"Name:        {location.Name}",
                    $"Address:     {location.Address}",
                    $"Phone:       {location.Phone}",
                    $"Email:       {location.Email}",
                    $"Opening Hours:\n{GetFormattedOpeningHours(location.OpeningHours)}");

                switch (selectedOption)
                {
                    case 0:
                        // Save Changes
                        if (isModified)
                        {
                            // Save the updated location to a JSON file
                            location.SaveToJsonFile(@"DataSources/Informatie.json");
                            // ga terug naar admin menu
                            menu = false;
                        }
                        else
                        {
                            Console.WriteLine("geen veranderingen, niks om op te slaan.");
                            // ga terug naar admin menu
                            menu = false;
                        }
                        break;
                    case 1:
                        // Annuleren
                        // ga terug naar admin menu
                        menu = false;
                        break;
                    case 2:
                        // maak check if name empty ask again
                        string? newName;
                        do
                        {
                            // Name
                            Console.Write("Vul de nieuwe naam in: ");
                            newName = Console.ReadLine() ?? string.Empty;
                            location.Name = newName;
                        } while (string.IsNullOrWhiteSpace(newName));
                        isModified = true;
                        break;
                    case 3:
                        // Address
                        string? newAddress;
                        do
                        {
                            Console.Write("Vul een nieuwe adres in: ");
                            newAddress = Console.ReadLine() ?? string.Empty;
                            location.Address = newAddress;
                        } while (string.IsNullOrWhiteSpace(newAddress));
                        isModified = true;
                        break;
                    case 4:
                        string newPhone;
                        do
                        {
                            Console.Write("Vul een nieuwe telefoonnummer in: ");
                            newPhone = Console.ReadLine()?.Trim() ?? string.Empty;

                            if (!Regex.IsMatch(newPhone, @"^\(?(0\d{2})\)?[-.\s]?(\d{3})[-.\s]?(\d{2})[-.\s]?(\d{2})$") && newPhone != "1")
                            {
                                Console.WriteLine("Ongeldig telefoonnummer. Probeer het opnieuw.");
                            }
                        } while (!Regex.IsMatch(newPhone, @"^\(?(0\d{2})\)?[-.\s]?(\d{3})[-.\s]?(\d{2})[-.\s]?(\d{2})$") && newPhone != "1");
                        isModified = true;
                        break;
                    case 5:
                        // Email
                        string? newEmail;
                        do
                        {
                            Console.WriteLine("Graag hier de nieuwe email invullen:");
                            newEmail = Console.ReadLine()!.ToLower();
                            if (!Regex.IsMatch(newEmail, @"^[^@\s]+@[^@\s]+.[^@\s]+$") && newEmail != "1")
                            {
                                Console.WriteLine("De email heeft niet de juiste syntax, probeer het opnieuw");
                            }
                        } while (!Regex.IsMatch(newEmail, @"^[^@\s]+@[^@\s]+.[^@\s]+$") && newEmail != "1");
                        isModified = true;
                        break;
                    case 6:
                        // Opening Hours
                        Console.WriteLine("Vul een dag in waarvan je tijden wilt aanpassen:");
                        string? dayToEdit = Console.ReadLine();

                        if (dayToEdit != null && location.OpeningHours.ContainsKey(dayToEdit))
                        {
                            Console.Write($"Pas de openingstijden aan voor {dayToEdit}: ");
                            string? Newbegintijd;
                            do
                            {
                                Console.WriteLine("Vul de tijd in wanneer het restaurant open gaat (HH:mm): ");
                                Newbegintijd = Console.ReadLine()?.Trim() ?? string.Empty;

                                if (!Regex.IsMatch(Newbegintijd, @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$"))
                                {
                                    Console.WriteLine("Ongeldige tijd. Voer een geldige tijd in in het formaat HH:mm.");
                                }
                            } while (!Regex.IsMatch(Newbegintijd, @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$"));
                            // optioneel hoeft niet niet ingevuld behoud oude eindtijd
                            string oudetijd = location.OpeningHours[dayToEdit];
                            string[] oudegedeeltes = oudetijd.Split('-');
                            // Get the second part, which represents the end time
                            string eindtijd = oudegedeeltes[1].Trim();
                            bool doorgaan = true;
                            string? Neweindtijd;
                            do
                            {
                                Console.WriteLine($"Vul de tijd in wanneer het restaurant dicht gaat (HH:mm): (vul niks in/op enter om de oude tijd te behouden ({eindtijd}))");
                                Neweindtijd = Console.ReadLine()?.Trim();

                                if (string.IsNullOrEmpty(Neweindtijd))
                                {
                                    Neweindtijd = eindtijd; // dus je behoud de oude eindtijd
                                    doorgaan = false;
                                }
                                else if (Regex.IsMatch(Neweindtijd, @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$"))
                                {
                                    // wnr het correct is
                                    doorgaan = false;
                                }
                            } while (doorgaan);
                            // maak nu functie om te kijken of de eind tijd wel later is dan de begin tijd
                            if (IsEndTimeLater(Neweindtijd, Newbegintijd))
                            {
                                Console.WriteLine("Error: Sluitings tijd moet later zijn dan openingstijd, probeer opnieuw. (press enter)");
                                Console.ReadKey();
                                break;
                            }
                            // anders is het wel correct
                            // zet de 2 tijden samen
                            string newHours = Newbegintijd + "-" + Neweindtijd;
                            location.OpeningHours[dayToEdit] = newHours;
                            isModified = true;
                        }
                        else
                        {
                            Console.WriteLine($"Invalid day: {dayToEdit}");
                        }
                        break;
                }
            } while (menu);
        }
        // Return to the admin menu\
        ManagerMenu.Admin_menu(username, id);// zodat je terug gaat
    }

    // Helper method to format the opening hours dictionary
    private static string GetFormattedOpeningHours(Dictionary<string, string> openingHours)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var kvp in openingHours)
        {
            sb.AppendLine($"  {kvp.Key}: {kvp.Value}");
        }

        return sb.ToString();
    }
    private static bool IsEndTimeLater(string startTime, string endTime)
    {
        DateTime startDateTime = DateTime.Parse(startTime);
        DateTime endDateTime = DateTime.Parse(endTime);

        return endDateTime > startDateTime;
    }
}
