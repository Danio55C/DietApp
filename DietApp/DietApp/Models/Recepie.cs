using System;
using SQLite;

namespace DietApp.Models
{
    public class Recepie
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
