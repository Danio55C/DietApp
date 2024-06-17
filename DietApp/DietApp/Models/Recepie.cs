using System;
using System.Collections.Generic;
using SQLite;
using Newtonsoft.Json;

namespace DietApp.Models
{
    public class Recepie
    {
        public Recepie()
        {
            Ingredients = new List<RecepieIngredients>();
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull]
        public string RecepieName { get; set; }

        public string PreparatingMethod { get; set; }
        public int RecepieCalories { get; set; }
        public int RecepieCarbs { get; set; }
        public int RecepieProtein { get; set; }
        public int RecepieFats { get; set; }
        public DateTime Date { get; set; }

        // Serialize list to JSON for storage in SQLite
        public string IngredientsSerialized
        {
            get => JsonConvert.SerializeObject(Ingredients);
            set => Ingredients = JsonConvert.DeserializeObject<List<RecepieIngredients>>(value);
        }

        [Ignore]
        public List<RecepieIngredients> Ingredients { get; set; }
    }
}

