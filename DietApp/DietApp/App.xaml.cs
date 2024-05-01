using System;
using System.IO;
using DietApp;
using DietApp.Data;
using Xamarin.Forms;

namespace DietApp
{
    public partial class App : Application
    {
        static RecepieDatabase database;

        // Create the database connection as a singleton.
        public static RecepieDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new RecepieDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
