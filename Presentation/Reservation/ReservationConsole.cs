using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Project_B.Logic;
using Project_B.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


class ReservationConsole
{

    public int id { get; set; }
    public int clientnumber { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public DateTime date { get; set; }
    public string? reservationcode { get; set; }
    public TimeSpan timeslot { get; set; }
    public int amt_people { get; set; }
    public List<string> tables = new List<string>();

    bool reserveValid = true;
    public string? phoneNumber { get; set; }

    public void Reserveren()
    {
        Console.Clear();
        string nameCheck;
        do
        {
            Console.WriteLine("Wat is uw voornaam en achternaam?");
            nameCheck = Console.ReadLine()!;
        } while (nameCheck.Length <= 3);

        // Change class propperty to given variable after conditions are correct
        name = nameCheck;

        Console.Clear();

        string phoneCheck;
        string phonePattern = @"^(?:\+31|0)6\d{8}$";

        do
        {
            Console.WriteLine("(Optioneel) Wat is uw telefoon nummer? (+31612345678 or 0612345678)");
            phoneCheck = Console.ReadLine()!;

            if (!Regex.IsMatch(phoneCheck, phonePattern))
            {
                Console.WriteLine("Dit is geen valide telefoon nummer!");
            }

        } while (!Regex.IsMatch(phoneCheck, phonePattern) && phoneCheck == null);

        Console.Clear();

        string emailCheck;
        do
        {
            Console.WriteLine("Wat is uw email adres? (bijv. John.Doe@gmail.com)");
            emailCheck = Console.ReadLine()!.ToLower();
        } while (ValidationLogic.IsValidEmail(emailCheck) != true); // Will return an error message if not correct.

        email = emailCheck;

        Console.Clear();
        int amountPeopleCheck;
        bool isValidAmount = false;

        do
        {
            Console.WriteLine("Hoeveel mensen zullen er zijn inclusief u?");
            string input = Console.ReadLine()!;

            if (int.TryParse(input, out amountPeopleCheck) && amountPeopleCheck > 0)
            {
                isValidAmount = true;
            }
            else
            {
                Console.WriteLine("Ongeldige invoer. Voer een geldig aantal mensen in.");
            }
        } while (!isValidAmount);


        amt_people = amountPeopleCheck;

        if (amountPeopleCheck > 6)
        { // Check if the person has more than 6 which should not be allowed. We give the user the option to continue or cancel the reservation
            int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] { "We zien dat u meer dan 6 personen heeft. Wilt u nogsteeds verder gaan met de reservering?", "Klik dan op \"Verder gaan\" en verander de hoeveelheid personen naar 6 of lager! Daarna heeft u de mogelijkheid om nog een reservering te maken met de overige personen.", "U kunt ook met ons contact opnemen via telefoon 063828192" }, "Reservering annuleren", "Verder gaan");

            switch (selectedClass)
            {
                case 0:
                    return; // Cancel the reservation
                case 1:
                    do
                    {
                        Console.WriteLine("Hoeveel mensen zullen er zijn inclusief u?");
                        amountPeopleCheck = Convert.ToInt32(Console.ReadLine());
                    } while (ValidationLogic.AmtPeopleCheck(amountPeopleCheck) != true); // Let the user pick the amount of people again with the validation of 6 or under.
                    amt_people = amountPeopleCheck;
                    break;
                default:
                    return;
            }
        }

        // je weet nu hoeveel mensen er zullen komen voeg (maruf) functie's toe om te weten hoeveel het zal kosten
        Prijs geld = new Prijs();
        decimal money = geld.prijs(amt_people);

        Console.WriteLine($"\nIn totaal betaal je voor {amt_people} mensen {money} euro.");
        Console.WriteLine("Druk op iets om verder te gaan...");
        Console.ReadKey();

