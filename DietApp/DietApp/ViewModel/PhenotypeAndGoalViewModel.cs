﻿using DietApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static Humanizer.In;

namespace DietApp.ViewModel
{
    public class PhenotypeAndGoalViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public int ID { get; set; }
        public int CurrentWeight { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string LifeStyleTDEE { get; set; } //lifestyle TDEE

        public ICommand SaveCommand { get; private set; }

        public PhenotypeAndGoalViewModel()
        {
            SaveCommand = new Command(OnSave);

        }

        private async void OnSave(object obj)
        {
            try
            {
                var userData = new UserData
                {
                    ID = ID,
                    CurrentWeight = CurrentWeight,
                    Height = Height,
                    Age = Age,
                    Gender = Gender,
                    LifeStyleTDEE = LifeStyleTDEE

                };
                await App.Database.SaveUserDataAsync(userData);
                await Application.Current.MainPage.DisplayAlert("Sukces", "$Twoje dane zostały zapisaneCZyli:", "OK");
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("bŁąd", ex.ToString(), "OK");
            }

        }

        //zamień ten activity index

        //public double ConvertActivityIndex(string LifeStyleTDEE)
        //{
        //    switch (LifeStyleTDEE)
        //    {
        //        case "Nieaktywny/siedzący: prawie brak aktywności w ciągu dnia":
        //            return 1.2;
        //            break;
        //        case "Lekki: 1-3 treningów w tygodniu, lub praca z lekką aktywnością":
        //            return 1.375;
        //            break;
        //        case "Średni: 3-5 treningów w tygodniu":
        //            return 1.55;
        //            break;
        //        case "Wysoki: 6-7 treningów w tygodniu":
        //            return 1.725;
        //            break;
        //        case "Bardzo Wysoki: Ciężka praca fizyczna lub treningi dwa razy w tygodniu":
        //            return 1.9;
        //            break;
        //        default:
        //            return 0;
        //            break;
        //    }
        //}

    }
}
