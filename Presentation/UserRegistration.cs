using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Project_B.Logic;

static class UserRegistration
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    private const int Level = 0;

    public static void Start()
    {
        Console.WriteLine("Welkom bij de registratiepagina");
        Console.WriteLine("[1] terug naar het startmenu");

        string? Email;
        do
        {
            do
            {
                Console.WriteLine("Graag hier je email invullen:");
                Email = Console.ReadLine()!.ToLower();
            } while (ValidationLogic.IsValidEmail(Email) != true && Email != "1");

            if (Email == "1")
            {
                Menu.Start();
                return;
            }
        } while (Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") != true);

        string? Password;
        do
        {
            Console.WriteLine("Graag hier je wachtwoord invullen:");
            Console.WriteLine("De juiste syntax is minimaal 6 tekens lang, 1 hoofdletter, 1 kleine letter en 1 cijfer");
            Password = Console.ReadLine()!;

        } while (ValidationLogic.IsValidPassword(Password) != true);

        string FullName;
        do
        {
            Console.WriteLine("Graag hier je volledige naam invullen:");
            Console.WriteLine("De juiste syntax is minimaal 1 letter lang en bevat geen nummers");
            FullName = Console.ReadLine()!;
        } while (ValidationLogic.IsValidFullname(FullName) != true);

        AccountModel? acc = accountsLogic.CheckRegistration(Email);
        if (acc == null)
        {
            AccountModel newAcc = new AccountModel(accountsLogic.GetLastID() + 1, Email, Password, FullName, Level);
            accountsLogic.UpdateList(newAcc);

            Console.WriteLine("Je gegevens zijn opgeslagen, u kunt nu inloggen met uw account.");
        }
        else
        {
            Console.WriteLine("De email die u heeft ingevuld is al gekoppeld aan een account.");
            Console.WriteLine("Probeer het opnieuw.");
            Start();
        }

        Menu.Start();
    }

}