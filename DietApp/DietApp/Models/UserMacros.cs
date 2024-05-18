using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DietApp.Models
{
    public class UserMacros
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int DailyCaloricLimit { get; set; }
        public int CaloriesConsumed { get; set; }
        public int DailyCarbsLimit { get; set; }
        public int CarbsConsumed { get; set; }
        public int DailyProteinLimit { get; set; }
        public int ProteinConsumed { get; set; }
        public int DailyFatsLimit { get; set; }
        public int FatsConsumed { get; set; }


    }
}
