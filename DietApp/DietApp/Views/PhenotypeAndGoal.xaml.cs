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

            BindingContext = new UserData();
            
        }

        List<string> GenderList = new List<string>
        {
            "Female","Male"
        };

        List<string> GoalList = new List<string>
        {
            "lose weight","gain weight","maintain weight"
        };


        

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                GetUsersData();
            }
            catch (Exception ex)
            {

                DisplayAlert("Błąd",ex.ToString(),"cancel");
            }
            
            //DisplayAlert("Ustawienia zapisano","twoje dane zostały zapisane" , "cancel");
        }

        

        

        async private void GetUsersData()
        {
            try
            {

                var userdate = (UserData)BindingContext;
                userdate.Gender = (string)genderPicker.SelectedItem;
                userdate.CurrentWeight = int.Parse(weightEntry.Text);
                userdate.Height = int.Parse(heightEntry.Text);
                userdate.Age = int.Parse(ageEntry.Text);






                switch (tDEpicker.SelectedItem)
            { 
                    case "Nieaktywny/siedzący: prawie brak aktywności w ciągu dnia":
                    userdate.ActivityIndex = 1.2;
                    break;
                    case "Lekki: 1-3 treningów w tygodniu, lub praca z lekką aktywnością":
                    userdate.ActivityIndex = 1.375;
                    break;
                    case "Średni: 3-5 treningów w tygodniu":
                    userdate.ActivityIndex = 1.55;
                    break;
                    case "Wysoki: 6-7 treningów w tygodniu":
                    userdate.ActivityIndex = 1.725;
                    break;
                    case "Bardzo Wysoki: Ciężka praca fizyczna lub treningi dwa razy w tygodniu":
                    userdate.ActivityIndex = 1.9;
                    break;
                default:
                  await  DisplayAlert("bŁad", "Zlie wybrany Tryb życia", "cancel");
                    break;
            }
                await App.Database.SaveUserDataAsync(userdate);

           await DisplayAlert("Dobrze", $"płeć: {userdate.Gender}, waga:{userdate.CurrentWeight}, wzrost: {userdate.Height}, wiek:{userdate.Age}, index:{userdate.ActivityIndex}", "cancel");

                
               
                
            }
           
             catch (Exception ex)
            {

               await DisplayAlert("Błąd", ex.ToString(), "cancel");
            }
        }



        private void OnSaveGoalButtonClicked(object sender, EventArgs e)
        {
            UserGoal usergoal = new UserGoal();
            usergoal.Goal = (string)goalPicker.SelectedItem;
        }
        private void OnEdditButtonClicked(object sender, EventArgs e)
        {
            try
            {

                var userdate = (UserData)BindingContext;
                
                DisplayAlert("Baza danych", $"płeć: {userdate.Gender}, waga:{userdate.CurrentWeight}, wzrost: {userdate.Height}, wiek:{userdate.Age}, index:{userdate.ActivityIndex}", "cancel");
            }
            catch (Exception ex)
            {

                 DisplayAlert("Błąd", ex.ToString(), "cancel");
            }
            
            
        }













        //zbinduj wiek wzrost i wage dodaj liste goal oraz tryb życia
    }
}