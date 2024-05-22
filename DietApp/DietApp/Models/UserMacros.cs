using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DietApp.Models
{
    public class UserMacros
    {
        public UserMacros()
        {
           

        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int DailyCaloricLimit { get; set; }
        //public int CaloriesConsumed { get; set; }
        public int DailyCarbsLimit { get; set; }
        //public int CarbsConsumed { get; set; }
        public int DailyProteinLimit { get; set; }
        //public int ProteinConsumed { get; set; }
        public int DailyFatsLimit { get; set; }
        //public int FatsConsumed { get; set; }



        private async Task<(int carbs, int fats, int protein, int calories)> CalcuteMacros()
        {
            var userData = await App.Database.GetUserDataAsync();

            if (userData == null)
            {
                // Obsługa przypadku, gdy dane użytkownika nie zostały znalezione w bazie danych
                return (0, 0, 0, 0);
            }


            switch (userData.UserGoal)
            {
                case "gain weight":
                    var carbsGain = (int)Math.Ceiling(userData.CurrentWeight * 2.2 * 2);
                    var fatsGain = (int)Math.Ceiling(userData.CurrentWeight * 2.2 * 0.75);
                    var proteinGain = (int)Math.Ceiling(userData.CurrentWeight * 2.2);
                    var totalCaloriesGain = carbsGain / 4 + fatsGain / 9 + proteinGain / 4;
                    return (carbsGain, fatsGain, proteinGain, totalCaloriesGain);

                case "lose weight":
                    var carbsLose = (int)Math.Ceiling(userData.CurrentWeight * 2.2);
                    var proteinLose = (int)Math.Ceiling(userData.CurrentWeight * 2.2);
                    var totalCaloriesLose = userData.TDEE - 100;
                    var fatsLose = userData.TDEE - 100 - (carbsLose / 4 + proteinLose / 4) / 9;
                    return (carbsLose, fatsLose, proteinLose, totalCaloriesLose);

                case "maintain weight":
                    var proteinMaintain = (int)Math.Ceiling(userData.CurrentWeight * 2.2);
                    var totalCaloriesMaintain = userData.TDEE;
                    var carbsMaintain = totalCaloriesMaintain / 2 * 4;
                    var fatsMaintain = totalCaloriesMaintain / 2 * 9;
                    return (carbsMaintain, fatsMaintain, proteinMaintain, totalCaloriesMaintain);

                default:
                    return (0, 0, 0, 0);

            }
        }

        public async Task SaveMacrosAsync()
        {

            var (carbs, fats, protein, calories) = await CalcuteMacros();

            var userMacros = new UserMacros
            {
                DailyCaloricLimit = calories,
                DailyCarbsLimit = carbs,
                DailyProteinLimit = protein,
                DailyFatsLimit = fats
            };

            await App.Database.SaveUserMacrosAsync(userMacros);
        }


    }
}

