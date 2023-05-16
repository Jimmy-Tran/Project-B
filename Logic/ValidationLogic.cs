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
      if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+.[^@\s]+$"))
      {
        Console.WriteLine("De email heeft niet de juiste syntax, probeer het opnieuw");
        return false;

      }
      else return true;
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

    public static bool IsValidPassword(string pass)
    {
      if (!Regex.IsMatch(pass, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$"))
      {
        if (!Regex.IsMatch(pass, "[0-9]"))
        {
          Console.WriteLine("Je bent een nummer vergeten in je wachtwoord.");
        }
        if (!Regex.IsMatch(pass, "[A-Z]"))
        {
          Console.WriteLine("Je bent een hoofdletter vergeten in je wachtwoord.");
        }
        if (pass.Length < 6)
        {
          Console.WriteLine("Het wachtwoord moet minimaal 6 tekens lang zijn");
        }
        return false;
      }
      return true;
    }
  }
}