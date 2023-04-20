using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Project_B.DataModels
{
    public class ReservationModel
    {
        [JsonPropertyName("id")]
        public int ID {get; set;}

        [JsonPropertyName("clientnumber")]
        public int ClientNumber {get; set;}

        [JsonPropertyName("name")]
        public string Name {get; set;}

        [JsonPropertyName("email")]
        public string Email {get; set;}

        [JsonPropertyName("date")]
        public string Date {get; set;}

        [JsonPropertyName("reservationcode")]
        public string ReservationCode {get; set;}

        [JsonPropertyName("timeslot")]
        public string TimeSlot {get; set;}

        [JsonPropertyName("tables")]
        public List<int> Tables {get; set;}
        
        [JsonPropertyName("amt_people")]
        public int Amt_People {get; set;}
    }
}