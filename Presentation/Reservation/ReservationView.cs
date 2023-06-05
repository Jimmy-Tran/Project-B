using Newtonsoft.Json;
using Project_B.Logic;
using Project_B.DataModels;

public class Reservation
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    static public void DisplayReservation()
    {
        List<ReservationModel> reservations = ReservationLogic.GetReservations();

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Reservaties Tabel:");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("--------------------");
        Console.WriteLine("{0,-5} {1,-30} {2,-15} {3,-15} {4,-15} {5,-10}", "ID", "Naam", "Datum", "Tijd", "Tafel nummers", "Aantal Pers.");

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        foreach (ReservationModel reservation in reservations)
        {
            Console.WriteLine("{0,-5} {1,-30} {2,-15} {3,-15} {4,-15} {5,-10}",
                reservation.ID, reservation.Name, reservation.Date.ToString("dd-MM-yyyy"), reservation.TimeSlot.ToString(@"hh\:mm"), string.Join(", ", reservation.Tables), reservation.Amt_People);
        }
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    static public void MakeReservation()
    {
        string HasAccount;
        do
        {
            Console.Write("Heeft de klant een account? (J/N): ");
            HasAccount = Console.ReadLine().ToUpper();
        } while (HasAccount != "J" && HasAccount != "N");

        string ClientNumber = "0";
        string Name = "";
        string Email = "";


        if (HasAccount == "J")
        {
            string SearchTerm;
            do
            {
                Console.Write("Zoeken naar account (Email): ");
                SearchTerm = Console.ReadLine().ToLower();
            } while (SearchTerm.Length == 0);

            try
            {
                AccountModel AccountData = accountsLogic.GetByEmail(SearchTerm);

                ClientNumber = Convert.ToString(AccountData.Id);
                Name = AccountData.FullName;
                Email = AccountData.EmailAddress;

            }
            catch (Exception e)
            {
                Console.WriteLine("Account niet gevonden!");
                HasAccount = "N";
            }


        }
        if (HasAccount == "N")
        {
            do
            {
                Console.Write("Naam: ");
                Name = Console.ReadLine();
            } while (Name.Length <= 3);


            do
            {
                Console.Write("Email: ");
                Email = Console.ReadLine();
            } while (ValidationLogic.IsValidEmail(Email) != true);
        }

        string Date;
        do
        {
            Console.Write("Date (DD-MM-JJJJ): ");
            Date = Console.ReadLine();
        } while (ValidationLogic.IsValidDate(Date) != true);


        string TimeSlot;
        do
        {
            Console.Write("Time (00:00): ");
            TimeSlot = Console.ReadLine();
        } while (ValidationLogic.IsValidTime(TimeSlot) != true);


        int Amt_People;
        do
        {
            Console.Write("Aantal Personen: ");
            Amt_People = Convert.ToInt32(Console.ReadLine());
        } while (Amt_People <= 0);


        List<string> AvailableTables = TableLogic.CheckTables(DateTime.Parse(Date), TimeSpan.Parse(TimeSlot), Amt_People);
        Console.WriteLine($"Tafelnummers beschikbaar: " + string.Join(", ", AvailableTables));

        Console.WriteLine("Tafel nummers (klaar ENTER)");
        List<string> Tables = new List<string>();
        while (true)
        {
            string Table_number = Console.ReadLine();

            // Keep looping until user pressed ENTER, only numeric
            if (Table_number != "")
            {
                try
                {
                    Tables.Add(Table_number);
                }
                catch
                {
                    Console.WriteLine("Toevoegen mislukt, voer tafel nummers in!");
                }
            }
            else
            {
                break;
            }

        }


        // All values has been checked and ready to be added
        bool ChangedValue = ReservationLogic.AddReservation((ReservationLogic.GetLastID() + 1), Convert.ToInt32(ClientNumber), Name, Email, DateTime.Parse(Date), ReservationLogic.CodeGenerator(), TimeSpan.Parse(TimeSlot), Tables, Convert.ToInt32(Amt_People));

        // Reservation returns a boolean of the process
        if (ChangedValue != true)
        {
            Console.WriteLine("Er is iets fouts gegaan!");
        }
        else
        {
            Console.WriteLine("Gelukt!");
            EmailFunction.sendmail(Email, Name, ReservationLogic.CodeGenerator(), DateTime.Parse(Date), TimeSpan.Parse(TimeSlot));
        }
    }

    static public void ChangeReservation()
    {
        Console.Write("Zoeken (ID / Naam / Email): ");
        string Searchterm = Console.ReadLine();

        List<ReservationModel> r = ReservationLogic.GetReservations(Searchterm);

        if (r != null) {
            if (r.Count() > 1) {
                foreach (ReservationModel reservation in r) {
                    Console.WriteLine($"[{reservation.ID}] {reservation.Email} - {reservation.TimeSlot}");
                }
                Searchterm = Console.ReadLine();
            }

            string Name;
            do
            {
                Console.Write("Naam: ");
                Name = Console.ReadLine();
            } while (Name.Length <= 3);

            string Email;
            do
            {
                Console.Write("Email: ");
                Email = Console.ReadLine();
            } while (ValidationLogic.IsValidEmail(Email) != true);

            string Date;
            do
            {
                Console.Write("Date (DD-MM-JJJJ): ");
                Date = Console.ReadLine();
            } while (ValidationLogic.IsValidDate(Date) != true);

            string TimeSlot;
            do
            {
                Console.Write("Time (00:00): ");
                TimeSlot = Console.ReadLine();
            } while (ValidationLogic.IsValidTime(TimeSlot) != true);

            string Amt_People;
            do
            {
                Console.Write("Aantal Personen: ");
                Amt_People = Console.ReadLine();
            } while (ValidationLogic.IsNumeric(Amt_People) != true);

            Console.WriteLine("Beschikbare tafels: " + string.Join(", ", TableLogic.CheckTables(DateTime.Parse(Date), TimeSpan.Parse(TimeSlot), Convert.ToInt32(Amt_People)).Select(x => x.Substring(1)).ToList()));
            Console.WriteLine("Tafel nummers");
            List<string> Tables = new List<string>();
            while (true)
            {
                string Table_number = Console.ReadLine();
                if (Table_number != "")
                {
                    try
                    {
                        Tables.Add(Table_number);
                    }
                    catch
                    {
                        Console.WriteLine("Toevoegen mislukt, voer tafel nummers in!");
                    }
                }
                else
                {
                    break;
                }

            }

            // All values has been checked and ready to be changed
            bool ChangedValue = ReservationLogic.ChangeReservation(Searchterm, Name, Email, DateTime.Parse(Date), TimeSpan.Parse(TimeSlot), Tables, Convert.ToInt32(Amt_People));

            if (ChangedValue != true)
            {
                Console.WriteLine("Er is iets fouts gegaan!");
            }
            else
            {
                Console.WriteLine("Gelukt!");
            }
        }
        else
        {
            Console.WriteLine("Niet gevonden");
        }
    }

    public static void DeleteReservationWithID()
    {
        Console.WriteLine("ID");
        int ID = Convert.ToInt32(Console.ReadLine());

        ReservationModel ReservationObject = ReservationLogic.GetReservation(Convert.ToString(ID));

        if (ReservationObject == null)
        {
            Console.WriteLine("Reservatie niet gevonden!");
            return;
        }

        Console.WriteLine($"\nID: {ReservationObject.ID}");
        Console.WriteLine($"Name: {ReservationObject.Name}");
        Console.WriteLine($"Email: {ReservationObject.Email}");
        Console.WriteLine($"\nWeet u zeker dat deze reservering wordt verwijderd?");

        string DeleteObjectbool = "";
        do
        {
            Console.WriteLine($"(J/N)");
            DeleteObjectbool = Console.ReadLine().ToUpper();
        }
        while (DeleteObjectbool != "J" && DeleteObjectbool != "N");

        if (DeleteObjectbool == "J")
        {
            bool DeletedObject = ReservationLogic.DeleteReservation(ID);

            if (DeletedObject != true)
            {
                Console.WriteLine("Er is iets fouts gegaan!");
            }
            else
            {
                Console.WriteLine("Gelukt!");
            }
        }
        else if (DeleteObjectbool == "N")
        {
            Console.WriteLine("Geannuleerd.");
        }



    }
}