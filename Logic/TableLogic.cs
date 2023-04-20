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


            return new List<int> ();
        }
    }    
}
