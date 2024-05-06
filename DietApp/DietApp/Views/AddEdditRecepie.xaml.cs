using System;
using System.IO;
using DietApp;
using DietApp.Models;
using Xamarin.Forms;

namespace DietApp.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class AddEdditRecepie : ContentPage
    {
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
            if (!string.IsNullOrWhiteSpace(recepie.Text))
            {
                await App.Database.SaveNoteAsync(recepie);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
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