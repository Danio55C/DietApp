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
        public AddAMeal()
        {
            InitializeComponent();
            BindingContext = new Meal();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            mealCollectionView.ItemsSource = await App.Database.GetMealsAsync();
        }


        async void OnAddMealButtonClicked(object sender, EventArgs e)
        {
            var meal = (Meal)BindingContext;

            meal.Date = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(meal.MealName))
                await App.Database.SaveMealAsync(meal);
        }

    }
}
            
            