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
        public static bool IsValidEmail(string email) {
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|nl)$")) {
                Console.WriteLine("De email heeft niet de juiste syntax, probeer het opnieuw");
                return false;

            } else return true;
        }

        public static bool IsValidDate(string date) {
            DateTime tempDate;
            return DateTime.TryParse(date, out tempDate);
        }

        public static bool IsValidTime(string time) {
            TimeSpan tempTime;
            return TimeSpan.TryParse(time, out tempTime);
        }

        public static bool IsNumeric(string number) {
            int tempNumber;
            return int.TryParse(number, out tempNumber);
        }
    }    
}