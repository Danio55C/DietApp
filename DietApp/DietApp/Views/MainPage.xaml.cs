﻿using DietApp.Data;
using DietApp.Models;
using DietApp.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DietApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            BindingContext = new UserMacros();
            InitializeComponent();
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

                await LoadUserMacros();

            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("bŁąd", ex.ToString(), "OK");
            }

        }
        async void OnChangeGoalButtonClicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync(nameof(PhenotypeAndGoal));
        }


        async void OnAddaMealButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"AddAMeal?source=AddAMeal");
        }
        private async Task LoadUserMacros()
        {
            try
            {
                var userMacros = await App.Database.GetUserMacrosAsync();

                if (userMacros != null)
                {

                    caloriesLabel.Text = $"Calories= {userMacros.CaloriesConsumed}/{userMacros.DailyCaloricLimit} kcal";
                    caloriesProgresBar.Progress = Math.Round(Math.Min((double)userMacros.CaloriesConsumed / userMacros.DailyCaloricLimit, 1), 2);

                    carbsLabel.Text = $"Carbs= {userMacros.CarbsConsumed}/{userMacros.DailyCarbsLimit} grams";
                    carbsProgresBar.Progress = Math.Round(Math.Min((double)userMacros.CarbsConsumed / userMacros.DailyCarbsLimit, 1), 2);

                    proteinLabel.Text = $"Protein= {userMacros.ProteinConsumed}/{userMacros.DailyProteinLimit} grams";
                    proteinProgresBar.Progress = Math.Round(Math.Min((double)userMacros.ProteinConsumed / userMacros.DailyProteinLimit, 1), 2);

                    fatsLabel.Text = $"Fats= {userMacros.FatsConsumed}/{userMacros.DailyFatsLimit} grams";
                    fatsProgresBar.Progress = Math.Round(Math.Min((double)userMacros.FatsConsumed / userMacros.DailyFatsLimit, 1), 2);

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No user macros found", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", ex.ToString(), "cancel");
            }
        }
    }
}


















