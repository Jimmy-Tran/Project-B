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

        public static List<string> AvailableTables = new List<string> {"_6A", "_6B", "_4A", "_4B", "_4C", "_4D", "_4E", "_1", "_2", "_3", "_4", "_5", "_6", "_7", "_8", "A", "B", "C", "D", "E", "F", "G", "H" };




        public static List<string> CheckTables(DateTime date, TimeSpan timeslot, int persons) {
            List<ReservationModel> res = ReservationLogic.GetReservations();
            foreach (ReservationModel reservartion in res) {
                if (reservartion.Date == date && reservartion.TimeSlot == timeslot) {
                    if(reservartion.Tables != null) {
                        foreach (string i in reservartion.Tables) {
                            Console.WriteLine(i);
                            AvailableTables.Remove($"{i}");
                        }
                    }
                    
                }
            }

        

            if (persons == 6 || persons == 5) {
                AvailableTables = AvailableTables.Where(table => table.StartsWith("_6") && table.Length == 3).ToList();
            } else if (persons == 4 || persons == 3) {
                AvailableTables = AvailableTables.Where(table => table.StartsWith("_4") && table.Length == 3).ToList();
            } else if (persons == 2) {
                AvailableTables = AvailableTables.Where(table => table.Length == 2).ToList();
            } else if (persons == 1) {
                AvailableTables = AvailableTables.Where(table => char.IsLetter(table[0])).ToList();
            }

            return AvailableTables;

        }

        public static bool TableChecker(string table) {
            if(AvailableTables.Contains(table)) {
                return true;
            } else {
                return false;
            }
        }
    }    
}