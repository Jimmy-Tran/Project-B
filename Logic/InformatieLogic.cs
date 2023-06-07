namespace Project_B.Logic;
using System.Text;
public class RestaurantInformatie
{
    public static void UpdateLocation(string username, int id)
    {
        Location location = Location.CreateLocation();

        bool isModified = false; // Flag to track if any changes were made
        bool menu = true;
        do
        {
            int selectedOption = MenuLogic.MultipleChoice(true, "", 1, new string[] { },
            "Save Changes",
            "Annuleren",
            $"Name: {location.Name}",
            $"Address: {location.Address}",
            $"Phone: {location.Phone}",
            $"Email: {location.Email}",
            $"Opening Hours: \n{GetFormattedOpeningHours(location.OpeningHours)}");

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
                        Console.WriteLine("No changes were made. Nothing to save.");
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
                    Console.Write("Enter the new name: ");
                    string newName = Console.ReadLine();
                    location.Name = newName;
                    isModified = true;
                    break;
                case 3:
                    // Address
                    Console.Write("Enter the new address: ");
                    string newAddress = Console.ReadLine();
                    location.Address = newAddress;
                    isModified = true;
                    break;
                case 4:
                    // Phone
                    Console.Write("Enter the new phone number: ");
                    string newPhone = Console.ReadLine();
                    location.Phone = newPhone;
                    isModified = true;
                    break;
                case 5:
                    // Email
                    Console.Write("Enter the new email address: ");
                    string newEmail = Console.ReadLine();
                    location.Email = newEmail;
                    isModified = true;
                    break;
                case 6:
                    // Opening Hours
                    Dictionary<string, string> newOpeningHours = new Dictionary<string, string>();
                    foreach (var day in location.OpeningHours.Keys)
                    {
                        Console.Write($"Enter the new opening hours for {day}: ");
                        string newHours = Console.ReadLine();
                        newOpeningHours.Add(day, newHours);
                    }
                    location.OpeningHours = newOpeningHours;
                    isModified = true;
                    break;
            }
        } while (menu);
        // Return to the admin menu\
        ManagerMenu.Admin_menu(username, id);// zodat je terug gaat
    }

    // Helper method to format the opening hours dictionary
    private static string GetFormattedOpeningHours(Dictionary<string, string> openingHours)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var kvp in openingHours)
        {
            sb.AppendLine($"{kvp.Key}: {kvp.Value}");
        }

        return sb.ToString();
    }
}
