using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Project_B.DataModels;

namespace Project_B.Logic;

public class AccountData
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    static public void Start(int id) // ingelogd geef parameter's mee om aan te geven dat de persoon is ingelogd
    {
        AccountModel? acc = accountsLogic.GetById(id);
        if (acc != null)
        {

            int selectedClass = MenuLogic.MultipleChoice(true, "", 1, new string[] { }, $"Email: {acc.EmailAddress}", $"Wachtwoord: {acc.Password}", $"Volledige naam: {acc.FullName}", "Mijn reserveringen", "Terug");
            if (selectedClass == 0)
            {
                // check syntax email, Will return an error message if not correct or the email already exists.
                do
                {
                    Console.WriteLine("Vul hier uw nieuwe email.");
                    acc.EmailAddress = Console.ReadLine()!.ToLower();
                } while (ValidationLogic.IsValidEmail(acc.EmailAddress) != true && accountsLogic.CheckRegistration(acc.EmailAddress) != null);

                // updates email to json and returns to Start
                accountsLogic.UpdateList(acc);
                Start(id);
            }
            else if (selectedClass == 1)
            {
                do
                {
                    Console.WriteLine("Graag hier je wachtwoord invullen:");
                    Console.WriteLine("De juiste syntax is minimaal 6 tekens lang, 1 hoofdletter en 1 cijfer");
                    acc.Password = Console.ReadLine()!;
                } while (ValidationLogic.IsValidPassword(acc.Password) != true);

                // updates password to json and returns to Start
                accountsLogic.UpdateList(acc);
                Start(id);
            }
            else if (selectedClass == 2)
            {
                do
                {
                    Console.WriteLine("Graag hier je volledige naam invullen:");
                    acc.FullName = Console.ReadLine()!.ToLower();
                    if (!Regex.IsMatch(acc.FullName, @"^[A-Za-z\\s]+$"))
                    {
                        Console.WriteLine("De email heeft niet de juiste syntax, probeer het opnieuw");
                    }
                } while (!Regex.IsMatch(acc.FullName, @"^[A-Za-z\\s]+$"));

                // updates full name to json and returns to Start
                accountsLogic.UpdateList(acc);
                Start(id);
            }
            else if (selectedClass == 3)
            {
                var ReservationList = ReservationLogic.GetReservations(acc.EmailAddress); // filters all reservations with this email, maybe change this to clientnumber
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Reservaties Tabel van {acc.FullName}:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("--------------------");
                Console.WriteLine("{0,-25} {1,-15} {2,-10} {3,-15} {4,-15} {5, -20}", "Naam", "Datum", "Tijd", "Tafel nummers", "Aantal pers.", "Reservatie code");
                foreach (ReservationModel reservation in ReservationList)
                {
                    Console.WriteLine("{0,-25} {1,-15} {2,-10} {3,-15} {4,-15} {5, -20}",
                    reservation.Name, reservation.Date.ToString("dd-MM-yyyy"), reservation.TimeSlot.ToString(@"hh\:mm"), string.Join(", ", reservation.Tables), reservation.Amt_People, reservation.ReservationCode);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("--------------------");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("> Terug");
                Console.ForegroundColor = ConsoleColor.Gray;
                string back_button = Console.ReadLine()!;
                if (back_button != null)
                {
                    Start(id);
                }
            }

            else if (selectedClass == 4)
            {
                // je wordt terug gestuurd naar de CustomerMenu
                CustomerMenu.Start(acc.FullName, id);
            }
        }
    }

}