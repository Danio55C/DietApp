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
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Błąd", ex.ToString(), "cancel");
            }
        }

        //async void OnSaveButtonClicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GetUsersData();
        //    }
        //    catch (Exception ex)
        //    {

        //        await DisplayAlert("Błąd", ex.ToString(), "cancel");
        //    }

        //    //DisplayAlert("Ustawienia zapisano","twoje dane zostały zapisane" , "cancel");
        //}





        //async private void GetUsersData()
        //{
        //    try
        //    {

        //        var userdate = (UserData)BindingContext;
        //        //userdate.ID = 1;
        //        //userdate.Gender = (string)genderPicker.SelectedItem;
        //        //userdate.CurrentWeight = int.Parse(weightEntry.Text);
        //        //userdate.Height = int.Parse(heightEntry.Text);
        //        //userdate.Age = int.Parse(ageEntry.Text);






        //        

        //         App.Database.SaveUserData(userdate);

        //   await DisplayAlert("Dobrze", $"płeć: {userdate.Gender}, waga:{userdate.CurrentWeight}, wzrost: {userdate.Height}, wiek:{userdate.Age}, index:{userdate.ActivityIndex}", "cancel");




        //    }

        //     catch (Exception ex)
        //    {

        //       await DisplayAlert("Błąd", ex.ToString(), "cancel");
        //    }
        //}



        //private void OnSaveGoalButtonClicked(object sender, EventArgs e)
        //{
        //    UserGoal usergoal = new UserGoal();
        //    usergoal.Goal = (string)goalPicker.SelectedItem;
        //}
        //private void OnEdditButtonClicked(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        var userdate = (UserData)BindingContext;

        //        DisplayAlert("Baza danych", $"płeć: {userdate.Gender}, waga:{userdate.CurrentWeight}, wzrost: {userdate.Height}, wiek:{userdate.Age}, index:{userdate.ActivityIndex}", "cancel");
        //    }
        //    catch (Exception ex)
        //    {

        //         DisplayAlert("Błąd", ex.ToString(), "cancel");
        //    }


        //}













        //zbinduj wiek wzrost i wage dodaj liste goal oraz tryb życia
    }
}