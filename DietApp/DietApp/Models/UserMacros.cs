using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DietApp.Models
{
    public class UserMacros
    {
        

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


        public UserMacros()
        {
            InitializeAsync();
        }
        private async Task<(int carbs, int fats, int protein, int calories)> CalculateMacros()
        {
            var userData = await App.Database.GetUserDataAsync();

            if (userData == null)
            {
                // Obsługa przypadku, gdy dane użytkownika nie zostały znalezione w bazie danych
                return (0, 0, 0, 0);
            }


            switch (userData.UserGoal)
            {
                //to do poprawy
                case "gain weight":
                    var carbsGain = (int)Math.Ceiling(userData.CurrentWeight * 2.2 * 2);
                    var proteinGain = (int)Math.Ceiling(userData.CurrentWeight * 2.2);
                    var fatsGain = (int)Math.Ceiling(userData.CurrentWeight * 2.2 * 0.75);
                    var totalCaloriesGain = ((carbsGain * 4) + (fatsGain * 9) + (proteinGain * 4));
                    return (carbsGain, fatsGain, proteinGain, totalCaloriesGain);

                case "lose weight":
                    var carbsLose = (int)Math.Ceiling(userData.CurrentWeight * 2.2);
                    var proteinLose = (int)Math.Ceiling(userData.CurrentWeight * 2.2);
                    var totalCaloriesLose = userData.TDEE - 100;
                    var fatsLose = (totalCaloriesLose-(carbsLose * 4 + proteinLose * 4)) / 9;
                    return (carbsLose, fatsLose, proteinLose, totalCaloriesLose);

                case "maintain weight":
                    var proteinMaintain = (int)Math.Ceiling(userData.CurrentWeight * 2.2);
                    var totalCaloriesMaintain = userData.TDEE;
                    var carbsMaintain = (totalCaloriesMaintain- (proteinMaintain*4)) / 2 / 4;
                    var fatsMaintain = (totalCaloriesMaintain-(proteinMaintain*4)) /2 / 9;
                    return (carbsMaintain, fatsMaintain, proteinMaintain, totalCaloriesMaintain);

                default:
                    return (0, 0, 0, 0);
            }
        }

        private async void InitializeAsync()
        {
            // Wywołaj metodę CalculateMacros i przypisz wartości do właściwości
            var macros = await CalculateMacros();
            DailyCaloricLimit = macros.calories;
            DailyCarbsLimit = macros.carbs;
            DailyProteinLimit = macros.protein;
            DailyFatsLimit = macros.fats;

            // Zapisz dane do bazy danych
            await SaveMacrosAsync();
        }
        public async Task SaveMacrosAsync()
        {

            await App.Database.SaveUserMacrosAsync(this);
        }


    }
}

