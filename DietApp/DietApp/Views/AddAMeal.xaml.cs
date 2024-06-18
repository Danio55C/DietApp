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
    [QueryProperty(nameof(Source), "source")]
    public partial class AddAMeal : ContentPage
    {
        UserMacros _userMacros;
        int _defoultMealServingSize = 100;

        private int _recipeId;


        private string _source;

        public string Source
        {
            get => _source;
            set
            {
                _source = value;

            }
        }

        public AddAMeal()
        {
            InitializeComponent();
            BindingContext = new Meal();
        }

        public AddAMeal(int RecipeId)
        {
            InitializeComponent();
            _recipeId = RecipeId;
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
            try
            {

                var meal = (Meal)BindingContext;
                if (meal == null)
                {
                    await DisplayAlert("Error", "Meal is null", "OK");
                    return;
                }

                meal.Date = DateTime.UtcNow;
                if (!string.IsNullOrWhiteSpace(meal.MealName))
                    await App.Database.SaveMealAsync(meal);



                if (_source == "AddAMeal")
                {


                    string result = await DisplayPromptAsync("Did you consumed this meal?", $"Enter the serving size for {meal.MealName}:", "OK", "Cancel", null, 4);
                    if (int.TryParse(result, out int servingSize))
                        await CalculateConsumedCalories(meal, servingSize);
                }
                else
                {

                    var mealIngredient = new RecipeIngredients
                    {
                        MealName = meal.MealName,
                        MealCalories = meal.MealCalories,
                        MealCarbs = meal.MealCarbs,
                        MealProtein = meal.MealProtein,
                        MealFats = meal.MealFats,
                        RecipeId = _recipeId
                    };

                    var Recipe = await App.Database.GetNoteAsync(_recipeId);


                    string result = await DisplayPromptAsync("Quantiy", $"Enter the serving size for {mealIngredient.MealName}:", "OK", "Cancel", null, 4);
                    if (int.TryParse(result, out int servingSize))
                        mealIngredient.QuantityInGrams = servingSize;

                    Recipe.Ingredients.Add(mealIngredient);
                    await App.Database.SaveNoteAsync(Recipe);
                    await App.Database.SaveRecipeIngredientAsync(mealIngredient);

                    await Shell.Current.GoToAsync("..");
                    //await DisplayAlert("Sukses z AddAIgredient", Recipe.In, "cancel");

                    // Navigate backwards
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("bład", ex.ToString(), "cancel");
            }
        }


        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_source == "AddAMeal")
            {

                var selectedMeal = e.CurrentSelection.FirstOrDefault() as Meal;
                if (e.CurrentSelection != null)
                {
                    string result = await DisplayPromptAsync("Did you consumed this meal?", $"Enter the serving size for {selectedMeal.MealName}:", "OK", "Cancel", null, 4);
                    if (int.TryParse(result, out int servingSize))
                        await CalculateConsumedCalories(selectedMeal, servingSize);
                }
            }
            else
            {
                try
                {
                    await DisplayAlert("id", _recipeId.ToString(), "cancel");



                    var selectedIngredient = e.CurrentSelection.FirstOrDefault() as Meal;
                    if (selectedIngredient != null)
                    {
                        // Tworzenie nowego obiektu RecipeIngredients na podstawie selectedIngredient
                        var mealIngredient = new RecipeIngredients
                        {
                            MealName = selectedIngredient.MealName,
                            MealCalories = selectedIngredient.MealCalories,
                            MealCarbs = selectedIngredient.MealCarbs,
                            MealProtein = selectedIngredient.MealProtein,
                            MealFats = selectedIngredient.MealFats,
                            RecipeId = _recipeId
                        };


                        var Recipe = await App.Database.GetNoteAsync(_recipeId);

                        string result = await DisplayPromptAsync("Quantiy", $"Enter the serving size for {mealIngredient.MealName}:", "OK", "Cancel", null, 4);
                        if (int.TryParse(result, out int servingSize))
                        {
                            mealIngredient.QuantityInGrams = servingSize;



                            Recipe.Ingredients.Add(mealIngredient);
                            await App.Database.SaveRecipeIngredientAsync(mealIngredient);
                            await App.Database.SaveNoteAsync(Recipe);
                        }
                        //await DisplayAlert("Sukses z AddAIgredient", Recipe.In, "cancel");

                        // Navigate backwards
                        await Shell.Current.GoToAsync("..");
                    }
                }
                catch (Exception ex)
                {

                    await DisplayAlert("bład", ex.ToString(), "cancel");
                }
            }
        }

        async void OnGoToRecipesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//RecipePage");
        }


        private async Task CalculateConsumedCalories(Meal meal, int servingSize)
        {
            _userMacros.CaloriesConsumed += meal.MealCalories * servingSize / _defoultMealServingSize;
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









































