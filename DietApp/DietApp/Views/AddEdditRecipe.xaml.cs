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
        async void OnConsumeThisRecipeClicked(object sender, EventArgs e)
        {
            var Recipe = (Recipe)BindingContext;
            var result = await DisplayAlert("Did you ate this dish?", $"Total macros that will be added: Calories:{Recipe.RecipeCalories}, Carbs: {Recipe.RecipeCarbs}, Protein: {Recipe.RecipeProtein}, Fats: {Recipe.RecipeFats}", "OK", "Cancel");

            if (result == true)
                await CalculateConsumedCalories(Recipe);
            
            await Shell.Current.GoToAsync("//HomePage");
        }


        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var Recipe = (Recipe)BindingContext;

            await App.Database.DeleteNoteAsync(Recipe);
            await Shell.Current.GoToAsync("..");
        }
            

        async void OnGenerateShoppingListButtonClicked(object sender, EventArgs e)
        {
            var recipe = (Recipe)BindingContext;
            var ingredients = await App.Database.GetIngredientsForRecipeAsync(recipe.ID);
            recipe.RecipePrice = 0;
            
            var stackLayout = new StackLayout
            {
                Padding = new Thickness(20)
            };

            stackLayout.Children.Add(new Label
            {
                Text = "Your Shopping List: \n",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold
            });

            foreach (var item in ingredients)
            {
                recipe.RecipePrice += item.MealPrice;
                
                stackLayout.Children.Add(new Label { Text = $"{item.MealName}", FontSize = 20, });
                stackLayout.Children.Add(new Label { Text = $"quantity: {item.QuantityInGrams} grams", FontSize = 10, });
                stackLayout.Children.Add(new Label { Text = $"cost: {item.MealPrice} zł \n", FontSize = 10, });
            }

            stackLayout.Children.Add(new Button
            {
                Text = "Zamknij",
                Command = new Command(async () => await Navigation.PopModalAsync())
            });
            var scrollView = new ScrollView
            {
                Content = stackLayout
            };
            stackLayout.Children.Add(new Label
            {
                Text =$"Total Dish cost: {recipe.RecipePrice.ToString()} zł",
                FontSize = 30,

            });
            var modalPage = new ContentPage
            {
                Title = "Lista zakupów",
                Content = scrollView
            };
            await Navigation.PushModalAsync(modalPage);
        }



    }
}





            






        






            
















