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
    public partial class PrecioDatosOfertaPage : ContentPage
    {
        public PrecioDatosOfertaPage(Producto producto)
        {
            InitializeComponent();
            

            lblDescripcion.Text = producto.Descripcion;
            lblPrecio.Text = producto.Precio;
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {

                Device.BeginInvokeOnMainThread(() => Cerrar());

                return true;

            });
            lblOferta.Text = "";
            foreach (var item in producto.OfertaDescripcion)
            {
                lblOferta.Text += item + "&#x0a;&#x0a;";
            }
        }
        public async void Cerrar()
        {
            await this.Navigation.PopModalAsync();
        }


    }
}