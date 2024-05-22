using DietApp.Data;
using DietApp.Models;
using DietApp.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DietApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new UserMacros();
            LoadUserMacors();
        }

        protected override async void OnAppearing()
        {
             
            try
            {

            var userData = await App.Database.GetUserDataAsync();
            if (userData != null)
            {
                userDataListView.Text = userData.UserGoal;
            }
                
                
                
              
               
                
            }
            catch (Exception ex )
            {

                await Application.Current.MainPage.DisplayAlert("bŁąd", ex.ToString(), "OK");
            }



        }
        async void OnButtonClicked(object sender, EventArgs e)
        {
           
            await Shell.Current.GoToAsync(nameof(PhenotypeAndGoal));
        }

        private async void LoadUserMacors()
        {
            var userMacros = await App.Database.GetUserMacrosAsync();
            try
            {

                if (userMacros != null)
                {
                    caloriesLabel.Text = userMacros.DailyCaloricLimit.ToString();
                   
                }

                await Application.Current.MainPage.DisplayAlert("calorie", userMacros.DailyCaloricLimit.ToString(), "OK");
            }
            catch (Exception ex)
            {

                await DisplayAlert("Błąd", ex.ToString(), "cancel");
            }
        }
    }
}