using System;
using System.IO;
using Tarea1_4.Controlador;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarea1_4
{
    public partial class App : Application
    {
        static PersonaControlador database;

        public static PersonaControlador Database
        {
            get
            {
                if (database == null)
                {
                    database = new PersonaControlador(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dbPersona.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
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
