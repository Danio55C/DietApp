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
    public partial class AddEdditRecepie : ContentPage
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

        public AddEdditRecepie()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Note.
            BindingContext = new Recepie();
        }

        async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                Recepie recepie = await App.Database.GetNoteAsync(id);
                BindingContext = recepie;



                var ingredients = await App.Database.GetIngredientsForRecipeAsync(recepie.ID);
                ingredientListCollectionView.ItemsSource = ingredients;
                CalculateRecepieMacros(recepie, ingredients);


                //string test="";
                //for (int i = 0; i < recepie.Ingredients.Count; i++)
                //{

                //    test = test + recepie.Ingredients[i].MealName + ", ";
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
            var recepie = (Recepie)BindingContext;

            recepie.Date = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(recepie.RecepieName))
                await App.Database.SaveNoteAsync(recepie);
            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnAddaIngredientButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var recepie = (Recepie)BindingContext;
                var addIngredientPage = new AddAMeal(Convert.ToInt32(recepie.ID));
                await Navigation.PushAsync(addIngredientPage);
            }
            catch (Exception ex)
            {

                await DisplayAlert("bład", ex.ToString(), "cancel");
            }
        }
        async private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var recepie = (Recepie)BindingContext;
            var ingredients = await App.Database.GetIngredientsForRecipeAsync(recepie.ID);
           CalculateRecepieMacros(recepie, ingredients);
        }
        async void CalculateRecepieMacros(Recepie recepie, List<RecepieIngredients> ingredients)
        {
            recepie.RecepieCalories = 0;
            recepie.RecepieCarbs = 0;
            recepie.RecepieProtein = 0;
            recepie.RecepieFats = 0;
            foreach (var item in ingredients)
            {
                recepie.RecepieCalories += item.MealCalories * item.QuantityInGrams / _defoultMealServingSize;
                recepie.RecepieCarbs += item.MealCarbs * item.QuantityInGrams / _defoultMealServingSize;
                recepie.RecepieProtein += item.MealProtein * item.QuantityInGrams / _defoultMealServingSize;
                recepie.RecepieFats += item.MealFats * item.QuantityInGrams / _defoultMealServingSize;
            }
            await App.Database.SaveNoteAsync(recepie);
        }

        async void OnConsumeThisRecepieClicked(object sender, EventArgs e)
        {
            var recepie = (Recepie)BindingContext;
            var result =  await DisplayAlert("Did you ate this dish?", $"Total macros that will be added: Calories:{recepie.RecepieCalories}, Carbs: {recepie.RecepieCarbs}, Protein: {recepie.RecepieProtein}, Fats: {recepie.RecepieFats}", "OK", "Cancel");

            if (result==true)
            {
               await CalculateConsumedCalories(recepie);
            }

            await Shell.Current.GoToAsync("//HomePage");


        }

        private async Task CalculateConsumedCalories(Recepie recepie)
        {
            _userMacros = await App.Database.GetUserMacrosAsync();
            _userMacros.CaloriesConsumed += recepie.RecepieCalories;
            _userMacros.CarbsConsumed += recepie.RecepieCarbs;
            _userMacros.ProteinConsumed += recepie.RecepieProtein;
            _userMacros.FatsConsumed += recepie.RecepieFats;

            await _userMacros.SaveMacrosAsync();
            await DisplayAlert("Updated Macros", $"Calories: {_userMacros.CaloriesConsumed}, Carbs: {_userMacros.CarbsConsumed}, Protein: {_userMacros.ProteinConsumed}, Fats: {_userMacros.FatsConsumed}", "OK", "Cancel");
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var recepie = (Recepie)BindingContext;

            await App.Database.DeleteNoteAsync(recepie);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}





            
