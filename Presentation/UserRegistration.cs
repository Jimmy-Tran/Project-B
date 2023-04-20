using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

static class UserRegistration
{
  static private AccountsLogic accountsLogic = new AccountsLogic();
  private const int Level = 0;

  public static void Start()
  {
    Console.WriteLine("Welkom bij de registratiepagina");
    Console.WriteLine("[1] terug naar het startmenu");

    string Email;
    do
    {
      Console.WriteLine("Graag hier je email invullen:");
      Email = Console.ReadLine().ToLower();
      if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|nl)$") && Email != "1")
      {
        Console.WriteLine("De email heeft niet de juiste syntax, probeer het opnieuw");
      }
    } while (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|nl)$") && Email != "1");

    if (Email == "1")
    {
      Menu.Start();
      return;
    }

    string Password;
    do
    {
      Console.WriteLine("Graag hier je wachtwoord invullen:");
      Console.WriteLine("De juiste syntax is minimaal 6 tekens lang, 1 hoofdletter en 1 cijfer");
      Password = Console.ReadLine();

      if (!Regex.IsMatch(Password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$"))
      {
        Console.WriteLine("Het wachtwoord heeft niet de juiste syntax, probeer het opnieuw");
      }

    } while (!Regex.IsMatch(Password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$"));

    Console.WriteLine("Graag hier je volledige naam invullen:");
    string FullName = Console.ReadLine();

    AccountModel acc = accountsLogic.CheckRegistration(Email);
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