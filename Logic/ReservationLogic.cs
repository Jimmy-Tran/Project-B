using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Project_B.DataModels;



namespace Project_B.Logic
{
    public static class ReservationLogic
    {
    static string unavailable = "\u001b[31m";
    static string available = "\u001b[32m";
    static string cyan = "\u001b[36m";

    public static List<string> availableTables = new List<string>();

    static string _6A = unavailable;
    static string _6B = unavailable;
    static string _4A = unavailable;
    static string _4B = unavailable;
    static string _4C = unavailable;
    static string _4D = unavailable;
    static string _4E = unavailable;
    static string _1 = unavailable;
    static string _2 = unavailable;
    static string _3 = unavailable;
    static string _4 = unavailable;
    static string _5 = unavailable;
    static string _6 = unavailable;
    static string _7 = unavailable;
    static string _8 = unavailable;
    static string A = unavailable;
    static string B = unavailable;
    static string C = unavailable;
    static string D = unavailable;
    static string E = unavailable;
    static string F = unavailable;
    static string G = unavailable;
    static string H = unavailable;

        public static bool AddReservation(int _id, int _clientnumber, string _name, string _email, DateTime _date, string _reservationcode, TimeSpan _timeslot, List<string> _tables, int _amt_people) {
            try {
                //Try to get the reservations and convert them into a list
                string jsonContent = File.ReadAllText("DataSources/reservations.json");
                List<ReservationModel> reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(jsonContent);

                //Add an item to the list
                reservations.Add(new ReservationModel(_id, _clientnumber, _name, _email, _date, _reservationcode, _timeslot, _tables, _amt_people));

                //Serialize the list to an object and write it back to the JSON file. Return true when all went good
                string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                File.WriteAllText("DataSources/reservations.json", updatedJson);
                return true;
            }
            catch (Exception ex) { // Catch the error and return false
                return false;
            }
        }
          
