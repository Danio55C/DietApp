using DietApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Reflection;

namespace DietApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhenotypeAndGoal : ContentPage
    {


        public PhenotypeAndGoal()
        {
            InitializeComponent();
            genderPicker.ItemsSource = genderList;

            tDEpicker.ItemsSource = Enum.GetValues(typeof(LifeStyleTDE))
                            .Cast<LifeStyleTDE>()
                            .Select(value => EnumHelper.GetDescription(value))
                            .ToList();
            goalPicker.ItemsSource = goalList;



        }

        List<string> genderList = new List<string>
        {
            "Female","Male"
        };

        List<string> goalList = new List<string>
        {
            "lose weight","gain weight","maintain weight"
        };
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {

            UserData userdate = new UserData();
            userdate.gender = (string)genderPicker.SelectedItem;
            userdate.currentWeight = Int32.Parse(weightEntry.Text);
            userdate.height = Int32.Parse(heightEntry.Text);
            userdate.age = Int32.Parse(ageEntry.Text);
        }













        //zbinduj wiek wzrost i wage dodaj liste goal oraz tryb życia
    }
}