using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DietApp;
using DietApp.Models;
using DietApp.Views;
using Xamarin.Forms;

namespace DietApp.Views
{
    public partial class RecipePage : ContentPage
    {
        public RecipePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetNotesAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddEdditRecipe));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Recipe Recipe = (Recipe)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(AddEdditRecipe)}?{nameof(AddEdditRecipe.ItemId)}={Recipe.ID.ToString()}");
            }
        }

    }
}

           

            

                