using DietApp.Data;
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

        protected override async void OnAppearing()
        {


            var userData = await App.Database.GetUserDataAsync();
            if (userData != null)
            {
                userDataListView.Text = userData.UserGoal;
            }
        }
        async void OnButtonClicked(object sender, EventArgs e)
        {
           
            await Shell.Current.GoToAsync(nameof(PhenotypeAndGoal));
        }
    }
}