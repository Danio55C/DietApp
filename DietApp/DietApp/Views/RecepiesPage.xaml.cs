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
    public partial class RecepiesPage : ContentPage
    {
        public RecepiesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.Database.GetNotesAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the NoteEntryPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(AddEdditRecepie));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                Recepie recepie = (Recepie)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(AddEdditRecepie)}?{nameof(AddEdditRecepie.ItemId)}={recepie.ID.ToString()}");
            }
        }
    }
}