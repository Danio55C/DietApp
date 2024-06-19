using System;
using System.Collections.Generic;
using SQLite;
using Newtonsoft.Json;

namespace DietApp.Models
{
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new List<RecipeIngredients>();
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull]
        public string RecipeName { get; set; }
        public string PreparatingMethod { get; set; }
        public int RecipeCalories { get; set; }
        public int RecipeCarbs { get; set; }
        public int RecipeProtein { get; set; }
        public int RecipeFats { get; set; }
        public double RecipePrice { get; set; }
        public DateTime Date { get; set; }

        // Serialize list to JSON for storage in SQLite
        public string IngredientsSerialized
        {
            get => JsonConvert.SerializeObject(Ingredients);
            set => Ingredients = JsonConvert.DeserializeObject<List<RecipeIngredients>>(value);
        }

        [Ignore]
        public List<RecipeIngredients> Ingredients { get; set; }
    }
}


