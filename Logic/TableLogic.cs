using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Project_B.Logic;
using Project_B.DataModels;

namespace Project_B.Logic
{
    public static class TableLogic
    {
        public static List<int> CheckTables() {
            string DateNow = DateTime.Now.ToString("dd-MM-yyyy");
            List<ReservationModel> reservations = ReservationLogic.GetReservations();

            List<int> AvailableTables = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};

            foreach (ReservationModel reservartion in reservations) {
                if (reservartion.Date == DateNow) {
                    foreach (int i in reservartion.Tables) {
                        AvailableTables.Remove(i);
                    }
                    
                }
            }

            return AvailableTables;

        }
    }    
}
