using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

static class UserRegistration
{
  static private AccountsLogic accountsLogic = new AccountsLogic();
  private const int Level = 0;

  public static void Start()
  {
    Console.WriteLine("Welkom bij de regristratie pagina");
    Console.WriteLine("[1] terug naar het start menu");
    Console.WriteLine("Graag hier je email invullen");
    string Email = Console.ReadLine();
    if (IsValid(Email, @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|nl)$") is false && Email != "1")
    {
      Console.WriteLine("De email heeft niet de juiste syntax, probeer het opnieuw");
      Start();
    }
    else if (Email == "1")
    {
      Menu.Start();
    }

    Console.WriteLine("Graag hier je wachtwoord invullen");
    string Password = Console.ReadLine();
    if (IsValid(Password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$") is false)
    {
      Console.WriteLine("Het wachtwoord heeft niet de juiste syntax, probeer het opnieuw");
      Start();
    }

    Console.WriteLine("Graag hier je volledige naam invullen");
    string FullName = Console.ReadLine();

    AccountModel acc = accountsLogic.CheckRegistration(Email);
    if (acc == null)
    {
      AccountModel NewAcc = new AccountModel(accountsLogic.GetLastID() + 1, Email, Password, FullName, Level);
      accountsLogic.UpdateList(NewAcc);

      Console.WriteLine("Je gegevens zijn opgeslagen, u kunt nu inloggen met uw account");
    }
    else
    {
      Console.WriteLine("De email die u heeft ingevuld is al gekoppeld aan een account");
      Console.WriteLine("Probeer het opnieuw");
      Start();
    }

    Menu.Start();
  }

  private static bool IsValid(string formatString, string regex)
  {
    return Regex.IsMatch(formatString, regex, RegexOptions.IgnoreCase);
  }

}