using DietApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Reflection;
using System.Linq.Expressions;
using System.Security.Cryptography;
using DietApp.Data;
using DietApp.ViewModel;

namespace DietApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhenotypeAndGoal : ContentPage
    {
        public PhenotypeAndGoal()
        {
            InitializeComponent();
            genderPicker.ItemsSource = GenderList;

            tDEpicker.ItemsSource = Lifestyle;
            goalPicker.ItemsSource = GoalList;

            BindingContext = new PhenotypeAndGoalViewModel();
           

            LoadUserData();

        }



        List<string> GenderList = new List<string>
        {
            "Female","Male"
        };

        List<string> GoalList = new List<string>
        {
            "lose weight","gain weight","maintain weight"
        };

        List<string> Lifestyle = new List<string>
        {
            "Nieaktywny/siedzący: prawie brak aktywności w ciągu dnia",
            "Lekki: 1-3 treningów w tygodniu, lub praca z lekką aktywnością",
            "Średni: 3-5 treningów w tygodniu",
            "Wysoki: 6-7 treningów w tygodniu",
            "Bardzo Wysoki: Ciężka praca fizyczna lub treningi dwa razy w tygodniu"
        };



        private async void LoadUserData()
        {
            var userData = await App.Database.GetUserDataAsync();
            try
            {

                if (userData != null)
                {

                    genderPicker.SelectedItem = userData.Gender;
                    weightEntry.Text = userData.CurrentWeight.ToString();
                    heightEntry.Text = userData.Height.ToString();
                    ageEntry.Text = userData.Age.ToString();
                    tDEpicker.SelectedItem = userData.LifeStyleTDEE;
                    goalPicker.SelectedItem = userData.UserGoal;
                    //testLabel.Text = userData.ActivityIndex.ToString();
                }

                else
                {
                    await DisplayAlert("Błąd", "null", "cancel");
                }
            
            }
            catch (Exception ex)
            {

                await DisplayAlert("Błąd", ex.ToString(), "cancel");
            }
        }
    }
}





        