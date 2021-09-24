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
    public partial class PrecioDatosPage : ContentPage
    {

        private Producto producto;


        public async void Cerrar()
        {
            await this.Navigation.PopModalAsync();
        }


        public PrecioDatosPage(Producto producto)
        {
            InitializeComponent();
            this.producto = producto;

            lblDescripcion.Text = producto.Descripcion;
            lblPrecio.Text = producto.Precio;
            

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {

                Device.BeginInvokeOnMainThread(() => Cerrar());

                return true;

            });
        }
    }
}