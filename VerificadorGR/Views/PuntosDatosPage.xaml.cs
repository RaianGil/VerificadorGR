using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerificadorGR.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VerificadorGR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PuntosDatosPage : ContentPage
    {
        private Puntos persona;


        public async void Cerrar()
        {
            await this.Navigation.PopModalAsync();
        }
        public PuntosDatosPage(Puntos persona)
        {
            InitializeComponent();
            this.persona = persona;
            lblCliente.Text = persona.Nombre;
            lblPuntos.Text = "Tiene actualmente " + persona.PuntosAcumulados + " puntos acumulados.";
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {

                Device.BeginInvokeOnMainThread(() => Cerrar());

                return true;

            });
        }
    }
}