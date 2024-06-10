using System;
using System.Collections.Generic;
using System.IO;
using DietApp;
using DietApp.Models;
using Xamarin.Forms;

namespace DietApp.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class AddEdditRecepie : ContentPage
    {
        int _defoultMealServingSize = 100;
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
       async void CalculateRecepieMacros(Recepie recepie, List<RecepieIngredients> ingredients)
        {
            
            foreach (var item in ingredients)
            {
                recepie.RecepieCalories += item.MealCalories * item.QuantityInGrams / _defoultMealServingSize;
                recepie.RecepieCarbs += item.MealCarbs * item.QuantityInGrams / _defoultMealServingSize;
                recepie.RecepieProtein += item.MealProtein * item.QuantityInGrams / _defoultMealServingSize;
                recepie.RecepieFats += item.MealFats * item.QuantityInGrams / _defoultMealServingSize;
            }
            await App.Database.SaveNoteAsync(recepie);
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