using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace Project_B.Logic;
public class Mederwerker
{
    private const string ACCOUNT_FILE = @"DataSources/accounts.json";
    public static void Toevoeg_Mederwerker_Menu(string username, int id)
    {
        // Mederwerker menu
        Console.WriteLine("[1] Persoon toevoegen");
        Console.WriteLine("[T] Terug gaan");
        string input = Console.ReadLine();
        switch (input.ToUpper())
        {
            case "1":
                // persoon toevoegen
                // ga eerst naar functie die vraagt voor gegevens
                Mederwerker_gegevens(username, id);
                break;
            case "T":
                Menu.Admin_menu(username, id);// zodat je terug gaat
                // code block
                break;
            default:
                Console.WriteLine("Ongeldige optie. Kies opnieuw.");
                Toevoeg_Mederwerker_Menu(username, id);
                break;
        }
    }
    public static void Mederwerker_gegevens(string username, int id)
    {
        AccountsLogic accountsLogic = new AccountsLogic();
        Console.WriteLine("Voer de gegevens van de medewerker in:");
        Console.Write("E-mailadres: ");
        string emailAddress = Console.ReadLine();

        Console.Write("Wachtwoord: ");
        string password = Console.ReadLine();

        Console.Write("Volledige naam: ");
        string fullName = Console.ReadLine();

        // create new employee object
        AccountModel newEmployee = new AccountModel(accountsLogic.GetLastID() + 1, emailAddress, password, fullName, 2);

        // add new employee to accounts
        accountsLogic.UpdateList(newEmployee);

        Console.WriteLine("Nieuwe medewerker is toegevoegd.");
        Menu.Admin_menu(username, id); // zodat je terug gaat
    }



}