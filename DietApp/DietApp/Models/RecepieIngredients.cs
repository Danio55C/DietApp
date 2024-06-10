using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DietApp.Models
{
   public class RecepieIngredients :Meal
    {
        public int RecepieId { get; set;}
        public int QuantityInGrams { get; set;}

       
    }
}
