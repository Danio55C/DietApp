using System;
using System.Diagnostics;
using System.IO;
using DietApp;
using DietApp.Data;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DietApp
{
    public partial class App : Application
    {
        static RecipieDatabase _database;

        // Create the database connection as a singleton.
        public static RecipieDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new RecipieDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DietAppDataBase.db3"));
                    
                }
                return _database;
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
