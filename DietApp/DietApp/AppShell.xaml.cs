using DietApp.Views;

using Xamarin.Forms;

namespace DietApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddEdditRecepie), typeof(AddEdditRecepie));
            Routing.RegisterRoute(nameof(PhenotypeAndGoal), typeof(PhenotypeAndGoal));
        }
    }
}