using DietApp.Views;

using Xamarin.Forms;

namespace DietApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddEdditRecipe), typeof(AddEdditRecipe));
            Routing.RegisterRoute(nameof(PhenotypeAndGoal), typeof(PhenotypeAndGoal));
            Routing.RegisterRoute(nameof(AddAMeal), typeof(AddAMeal));
        }
    }
}