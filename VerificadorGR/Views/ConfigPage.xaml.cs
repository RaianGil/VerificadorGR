using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VerificadorGR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigPage : ContentPage
    {
        public ConfigPage()
        {
            InitializeComponent();

        }
        protected override void OnAppearing() //Cuando aparezca la pagina, refrescamos.
        {
            ServerIP.Text = Preferences.Get("ServerIP", "10.0.2.2");
            ServerPORT.Text = Preferences.Get("ServerPORT", "8080");
        }
        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("ServerIP", ServerIP.Text);
            Preferences.Set("ServerPORT", ServerPORT.Text);

            await Navigation.PopModalAsync();
        }
    }
}