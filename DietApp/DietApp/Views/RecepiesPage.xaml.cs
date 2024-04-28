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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var recepies = new List<Recepie>();

            // Create a Note object from each file.
            var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
            foreach (var filename in files)
            {
                recepies.Add(new Recepie
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                });
            }

            // Set the data source for the CollectionView to a
            // sorted collection of notes.
            collectionView.ItemsSource = recepies
                .OrderBy(d => d.Date)
                .ToList();
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
                // Navigate to the NoteEntryPage, passing the filename as a query parameter.
                Recepie recepie = (Recepie)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(AddEdditRecepie)}?{nameof(AddEdditRecepie.ItemId)}={recepie.Filename}");
            }
        }
    }
}