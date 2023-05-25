using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Project_B.DataModels
{
    public class ReservationModel
    {
        [JsonPropertyName("ID")]
        public int ID {get; set;}

        [JsonPropertyName("ClientNumber")]
        public int ClientNumber {get; set;}

        [JsonPropertyName("Name")]
        public string Name {get; set;}

        [JsonPropertyName("Email")]
        public string Email {get; set;}

        [JsonPropertyName("Date")]
        public DateTime Date {get; set;}

        [JsonPropertyName("ReservationCode")]
        public string ReservationCode {get; set;}

        [JsonPropertyName("TimeSlot")]
        public TimeSpan TimeSlot {get; set;}

        [JsonPropertyName("Tables")]
        public List<string> Tables {get; set;}
        
        [JsonPropertyName("Amt_People")]
        public int Amt_People {get; set;}

        [JsonPropertyName("Course_Menu")]
        public List<string> CourseMenu {get; set;}

        [JsonPropertyName("Verified")]
        public bool Verified {get; set;}

        public ReservationModel(int _id, int _clientnumber, string _name, string _email, DateTime _date, string _reservationcode, TimeSpan _timeslot, List<string> _tables, int _amt_people) {
            ID = _id;
            ClientNumber = _clientnumber;
            Name = _name;
            Email = _email;
            Date = _date;
            ReservationCode = _reservationcode;
            TimeSlot = _timeslot;
            Tables = _tables;
            Amt_People = _amt_people;
            Verified = false;
    }
    }

    
}