using System;
using SQLite;


namespace DietApp.Models
{
    public class Meal
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50)]   [NotNull]
        public string MealName { get; set; }
        [MaxLength(50)]
        public string MealType { get; set; }
        [NotNull]
        public int MealCalories { get; set; }
        [NotNull]
        public int MealCarbs { get; set; }
        [NotNull]
        public int MealProtein { get; set; }
        [NotNull]
        public int MealFats { get; set; }
        public double MealPrice { get; set; }
        public DateTime Date { get; set; }
    }
}

      







