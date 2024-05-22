using DietApp.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static Humanizer.In;

namespace DietApp.ViewModel
{
    public class MainPageViewModel
    {
        public int ID { get; set; }
        public int DailyCaloricLimit { get; }
        //public int CaloriesConsumed { get; set; }
        public int DailyCarbsLimit { get; }
       // public int CarbsConsumed { get; set; }
        public int DailyProteinLimit { get; }
       // public int ProteinConsumed { get; set; }
        public int DailyFatsLimit { get; }
        //public int FatsConsumed { get; set; }

        public MainPageViewModel()
        {
            
        }
    }
}
