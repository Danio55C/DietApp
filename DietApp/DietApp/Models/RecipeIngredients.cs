using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DietApp.Models
{
   public class RecipeIngredients :Meal
    {
        public int RecipeId { get; set;}
        public int QuantityInGrams { get; set;}
    }
}
        
        

       
