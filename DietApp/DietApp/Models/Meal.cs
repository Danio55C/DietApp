using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace DietApp.Models
{
    public class Meal
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string MealName { get; set; }
        public string MealType { get; set; }
        public int MealCalories { get; set; }
        public int MealCarbs{ get; set; }
        public int MealProtein { get; set; }
        public int MealFats { get; set; }

    }
}
