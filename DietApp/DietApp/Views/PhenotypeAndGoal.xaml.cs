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
using System.Linq.Expressions;
using System.Security.Cryptography;

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
         void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                GetUsersData();
            }
            catch (Exception ex)
            {

                DisplayAlert("Błąd",ex.ToString(),"cancel");
            }

            //DisplayAlert("Ustawienia zapisano","twoje dane zostały zapisane" , "cancel");
        }

        private void GetUsersData()
        {

            UserData userdate = new UserData();
            userdate.gender = (string)genderPicker.SelectedItem;
            userdate.currentWeight =int.Parse(weightEntry.Text);
            userdate.height = int.Parse(heightEntry.Text);
            userdate.age = int.Parse(ageEntry.Text);
            
            
            switch (tDEpicker.SelectedItem)
            { 
                    case "Nieaktywny/siedzący: prawie brak aktywności w ciągu dnia":
                    userdate.activityIndex = 1.2;
                    break;
                    case 2:
                    userdate.activityIndex = 1.375;
                    break;
                    case 3:
                    userdate.activityIndex = 1.55;
                    break;
                    case 4:
                    userdate.activityIndex = 1.725;
                    break;
                    case 5:
                    userdate.activityIndex = 1.9;
                    break;
                default:
                    DisplayAlert("bŁad", "złe dane", "cancel");
                    break;
            }
            DisplayAlert("Dobrze",$"płeć: {userdate.gender}, waga:{userdate.currentWeight}, wzrost: {userdate.height}, wiek:{userdate.age}, index:{userdate.activityIndex}"  ,"cancel");
        }


















        //zbinduj wiek wzrost i wage dodaj liste goal oraz tryb życia
    }
}