        bool field4Valid = false;
        while (field4Valid is false)
        {
            Console.Clear();
            string DateCheck;
            do
            {
                Console.WriteLine("Welke datum wilt u reserveren? (DD-MM-JJJJ):");
                DateCheck = Console.ReadLine()!;
            } while (ValidationLogic.IsValidDate(DateCheck) != true);

            if (DateTime.Parse(DateCheck) < DateTime.Today)
            {
                Console.WriteLine("Kies een datum in de toekomst!");
            }

            else
            {
                field4Valid = true;
                // Console.WriteLine("The input is a valid date.");

                date = DateTime.Parse(DateCheck);

                string TimeSlotCheck = "";
                do
                {
                    int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] { $"{date.ToString("dddd, dd MMMM yyyy")}", "Selecteer een tijdslot:" }, "16:00 - 18:00", "18:00 - 20:00", "20:00 - 22:00");

                    switch (selectedClass)
                    {
                        case 0:
                            TimeSlotCheck = "16:00";
                            break;
                        case 1:
                            TimeSlotCheck = "18:00";
                            break;
                        case 2:
                            TimeSlotCheck = "20:00";
                            break;
                    }
                } while (ValidationLogic.IsValidTime(TimeSlotCheck) != true);
                timeslot = TimeSpan.Parse(TimeSlotCheck);
            }
        }

        Console.Clear();
        Console.WriteLine(date.ToString("dddd, dd MMMM yyyy"));
        Console.WriteLine(timeslot.ToString(@"hh\:mm"));
        if (TableLogic.CheckTables(date, timeslot, amt_people).Count < 1)
        {
            Console.WriteLine($"Er is geen plek meer voor vandaag probeer een andere dag");
            Console.ReadKey();
            Menu.Start();
        }
        ReservationLogic.ShowTablesAvailability(date, timeslot, amt_people);
        Console.WriteLine("email: " + email);

        string text;
        do
        {
            text = ReservationLogic.CodeGenerator();
        } while (ValidationLogic.CodeExists(text) != true);

        reservationcode = text;


        string tableCheck;
        do
        {
            Console.WriteLine("Welke tafel wilt u? (bijv. 4E of 2)");
            tableCheck = Console.ReadLine()!;
        } while (!TableLogic.CheckTables(date, timeslot, amt_people).Contains($"_{tableCheck.ToUpper()}"));

        id = ReservationLogic.GetLastID() + 1;

        if (TableLogic.TableChecker($"_{tableCheck.ToUpper()}"))
        {
            tables.Add(tableCheck);
            try
            {
                ReservationLogic.AddReservation(id, 0, name, email, date, reservationcode, timeslot, tables, amt_people);
                Console.WriteLine("Gelukt!");
                EmailFunction.sendmail(email, name, reservationcode, date, timeslot);
                EmailFunction.warning();
                // terug naar de menu uitgelogd
                Menu.Start();
            }
            catch
            {
                Console.WriteLine("Niet Gelukt, systeem fout!");
                return;
            }

        }
    }



    public void Reserveren(int client_id, string username)
    {
        Console.Clear();

        clientnumber = client_id;
        name = username;
        AccountsLogic AccountData = new AccountsLogic();
        AccountModel? AccountResult = AccountData.GetById(client_id);

        email = AccountResult?.EmailAddress;

        int amountPeopleCheck;
        bool isValidAmount = false;

        do
        {
            Console.WriteLine("Hoeveel mensen zullen er zijn inclusief u?");
            string input = Console.ReadLine()!;

            if (int.TryParse(input, out amountPeopleCheck) && amountPeopleCheck > 0)
            {
                isValidAmount = true;
            }
            else
            {
                Console.WriteLine("Ongeldige invoer. Voer een geldig aantal mensen in.");
            }
        } while (!isValidAmount);

        amt_people = amountPeopleCheck;

        Console.Clear();

        if (amountPeopleCheck > 6)
        {
            int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] { "We zien dat u meer dan 6 personen heeft. Wilt u nogsteeds verder gaan met de reservering?", "Klik dan op \"Verder gaan\" en verander de hoeveelheid personen naar 6 of lager! Daarna heeft u de mogelijkheid om nog een reservering te maken met de overige personen.", "U kunt ook met ons contact opnemen via telefoon 063828192" }, "Reservering annuleren", "Verder gaan");

            switch (selectedClass)
            {
                case 0:
                    return;
                case 1:
                    do
                    {
                        Console.WriteLine("Hoeveel mensen zullen er zijn inclusief u?");
                        amountPeopleCheck = Convert.ToInt32(Console.ReadLine());
                    } while (ValidationLogic.AmtPeopleCheck(amountPeopleCheck) != true);

                    amt_people = amountPeopleCheck;
                    break;
                default:
                    return;
            }
        }


        // je weet nu hoeveel mensen er zullen komen voeg (maruf) functie's toe om te weten hoeveel het zal kosten
        Prijs geld = new Prijs();
        decimal money = geld.prijs(amt_people);
        Console.WriteLine($"intotaal betaal je voor {amt_people} mensen {money:f2} euro.");
        Console.WriteLine("Druk op iets om verder te gaan...");
        Console.ReadKey();

        bool field4Valid = false;
        while (field4Valid is false)
        {
            Console.Clear();
            string DateCheck;
            do
            {
                Console.WriteLine("Welke datum wilt u reserveren? (DD-MM-JJJJ):");
                DateCheck = Console.ReadLine()!;
            } while (ValidationLogic.IsValidDate(DateCheck) != true);

            if (DateTime.Parse(DateCheck) < DateTime.Today)
            {
                Console.WriteLine("Kies een datum in de toekomst!");
            }

            else
            {
                field4Valid = true;
                // Console.WriteLine("The input is a valid date.");

                date = DateTime.Parse(DateCheck);

                string TimeSlotCheck = "";
                do
                {
                    int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] { $"{date.ToString("dddd, dd MMMM yyyy")}", "Selecteer een tijdslot:" }, "16:00 - 18:00", "18:00 - 20:00", "20:00 - 22:00");

                    switch (selectedClass)
                    {
                        case 0:
                            TimeSlotCheck = "16:00";
                            break;
                        case 1:
                            TimeSlotCheck = "18:00";
                            break;
                        case 2:
                            TimeSlotCheck = "20:00";
                            break;
                    }
                } while (ValidationLogic.IsValidTime(TimeSlotCheck) != true);
                timeslot = TimeSpan.Parse(TimeSlotCheck);
            }
        }

        Console.Clear();
        Console.WriteLine(date.ToString("dddd, dd MMMM yyyy"));
        Console.WriteLine(timeslot.ToString(@"hh\:mm"));
        if (TableLogic.CheckTables(date, timeslot, amt_people).Count < 1)
        {
            Console.WriteLine($"Er is geen plek meer voor vandaag probeer een andere dag");
            Console.ReadKey();
            CustomerMenu.Start(username, client_id);
        }
        ReservationLogic.ShowTablesAvailability(date, timeslot, amt_people);
        Console.WriteLine("email: " + email);

        string text;
        do
        {
            text = ReservationLogic.CodeGenerator();
        } while (ValidationLogic.CodeExists(text) != true);

        reservationcode = text;

        string tableCheck;
        do
        {
            Console.WriteLine("Welke tafel wilt u? (bijv. 4E of 2)");
            tableCheck = Console.ReadLine()!;
        } while (!TableLogic.CheckTables(date, timeslot, amt_people).Contains($"_{tableCheck.ToUpper()}"));

        id = ReservationLogic.GetLastID() + 1;

        if (TableLogic.TableChecker($"_{tableCheck.ToUpper()}"))
        {
            tables.Add(tableCheck);
            try
            {
                ReservationLogic.AddReservation(id, clientnumber, name, email!, date, reservationcode, timeslot, tables, amt_people);
                Console.WriteLine("Gelukt!");
                EmailFunction.sendmail(email!, name, reservationcode, date, timeslot);
                EmailFunction.warning();
                // console read en dan terug naar de ingelogde scherm
                Console.WriteLine("druk op een toets om terug te gaan naar de menu");
                Console.ReadKey();
                CustomerMenu.Start(username, client_id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

        }
    }
}
