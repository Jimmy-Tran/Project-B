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
        } while (ValidationLogic.IsValidEmail(emailAddress) != true && emailAddress != "1");

        do
        {
            Console.WriteLine("Graag hier de wachtwoord invullen:");
            Console.WriteLine("De juiste syntax is minimaal 6 tekens lang, 1 hoofdletter, 1 kleine letter en 1 cijfer");
            password = Console.ReadLine()!;
        } while (ValidationLogic.IsValidPassword(password) != true);

        do
        {
            Console.WriteLine("Graag hier de volledige naam invullen:");
            Console.WriteLine("De juiste syntax is minimaal 1 letter lang en bevat geen nummers");
            fullName = Console.ReadLine()!;
        } while (ValidationLogic.IsValidFullname(fullName) != true);



        AccountModel? acc = accountsLogic.CheckRegistration(emailAddress);
        if (acc == null)
        {
            // create new employee object
            AccountModel newEmployee = new AccountModel(
                accountsLogic.GetLastID() + 1,
                emailAddress ?? string.Empty,
                password ?? string.Empty,
                fullName ?? string.Empty,
                2
            );
            accountsLogic.UpdateList(newEmployee);
            // add new employee to accounts
            accountsLogic.UpdateList(newEmployee);

            Console.WriteLine("Nieuwe medewerker is toegevoegd.");
            Console.ReadKey();
            ManagerMenu.Admin_menu(username, id); // zodat je terug gaat
        }
        else
        {
            Console.WriteLine("De email die u heeft ingevuld is al gekoppeld aan een account.");
            Console.WriteLine("Probeer het opnieuw.");
            Console.ReadKey();
            ManagerMenu.Admin_menu(username, id);
        }

    }



}