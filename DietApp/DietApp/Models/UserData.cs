using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DietApp.Models
{
    public class UserData
    {
        [PrimaryKey, AutoIncrement]
        public  int ID { get; set; }
        public int CurrentWeight { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string LifeStyleTDEE { get; set; }
        public double ActivityIndex { get; set ; }
        public string UserGoal { get; set ; }
        
    }
}


    
