using DietApp.Models;
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
        public double ActivityIndex { get; set; }

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
                    ID=ID,
                    CurrentWeight = CurrentWeight,
                    Height = Height,
                    Age = Age,
                    Gender = Gender,
                    ActivityIndex = ActivityIndex
                    
                };
                await App.Database.SaveUserDataAsync(userData);
                await Application.Current.MainPage.DisplayAlert("Sukces", "$Twoje dane zostały zapisaneCZyli:", "OK");
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("bŁąd", ex.ToString(), "OK");
            }
            
        }

    }
}
