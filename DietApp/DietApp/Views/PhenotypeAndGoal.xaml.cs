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

            tDEpicker.ItemsSource = Enum.GetValues(typeof(LifeStyleTDE))
                            .Cast<LifeStyleTDE>()
                            .Select(value => EnumHelper.GetDescription(value))
                            .ToList();
            goalPicker.ItemsSource = GoalList;

            BindingContext = new PhenotypeAndGoalViewModel();
            //BindingContext = new UserData();

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
                    
                 
            }
            catch (Exception ex)
            {

                await DisplayAlert("Błąd", ex.ToString(), "cancel");
            }
        }
    }
}





        