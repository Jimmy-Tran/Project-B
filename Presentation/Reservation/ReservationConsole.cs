using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Project_B.Logic;

class ReservationConsole
{

    public int id { get; set; }
    public int clientnumber { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public DateTime date { get; set; }
    public string? reservationcode { get; set; }
    public TimeSpan timeslot { get; set; }
    public int amt_people {get; set;}
    public List<string> tables = new List<string>();



    bool reserveValid = true;
    


    public void Reserveren()
    {
        bool fieldValid = false;
        while(fieldValid is false) {
            Console.WriteLine("[1] Reservering maken met een account.");
            Console.WriteLine("[2] Reservering maken zonder een account.");
            string option = Console.ReadLine();
            
            if (option == "1" || option == "2") {
                fieldValid = true;
                if (option == "1") {
                    
                } else {
                    bool field2Valid = false;
                    while(field2Valid is false) {
                        Console.WriteLine("Wat is uw email adres? (bijv. John.Doe@gmail.com)");
                        string emailCheck = Console.ReadLine();
                        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                        if(emailCheck.Length > 4 && Regex.IsMatch(emailCheck, pattern)){
                            email = emailCheck; 
                            field2Valid = true;
                            bool field3Valid = false;
                            while(field3Valid is false) {
                                Console.WriteLine("Hoeveel mensen zullen er zijn inclusief u?");
                                int amountpeopleCheck = Convert.ToInt32(Console.ReadLine());
                                if(amountpeopleCheck > 0) {
                                    field3Valid = true;
                                    amt_people = amountpeopleCheck;
                                    bool field4Valid = false;
                                    while(field4Valid is false) {
                                        string DateCheck;
                                        do {
                                            Console.WriteLine("Welke datum wilt u reserveren? (DD-MM-JJJJ):");
                                            DateCheck = Console.ReadLine();
                                        } while (ValidationLogic.IsValidDate(DateCheck) != true);                                    
                            
                                        if (DateTime.Parse(DateCheck) < DateTime.Today) {
                                            Console.WriteLine("The date entered is before the current date.");
                                        }

                                        else {
                                            field4Valid = true;
                                            Console.WriteLine("The input is a valid date.");

                                            date = DateTime.Parse(DateCheck);

                                            string TimeSlotCheck;
                                            do {
                                                Console.WriteLine("Selecteer een tijdslot:");
                                                int selectedClass = MenuLogic.MultipleChoice(true, "○", 1, "16:00 - 18:00", "18:00 - 20:00", "20:00 - 22:00");
                                                
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
                                } else if(amountpeopleCheck > 6) {
                                    Console.WriteLine("Graag contact met ons opnemen via telefoon 063828192");
                                }
                            }
                        }
                    }
                }
            }
        }

            Console.WriteLine(date.ToString("dddd, dd MMMM yyyy"));
            Console.WriteLine(timeslot);
            ReservationLogic.ShowTablesAvailability(date, timeslot, amt_people);
            Console.WriteLine("email: " + email);
            string json = File.ReadAllText("./DataSources/reservations.json");
            // Deserialize the JSON into a list of Reservation objects
            List<ReservationConsole> reservations = JsonConvert.DeserializeObject<List<ReservationConsole>>(json);
            bool codeExists = true;
            while (codeExists)
            {
                string text = ReservationLogic.CodeGenerator();
                if (!reservations.Any(r => r.reservationcode == text))
                {
                    reservationcode = text;
                    codeExists = false;
                }
            }
            int highestId = reservations.Max(reservation => reservation.id);
            Console.WriteLine("Welke tafel wilt u? (bijv. 4E of 2)");
            string tableCheck = Console.ReadLine();
            bool field6Valid = false;
            while(field6Valid is false) {
                Console.WriteLine("Welke tafel wilt u? (bijv. 4E of 2)");
                if(tableCheck.Length == 2) {
                    tableCheck = $"_{tableCheck.ToUpper()}";
                    Console.WriteLine(tableCheck);
                    field6Valid = true;
                    if(TableLogic.TableChecker(tableCheck) is true) {
                        tables.Add(tableCheck);
                        ReservationLogic.AddReservation(id, 0, name, email, date, reservationcode, timeslot, tables, amt_people);
                        field6Valid = true;
                    }
                } else {
                    Console.Write("wrong");
                }

            }
        }



    }

