using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLite;

namespace DietApp.Models
{
    public class Recepie
    {

        public Recepie()
        {
            //Ingredients = new Collection<RecepieIngredients>();
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string RecepieName { get; set; }
        public string PreparatingMethod { get; set; }
        //public string Ingredients { get; set; }
        public int RecepieCalories { get; set; }
        public int RecepieCarbs { get; set; }
        public int RecepieProtein { get; set; }
        public int RecepieFats { get; set; }
        public DateTime Date { get; set; }

       // public ICollection<RecepieIngredients> Ingredients { get; set; }

    }
}
