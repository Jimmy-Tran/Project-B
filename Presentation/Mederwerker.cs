using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace Project_B.Logic;
using System.Text.RegularExpressions;
public class Mederwerker
{
    private const string ACCOUNT_FILE = @"DataSources/accounts.json";
    public static void Toevoeg_Mederwerker_Menu(string username, int id)
    {
        // Mederwerker menu
        Console.WriteLine("[1] Persoon toevoegen");
        Console.WriteLine("[T] Terug gaan");
        string? input = Console.ReadLine();
        if (input != null)
        {
            switch (input.ToUpper())
            {
                case "1":
                    // persoon toevoegen
                    // ga eerst naar functie die vraagt voor gegevens
                    Mederwerker_gegevens(username, id);
                    break;
                case "T":
                    ManagerMenu.Admin_menu(username, id);// zodat je terug gaat
                                                         // code block
                    break;
                default:
                    Console.WriteLine("Ongeldige optie. Kies opnieuw.");
                    Toevoeg_Mederwerker_Menu(username, id);
                    break;
            }
        }
    }
    public static void Mederwerker_gegevens(string username, int id)
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        Console.WriteLine("Voer de gegevens van de medewerker in:");
        string emailAddress;
        string password;
        string fullName;
        do
        {
            Console.WriteLine("Graag hier de email invullen:");
            emailAddress = Console.ReadLine()!.ToLower();
            if (!Regex.IsMatch(emailAddress, @"^[^@\s]+@[^@\s]+.[^@\s]+$") && emailAddress != "1")
            {
                Console.WriteLine("De email heeft niet de juiste syntax, probeer het opnieuw");
            }
        } while (!Regex.IsMatch(emailAddress, @"^[^@\s]+@[^@\s]+.[^@\s]+$") && emailAddress != "1");

        do
        {
            Console.Write("Wachtwoord: ");
            password = Console.ReadLine()!;
        } while (string.IsNullOrWhiteSpace(password));

        do
        {
            Console.Write("Volledige naam: ");
            fullName = Console.ReadLine()!;
        } while (string.IsNullOrWhiteSpace(fullName));

        // create new employee object
        AccountModel newEmployee = new AccountModel(
      accountsLogic.GetLastID() + 1,
      emailAddress ?? string.Empty,
      password ?? string.Empty,
      fullName ?? string.Empty,
      2);

        // add new employee to accounts
        accountsLogic.UpdateList(newEmployee);

        Console.WriteLine("Nieuwe medewerker is toegevoegd.");
        ManagerMenu.Admin_menu(username, id); // zodat je terug gaat
    }



}