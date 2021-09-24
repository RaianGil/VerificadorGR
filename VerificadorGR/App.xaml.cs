using System;
using VerificadorGR.Model;
using VerificadorGR.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VerificadorGR
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new InicioPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
