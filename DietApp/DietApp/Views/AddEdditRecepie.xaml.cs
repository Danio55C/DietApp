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

        void LoadNote(string filename)
        {
            try
            {
                // Retrieve the note and set it as the BindingContext of the page.
                Recepie recepie = new Recepie
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                };
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

            if (string.IsNullOrWhiteSpace(recepie.Filename))
            {
                // Save the file.
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(filename, recepie.Text);
            }
            else
            {
                // Update the file.
                File.WriteAllText(recepie.Filename, recepie.Text);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Recepie)BindingContext;

            // Delete the file.
            if (File.Exists(note.Filename))
            {
                File.Delete(note.Filename);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}