using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Project_B.Logic;
class ReservationConsole
{

    public int id { get; set; }
    public int clientnumber { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public string? date { get; set; }
    public string? reservationcode { get; set; }
    public int timeslot { get; set; }
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
                                    Console.WriteLine("Welke datum wilt u reserveren? (DD/MM/YYYY):");
                                    string dateCheck = Console.ReadLine();
                                    DateTime d;
                                    if (DateTime.TryParseExact(dateCheck, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                                    {
                                        if (d.Date < DateTime.Today)
                                        {
                                            Console.WriteLine("The date entered is before the current date.");
                                        }
                                        else
                                        {
                                            field4Valid = true;
                                            Console.WriteLine("The input is a valid date.");
                                            date = dateCheck;
                                            bool field5Valid = false;
                                            while(field5Valid is false) {
                                                Console.WriteLine("Selecteer een tijdslot:");
                                                Console.WriteLine("[1] 16:00 - 18:00");
                                                Console.WriteLine("[2] 18:00 - 20:00");
                                                Console.WriteLine("[3] 20:00 - 22:00");
                                                int timeslotCheck = Convert.ToInt32(Console.ReadLine());
                                                if(timeslotCheck == 1 || timeslotCheck == 2 || timeslotCheck == 3) {
                                                    field5Valid = true;
                                                    timeslot = timeslotCheck;
                                                }
                                            }
                                        }
                                    }

                                    else
                                        {
                                            // The input is not a valid date
                                            Console.WriteLine("Ongeldige datum. Voer een geldige datum in (DD/MM/YYYY).");
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

            Console.WriteLine(date);
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

