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
        public static List<ReservationModel> GetReservation() {
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
    }
}