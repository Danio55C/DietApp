using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DietApp.Models
{
    public class UserData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        [NotNull]
        public int CurrentWeight { get; set; }
        [NotNull]
        public int Height { get; set; }
        
        [NotNull]
        public int Age { get; set; }
        [MaxLength(10)]
        [NotNull]
        public string Gender { get; set; }
        [MaxLength(200)]
        [NotNull]
        public string LifeStyleTDEE { get; set; }
        public double ActivityIndex { get; set; }
        [MaxLength(50)]
        [NotNull]
        public string UserGoal { get; set; }
        public int TDEE => CalcuteTDEE();

        private int CalcuteTDEE()
        {
            if (Gender == "Male")
                return (int)Math.Ceiling( ActivityIndex * (1.1 * (10 * CurrentWeight + 6.26 * Height - 5 * Age + 5)));
            else
                return (int)Math.Ceiling(ActivityIndex * (1.1 * (10 * CurrentWeight + 6.26 * Height - 5 * Age - 161)));
        }
       
    }
}










