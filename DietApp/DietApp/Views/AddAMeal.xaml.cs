using DietApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAMeal : ContentPage
    {
        UserMacros _userMacros;
        int _defoultMealServingSize=100;
        public AddAMeal()
        {
                InitializeComponent();
            
                BindingContext = new Meal();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            mealCollectionView.ItemsSource = await App.Database.GetMealsAsync();
            _userMacros = await App.Database.GetUserMacrosAsync();
        }


        async void OnAddMealButtonClicked(object sender, EventArgs e)
        {
            var meal = (Meal)BindingContext;

            meal.Date = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(meal.MealName))
            {
                await App.Database.SaveMealAsync(meal);

                string result = await DisplayPromptAsync("Enter Quantity", $"Enter the quantity for {meal.MealName}:", "OK", "Cancel", null, -1);
                if (int.TryParse(result, out int servingSize))
                {

                    await CalculateConsumedCalories(meal, servingSize);
                }

            }
            
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMeal = e.CurrentSelection.FirstOrDefault() as Meal;
            if (e.CurrentSelection != null)
            {
                string result = await DisplayPromptAsync("Enter Quantity", $"Enter the quantity for {selectedMeal.MealName}:", "OK", "Cancel", null, -1);
                if (int.TryParse(result, out int servingSize))
                {

                await CalculateConsumedCalories(selectedMeal, servingSize);
                }

            }  
        }







        private async Task CalculateConsumedCalories(Meal meal, int servingSize)
        {
            _userMacros.CaloriesConsumed += meal.MealCalories* servingSize/ _defoultMealServingSize;
            _userMacros.CarbsConsumed += meal.MealCarbs * servingSize / _defoultMealServingSize;
            _userMacros.ProteinConsumed += meal.MealProtein * servingSize / _defoultMealServingSize;
            _userMacros.FatsConsumed += meal.MealFats * servingSize / _defoultMealServingSize;

            await _userMacros.SaveMacrosAsync();
            await DisplayAlert("Updated Macros", $"Calories: {_userMacros.CaloriesConsumed}, Carbs: {_userMacros.CarbsConsumed}, Protein: {_userMacros.ProteinConsumed}, Fats: {_userMacros.FatsConsumed}", "OK", "Cancel");

            await Shell.Current.GoToAsync("..");
        }
                
        async void OnSwipeDeleteClicked(object sender, EventArgs e)
        {
            var meal = (Meal)BindingContext;
            await App.Database.DeleteMealAsync(meal);
        }
           
                
    }
}
           
           
          
            

            
            