using DietApp.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DietApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
           
            await Shell.Current.GoToAsync(nameof(PhenotypeAndGoal));
        }
    }
}