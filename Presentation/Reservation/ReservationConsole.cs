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

    public void Reserveren()
    {
        string nameCheck;
        do
        {
            Console.WriteLine("Wat is uw voor en achternaam?");
            nameCheck = Console.ReadLine();
        } while (nameCheck.Length <= 3);

        // Change class propperty to ClientName after conditions are correct
        name = nameCheck;

        string emailCheck;
        do
        {
            Console.WriteLine("Wat is uw email adres? (bijv. John.Doe@gmail.com)");
            emailCheck = Console.ReadLine().ToLower();
        } while (ValidationLogic.IsValidEmail(emailCheck) != true); // Will return an error message if not correct.

        email = emailCheck;


        int amountPeopleCheck;
        do
        {
            Console.WriteLine("Hoeveel mensen zullen er zijn inclusief u?");
            amountPeopleCheck = Convert.ToInt32(Console.ReadLine());
        } while (amountPeopleCheck <= 0);

        amt_people = amountPeopleCheck;
        // je weet nu hoeveel mensen er zullen komen voeg (maruf) functie's toe om te weten hoeveel het zal kosten
        Gegevens begin = new Gegevens();
        // nu heb je een lijst met gegevens van de mensen op basis van hoeveel mensen gaan geef je dat door met de int
        List<Person> gegevens = begin.Gegevens_krijgen(amt_people);
        Prijs geld = new Prijs();
        List<double> betalen = geld.Prijs_berekenen(gegevens);
        Console.WriteLine($"intotaal betaal je voor {gegevens.Count} mensen {betalen.Sum()} euro.");
        //Todo: Cancel the reservation if the Amt of people is more than 6 or something else....?

        // if (amountPeopleCheck > 6) {
        //     Console.ForegroundColor = ConsoleColor.Yellow;
        //     Console.WriteLine("We zien dat u meer dan 6 personen heeft.");

        //     Console.WriteLine("Wilt u nogsteeds een reservering maken met meer dan 6 personen? Graag met ons contact opnemen via telefoon 063828192");
        //     Console.ForegroundColor = ConsoleColor.Gray;
        // }


        bool field4Valid = false;
        while (field4Valid is false)
        {
            string DateCheck;
            do
            {
                Console.WriteLine("Welke datum wilt u reserveren? (DD-MM-JJJJ):");
                DateCheck = Console.ReadLine();
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

                string TimeSlotCheck;
                do
                {
                    Console.WriteLine("Selecteer een tijdslot:");
                    int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] {}, "16:00 - 18:00", "18:00 - 20:00", "20:00 - 22:00");
                    
                    switch (selectedClass) {
                        case 0:
                            TimeSlotCheck = "16:00";
                            break;
                        case 1:
                            TimeSlotCheck = "18:00";
                            break;
                        case 2:
                            TimeSlotCheck = "20:00";
                            break;
                        default:
                            TimeSlotCheck = null;
                            break;
                    }
                } while (ValidationLogic.IsValidTime(TimeSlotCheck) != true);
                timeslot = TimeSpan.Parse(TimeSlotCheck);
            }
        }

            Console.Clear();
            Console.WriteLine(date.ToString("dddd, dd MMMM yyyy"));
            ReservationLogic.ShowTablesAvailability(date, timeslot, amt_people);
            Console.WriteLine("email: " + email);

            string text;
            do {
                text = ReservationLogic.CodeGenerator();
            } while (ValidationLogic.CodeExists(text) != true);

            reservationcode = text;

            string tableCheck;
            do {
                Console.WriteLine("Welke tafel wilt u? (bijv. 4E of 2)");
                tableCheck = Console.ReadLine();
            } while (!TableLogic.CheckTables(date, timeslot, amt_people).Contains($"_{tableCheck.ToUpper()}"));
                
            id = ReservationLogic.GetLastID() + 1;

        if (TableLogic.TableChecker($"_{tableCheck.ToUpper()}"))
        {
            tables.Add(tableCheck);
            try
            {
                ReservationLogic.AddReservation(id, 0, name, email, date, reservationcode, timeslot, tables, amt_people);
                Console.WriteLine("Gelukt!");
                Email.sendmail(email, name, date, timeslot);
                Email.warning();
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
        clientnumber = client_id;
        name = username;
        AccountsLogic AccountData = new AccountsLogic();
        AccountModel AccountResult = AccountData.GetById(client_id);

        email = AccountResult.EmailAddress;

        int amountPeopleCheck;
        do
        {
            Console.WriteLine("Hoeveel mensen zullen er zijn inclusief u?");
            amountPeopleCheck = Convert.ToInt32(Console.ReadLine());
        } while (amountPeopleCheck <= 0);

        amt_people = amountPeopleCheck;
        // je weet nu hoeveel mensen er zullen komen voeg (maruf) functie's toe om te weten hoeveel het zal kosten
        Gegevens begin = new Gegevens();
        // nu heb je een lijst met gegevens van de mensen op basis van hoeveel mensen gaan geef je dat door met de int
        List<Person> gegevens = begin.Gegevens_krijgen(amt_people);
        Prijs geld = new Prijs();
        List<double> betalen = geld.Prijs_berekenen(gegevens);
        Console.WriteLine($"intotaal betaal je voor {gegevens.Count} mensen {betalen.Sum()} euro.");
        //Todo: Cancel the reservation if the Amt of people is more than 6 or something else....?

        // if (amountPeopleCheck > 6) {
        //     Console.ForegroundColor = ConsoleColor.Yellow;
        //     Console.WriteLine("We zien dat u meer dan 6 personen heeft.");

        //     Console.WriteLine("Wilt u nogsteeds een reservering maken met meer dan 6 personen? Graag met ons contact opnemen via telefoon 063828192");
        //     Console.ForegroundColor = ConsoleColor.Gray;
        // }


        bool field4Valid = false;
        while (field4Valid is false)
        {
            string DateCheck;
            do
            {
                Console.WriteLine("Welke datum wilt u reserveren? (DD-MM-JJJJ):");
                DateCheck = Console.ReadLine();
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

                string TimeSlotCheck;
                do
                {
                    Console.WriteLine("Selecteer een tijdslot:");
                    int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, new string[] {}, "16:00 - 18:00", "18:00 - 20:00", "20:00 - 22:00");
                    
                    switch (selectedClass) {
                        case 0:
                            TimeSlotCheck = "16:00";
                            break;
                        case 1:
                            TimeSlotCheck = "18:00";
                            break;
                        case 2:
                            TimeSlotCheck = "20:00";
                            break;
                        default:
                            TimeSlotCheck = null;
                            break;
                    }
                } while (ValidationLogic.IsValidTime(TimeSlotCheck) != true);
                timeslot = TimeSpan.Parse(TimeSlotCheck);
            }
        }

            Console.Clear();
            Console.WriteLine(date.ToString("dddd, dd MMMM yyyy"));
            ReservationLogic.ShowTablesAvailability(date, timeslot, amt_people);
            Console.WriteLine("email: " + email);
            
            string text;
            do {
                text = ReservationLogic.CodeGenerator();
            } while (ValidationLogic.CodeExists(text) != true);

            reservationcode = text;

            string tableCheck;
            do {
                Console.WriteLine("Welke tafel wilt u? (bijv. 4E of 2)");
                tableCheck = Console.ReadLine();
            } while (!TableLogic.CheckTables(date, timeslot, amt_people).Contains($"_{tableCheck.ToUpper()}"));
                
            id = ReservationLogic.GetLastID() + 1;

        if (TableLogic.TableChecker($"_{tableCheck.ToUpper()}"))
        {
            tables.Add(tableCheck);
            try
            {
                ReservationLogic.AddReservation(id, 0, name, email, date, reservationcode, timeslot, tables, amt_people);
                Console.WriteLine("Geluk!");
                Email.sendmail(email, name, date, timeslot);
                Email.warning();
            }
            catch (Exception e)
            {
                Console.WriteLine("Niet Geluk!");
                return;
            }

        }
    }
}
