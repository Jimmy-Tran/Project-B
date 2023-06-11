namespace Project_B.Logic;
using System.Text;
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
                        // Name
                        Console.Write("Vul een nieuwe naam in: ");
                        string? newName = Console.ReadLine() ?? string.Empty;
                        location.Name = newName;
                        isModified = true;
                        break;
                    case 3:
                        // Address
                        Console.Write("Vul een nieuwe adres in: ");
                        string? newAddress = Console.ReadLine() ?? string.Empty;
                        location.Address = newAddress;
                        isModified = true;
                        break;
                    case 4:
                        // Phone
                        Console.Write("Vul een nieuwe telefoon nummer in: ");
                        string? newPhone = Console.ReadLine() ?? string.Empty;
                        location.Phone = newPhone;
                        isModified = true;
                        break;
                    case 5:
                        // Email
                        Console.Write("Vul een nieuwe email adress in: ");
                        string? newEmail = Console.ReadLine();
                        location.Email = newEmail ?? string.Empty;
                        isModified = true;
                        break;
                    case 6:
                        // Opening Hours
                        Console.WriteLine("Vul een dag in waarvan je tijden wilt aanpassen:");
                        string? dayToEdit = Console.ReadLine();

                        if (dayToEdit != null && location.OpeningHours.ContainsKey(dayToEdit))
                        {
                            Console.Write($"Pas de openingstijden aan voor {dayToEdit}: ");
                            string? newHours = Console.ReadLine() ?? string.Empty;
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
}
