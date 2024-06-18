using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;
using DietApp;
using DietApp.Models;
using Xamarin.Forms;

namespace DietApp.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class AddEdditRecipe : ContentPage
    {
        int _defoultMealServingSize = 100;
        UserMacros _userMacros;
        public string ItemId
        {

            set
            {
                LoadNote(value);
            }
        }

        public AddEdditRecipe()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Note.
            BindingContext = new Recipe();
        }

        async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                Recipe Recipe = await App.Database.GetNoteAsync(id);
                BindingContext = Recipe;



                var ingredients = await App.Database.GetIngredientsForRecipeAsync(Recipe.ID);
                ingredientListCollectionView.ItemsSource = ingredients;
                CalculateRecipeMacros(Recipe, ingredients);


                //string test="";
                //for (int i = 0; i < Recipe.Ingredients.Count; i++)
                //{

                //    test = test + Recipe.Ingredients[i].MealName + ", ";
                //}

                //await DisplayAlert("Sukses z AddAIgredient", "składniki: " + test, "cancel");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var Recipe = (Recipe)BindingContext;

            Recipe.Date = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(Recipe.RecipeName))
                await App.Database.SaveNoteAsync(Recipe);
            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnAddaIngredientButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var Recipe = (Recipe)BindingContext;
                var addIngredientPage = new AddAMeal(Convert.ToInt32(Recipe.ID));
                await Navigation.PushAsync(addIngredientPage);
            }
            catch (Exception ex)
            {

                await DisplayAlert("bład", ex.ToString(), "cancel");
            }
        }
        async private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var Recipe = (Recipe)BindingContext;
            var ingredients = await App.Database.GetIngredientsForRecipeAsync(Recipe.ID);
           CalculateRecipeMacros(Recipe, ingredients);
        }
        async void CalculateRecipeMacros(Recipe Recipe, List<RecipeIngredients> ingredients)
        {
            Recipe.RecipeCalories = 0;
            Recipe.RecipeCarbs = 0;
            Recipe.RecipeProtein = 0;
            Recipe.RecipeFats = 0;
            foreach (var item in ingredients)
            {
                Recipe.RecipeCalories += item.MealCalories * item.QuantityInGrams / _defoultMealServingSize;
                Recipe.RecipeCarbs += item.MealCarbs * item.QuantityInGrams / _defoultMealServingSize;
                Recipe.RecipeProtein += item.MealProtein * item.QuantityInGrams / _defoultMealServingSize;
                Recipe.RecipeFats += item.MealFats * item.QuantityInGrams / _defoultMealServingSize;
            }
            await App.Database.SaveNoteAsync(Recipe);
        }

        async void OnConsumeThisRecipeClicked(object sender, EventArgs e)
        {
            var Recipe = (Recipe)BindingContext;
            var result =  await DisplayAlert("Did you ate this dish?", $"Total macros that will be added: Calories:{Recipe.RecipeCalories}, Carbs: {Recipe.RecipeCarbs}, Protein: {Recipe.RecipeProtein}, Fats: {Recipe.RecipeFats}", "OK", "Cancel");

            if (result==true)
            {
               await CalculateConsumedCalories(Recipe);
            }

            await Shell.Current.GoToAsync("//HomePage");


        }

        private async Task CalculateConsumedCalories(Recipe Recipe)
        {
            _userMacros = await App.Database.GetUserMacrosAsync();
            _userMacros.CaloriesConsumed += Recipe.RecipeCalories;
            _userMacros.CarbsConsumed += Recipe.RecipeCarbs;
            _userMacros.ProteinConsumed += Recipe.RecipeProtein;
            _userMacros.FatsConsumed += Recipe.RecipeFats;

            await _userMacros.SaveMacrosAsync();
            await DisplayAlert("Updated Macros", $"Calories: {_userMacros.CaloriesConsumed}, Carbs: {_userMacros.CarbsConsumed}, Protein: {_userMacros.ProteinConsumed}, Fats: {_userMacros.FatsConsumed}", "OK", "Cancel");
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var Recipe = (Recipe)BindingContext;

            await App.Database.DeleteNoteAsync(Recipe);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}





            