        public static List<ReservationModel> GetReservations() {
            try {
                //Try to get the reservations and convert them into a list
                string jsonContent = File.ReadAllText("DataSources/reservations.json");
                List<ReservationModel> reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(jsonContent);
                return reservations;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new List<ReservationModel>();
        }

        public static ReservationModel GetReservation(string _Searchterm) {
            try {
                //Try to get the reservation and convert them into a list

                string jsonContent = File.ReadAllText("DataSources/reservations.json");
                List<ReservationModel> reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(jsonContent);

                // List<string> UpdatableFields = new() {"name", "email", "date", "timeslot", "tables", "amt_people"};

                //Loop through the list and get the reservation by the given searchterm
                foreach (ReservationModel reservation in reservations) {
                    if (Convert.ToString(reservation.ID) == _Searchterm || reservation.Name == _Searchterm || reservation.Email == _Searchterm) {
                        return reservation; //Return the reservation
                    }            
                }
                return null; //Return nothing if nothing came out
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool ChangeReservation(string _Searchterm, string _name, string _email, DateTime _date, TimeSpan _timeslot, List<string> _tables, int _amt_people) {
            try {
                //Try to get the reservations and convert them into a list
                string jsonContent = File.ReadAllText("DataSources/reservations.json");
                List<ReservationModel> reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(jsonContent);

                //Loop through the list and get the reservation by the given searchterm
                foreach (ReservationModel reservation in reservations) {
                    if (reservation.Name == _Searchterm || reservation.Email == _Searchterm) {
                        //Change the old value to the new value
                        reservation.Name = _name;
                        reservation.Email = _email;
                        reservation.Date = _date;
                        reservation.TimeSlot = _timeslot;
                        reservation.Tables = _tables;
                        reservation.Amt_People = _amt_people;
                    }
                }

                //Serialize the list and write it back to JSON file
                string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                File.WriteAllText("DataSources/reservations.json", updatedJson);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public static bool DeleteReservation(int _ID)
        {
            //Todo: Add a parameter to GetReservation called ID. ID will also return a Object.
            try {
                //Try to get the reservations and convert them into a list
                string jsonContent = File.ReadAllText("DataSources/reservations.json");
                List<ReservationModel> reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(jsonContent);

                reservations.RemoveAll(x => x.ID == _ID);

                string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                File.WriteAllText("DataSources/reservations.json", updatedJson);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public static bool VerifyingReservation(int _ID) {
            try {
                //Try to get the reservations and convert them into a list
                string jsonContent = File.ReadAllText("DataSources/reservations.json");
                List<ReservationModel> reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(jsonContent);

                reservations.Where(x => x.ID == _ID).ToList().ForEach(x => x.Verified = true);

                string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                File.WriteAllText("DataSources/reservations.json", updatedJson);
                
                return true;
            } 
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}"); 
                return false;
            }
        }
        
        public static void UpdateTableAvailability(List<string> TablesList)
        {
            _6A = TablesList.Contains("_6A") ? available : unavailable;
            _6B = TablesList.Contains("_6B") ? available : unavailable;
            _4A = TablesList.Contains("_4A") ? available : unavailable;
            _4B = TablesList.Contains("_4B") ? available : unavailable;
            _4C = TablesList.Contains("_4C") ? available : unavailable;
            _4D = TablesList.Contains("_4D") ? available : unavailable;
            _4E = TablesList.Contains("_4E") ? available : unavailable;
            _1 = TablesList.Contains("_1") ? available : unavailable;
            _2 = TablesList.Contains("_2") ? available : unavailable;
            _3 = TablesList.Contains("_3") ? available : unavailable;
            _4 = TablesList.Contains("_4") ? available : unavailable;
            _5 = TablesList.Contains("_5") ? available : unavailable;
            _6 = TablesList.Contains("_6") ? available : unavailable;
            _7 = TablesList.Contains("_7") ? available : unavailable;
            _8 = TablesList.Contains("_8") ? available : unavailable;
            A = TablesList.Contains("A") ? available : unavailable;
            B = TablesList.Contains("B") ? available : unavailable;
            C = TablesList.Contains("C") ? available : unavailable;
            D = TablesList.Contains("D") ? available : unavailable;
            E = TablesList.Contains("E") ? available : unavailable;
            F = TablesList.Contains("F") ? available : unavailable;
            G = TablesList.Contains("G") ? available : unavailable;
            H = TablesList.Contains("H") ? available : unavailable;
        }

        public static void ShowTablesAvailability(DateTime date, TimeSpan timeslot, int persons)
        {
            if (date == null)
            {
                // handle the case where date is null
                return;
            }
            else
            {
                List<string> TablesList = TableLogic.CheckTables(date, timeslot, persons);
                UpdateTableAvailability(TablesList);
                ShowTables();
            }
        }


        public static void ShowTables()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("");
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine($"║          ╭──────────────────╮          {cyan}║");
            Console.WriteLine($"║          │     Bar Area     │          {cyan}║");
            Console.WriteLine($"║          ╰──────────────────╯          {cyan}║");
            Console.WriteLine($"║            {A}○ {B}○ {C}○ {D}○ {E}○ {F}○ {G}○ {H}○             {cyan}║");
            Console.WriteLine($"║            {A}A {B}B {C}C {D}D {E}E {F}F {G}G H             {cyan}║");
            Console.WriteLine("║                                        ║");
            Console.WriteLine($"║      {_6A}  ○ ○ ○  {_6B}            ○ ○ ○        {cyan}║");
            Console.WriteLine($"║      {_6A}╭───────╮{_6B}          ╭───────╮      {cyan}║");
            Console.WriteLine($"║      {_6A}│   6   │{_6B}          │   6   │      {cyan}║");
            Console.WriteLine($"║      {_6A}│   A   │{_6B}          │   B   │      {cyan}║");
            Console.WriteLine($"║      {_6A}╰───────╯{_6B}          ╰───────╯      {cyan}║");
            Console.WriteLine($"║      {_6A}  ○ ○ ○  {_6B}            ○ ○ ○        {cyan}║");
            Console.WriteLine("║                                        ║");
            Console.WriteLine($"║{_4A}   ○  ○  {_4B}     ○  ○  {_4C}     ○  ○  {_1}    ○    {cyan}║");
            Console.WriteLine($"║{_4A} ╭──────╮{_4B}   ╭──────╮{_4C}   ╭──────╮{_1}  ╭───╮  {cyan}║");
            Console.WriteLine($"║{_4A} │  4A  │{_4B}   │  4B  │{_4C}   │  4C  │{_1}  │ 1 │  {cyan}║");
            Console.WriteLine($"║{_4A} ╰──────╯{_4B}   ╰──────╯{_4C}   ╰──────╯{_1}  ╰───╯  {cyan}║");
            Console.WriteLine($"║{_4A}   ○  ○  {_4B}     ○  ○  {_4C}     ○  ○  {_1}    ○    {cyan}║");
            Console.WriteLine("║                                        ║");
            Console.WriteLine($"║{_4D}   ○  ○  {_4E}    ○  ○  {_2}    ○  {_3}   ○  {_4}   ○    {cyan}║");
            Console.WriteLine($"║{_4D} ╭──────╮{_4E}  ╭──────╮{_2}  ╭───╮{_3} ╭───╮{_4} ╭───╮  {cyan}║");
            Console.WriteLine($"║{_4D} │  4D  │{_4E}  │  4E  │{_2}  │ 2 │{_3} │ 3 │{_4} │ 4 │  {cyan}║");
            Console.WriteLine($"║{_4D} ╰──────╯{_4E}  ╰──────╯{_2}  ╰───╯{_3} ╰───╯{_4} ╰───╯  {cyan}║");
            Console.WriteLine($"║{_4D}   ○  ○  {_4E}    ○  ○  {_2}    ○  {_3}   ○  {_4}   ○    {cyan}║");
            Console.WriteLine("║                                        ║");
            Console.WriteLine($"║{_5}      ○  {_6}      ○  {_7}      ○  {_8}      ○      {cyan}║");
            Console.WriteLine($"║{_5}    ╭───╮{_6}    ╭───╮{_7}    ╭───╮{_8}    ╭───╮    {cyan}║");
            Console.WriteLine($"║{_5}    │ 5 │{_6}    │ 6 │{_7}    │ 7 │{_8}    │ 8 │    {cyan}║");
            Console.WriteLine($"║{_5}    ╰───╯{_6}    ╰───╯{_7}    ╰───╯{_8}    ╰───╯    {cyan}║");
            Console.WriteLine($"║{_5}      ○  {_6}      ○  {_7}      ○  {_8}      ○      {cyan}║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║                Entrance                ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static int GetLastID()
        {
            string jsonContent = File.ReadAllText("DataSources/reservations.json");
            List<ReservationModel> reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(jsonContent);

            return reservations.Last().ID;
        }
        public static string CodeGenerator()
        {
            // Creating object of random class
            Random rand = new Random();

            int randValue;
            string str = "";
            char letter;
            for (int i = 0; i < 6; i++)
            {

                // Generating a random number.
                randValue = rand.Next(0, 26);
                if (i == 2 || i == 4)
                {
                    str += $"{rand.Next(1, 9)}";
                }
                else
                {
                    // Generating random character by converting
                    // the random number into character.
                    letter = Convert.ToChar(randValue + 65);

                    // Appending the letter to string.
                    str += letter;
                }
            }
            return str;
        }

    }

}





