using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Project_B.DataModels;
using System.Text.RegularExpressions;

namespace Project_B.Logic
{
  public static class ValidationLogic
  {
    public static bool IsValidEmail(string email)
    {
      if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        if (!Regex.IsMatch(email, @"@[^@\s]"))
        {
          Console.WriteLine("Je hebt geen @ in je email, voeg deze toe.");
        }
        if (!Regex.IsMatch(email, @"\.[^@\s]"))
        {
          Console.WriteLine("Je hebt geen . in je email, voeg deze toe.");
        }
        if (Regex.IsMatch(email, @"\s"))
        {
          Console.WriteLine("Je email bevat een spatie, verwijder deze");
        }
        if (email.Length == 0)
        {
          Console.WriteLine("Je email moet minimaal 1 teken lang zijn");
        }
        Console.ForegroundColor = ConsoleColor.Gray;
        return false;
      }
      return true;
    }

    public static bool IsValidFullname(string name)
    {
      if (!Regex.IsMatch(name, @"^[A-Za-z]+ [A-Za-z]+$"))
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        if (Regex.IsMatch(name, "[0-9]"))
        {
          Console.WriteLine("Je hebt een nummer in je naam, verwijder deze.");
        }
        if (!Regex.IsMatch(name, @"\s"))
        {
          Console.WriteLine("Je naam bevat geen spatie(tussen je voornaam en achternaam)");
        }
        if (name.Length == 0)
        {
          Console.WriteLine("Je naam moet minimaal 1 teken lang zijn");
        }
        Console.ForegroundColor = ConsoleColor.Gray;
        return false;
      }
      return true;
    }

    public static bool IsValidDate(string date)
    {
      DateTime tempDate;
      return DateTime.TryParse(date, out tempDate);
    }

    public static bool IsValidTime(string time)
    {
      TimeSpan tempTime;
      return TimeSpan.TryParse(time, out tempTime);
    }

    public static bool IsNumeric(string number)
    {
      int tempNumber;
      return int.TryParse(number, out tempNumber);
    }

    public static bool AmtPeopleCheck(int number) {
      if (6 >= number && number >= 0) {
        return true;
      }
      return false;
    }

    public static bool IsValidPassword(string pass)
    {
      
      if (!Regex.IsMatch(pass, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$"))
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        if (!Regex.IsMatch(pass, "[0-9]"))
        {
          Console.WriteLine("Je bent een nummer vergeten in je wachtwoord.");
        }
        if (!Regex.IsMatch(pass, "[A-Z]"))
        {
          Console.WriteLine("Je bent een hoofdletter vergeten in je wachtwoord.");
        }
        if (!Regex.IsMatch(pass, "[a-z]"))
        {
          Console.WriteLine("Je bent een kleine letter vergeten in je wachtwoord.");
        }
        if (pass.Length < 6)
        {
          Console.WriteLine("Het wachtwoord moet minimaal 6 tekens lang zijn");
        }
        Console.ForegroundColor = ConsoleColor.Gray;
        return false;
      }
      return true;
    }

    public static bool CodeExists(string code) {
      string json = File.ReadAllText("./DataSources/reservations.json");
        // Deserialize the JSON into a list of Reservation objects
        List<ReservationConsole>? reservations = JsonConvert.DeserializeObject<List<ReservationConsole>>(json);
          if (reservations != null) {
            if (!reservations.Any(r => r.reservationcode == code)) {
                return true;
            }
            else {
              return false;
            }
          }

          return true;
            
    }
  }
}