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
        public static bool AddReservation(int _id, int _clientnumber, string _name, string _email, string _date, string _reservationcode, string _timeslot, List<int> _tables, int _amt_people) {
            try {
                string jsonContent = File.ReadAllText("DataSources/reservations.json");
                List<ReservationModel> reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(jsonContent);

                reservations.Add(new ReservationModel(_id, _clientnumber, _name, _email, _date, _reservationcode, _timeslot, _tables, _amt_people));

                string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                File.WriteAllText("DataSources/reservations.json", updatedJson);
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public static List<ReservationModel> GetReservations() {
            try {
                string jsonContent = File.ReadAllText("DataSources/reservations.json");
                List<ReservationModel> reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(jsonContent);
                return reservations;
            
                }
            catch (Exception ex) {
                    Console.WriteLine($"Error: {ex.Message}"); 
                }
            return new List<ReservationModel> ();
        }

        public static ReservationModel GetReservation(string _Searchterm) {
            try {
                string jsonContent = File.ReadAllText("DataSources/reservations.json");
                List<ReservationModel> reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(jsonContent);

                // List<string> UpdatableFields = new() {"name", "email", "date", "timeslot", "tables", "amt_people"};

                foreach (ReservationModel reservation in reservations) {
                    if (Convert.ToString(reservation.ID) == _Searchterm || reservation.Name == _Searchterm || reservation.Email == _Searchterm) {
                        return reservation;
                    }            
                }
                return null;
            }
            catch (Exception ex) {
                return null;
            }
        }

        public static bool ChangeReservation(string _Searchterm, string _name, string _email, string _date, string _timeslot, List<int> _tables, int _amt_people) {
            try {
                string jsonContent = File.ReadAllText("DataSources/reservations.json");
                List<ReservationModel> reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(jsonContent);

                foreach (ReservationModel reservation in reservations) {
                    if (reservation.Name == _Searchterm || reservation.Email == _Searchterm) {
                        reservation.Name = _name;
                        reservation.Email = _email;
                        reservation.Date = _date;
                        reservation.TimeSlot = _timeslot;
                        reservation.Tables = _tables;
                        reservation.Amt_People = _amt_people;
                    }            
                }

                string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                File.WriteAllText("DataSources/reservations.json", updatedJson);
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}"); 
                return false;
            }
        }

        public static bool DeleteReservation(int _ID) {
            //Todo: Add a parameter to GetReservation called ID. ID will also return a Object.
            try {
                string jsonContent = File.ReadAllText("DataSources/reservations.json");
                List<ReservationModel> reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(jsonContent);

                reservations.RemoveAll(x => x.ID == _ID);

                string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                File.WriteAllText("DataSources/reservations.json", updatedJson);
                
                return true;
            } 
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}"); 
                return false;
            }
        }
    }    
}
