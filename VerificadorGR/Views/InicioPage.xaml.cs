using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VerificadorGR.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VerificadorGR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioPage : ContentPage
    {
        public InicioPage()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {

                //Device.BeginInvokeOnMainThread(() => entCodigo.Focus());

                return true;

            });
            entCodigo.Focus();
        }
        protected override void OnAppearing() //Cuando aparezca la pagina, refrescamos.
        {
            entCodigo.Focus();
        }
        
       

        private async void Entry_Completed(object sender, EventArgs e)
        {
            try
            
            {
                HttpClient _client = new HttpClient();
                _client.Timeout = new TimeSpan(0, 0, 100);
                string url = $"http://{Preferences.Get("ServerIP", "192.168.1.158") }:{Preferences.Get("ServerPORT", "4441")}/verifier?data={entCodigo.Text}&source=UPCA";

                var contentSync = _client.GetStringAsync(url);
                contentSync.Wait();
                Producto producto = new Producto();
                if (contentSync.Result == "Producto no encontrado<br/>Código: 1")
                {

                    producto.Descripcion = "Producto no encontrado";
                    producto.Precio = "0";
                    if (entCodigo.Text.Length == 11)
                    {
                        string urlcedula = $"http://{Preferences.Get("ServerIP", "192.168.1.158") }:{Preferences.Get("ServerPORT", "4441")}/verifier?data=a{entCodigo.Text}b&source=SCN1:CODABAR";
                        var contentSyncCedula = _client.GetStringAsync(urlcedula);
                        contentSyncCedula.Wait();
                        Puntos persona = new Puntos();
                        if (contentSyncCedula.Result != "Producto no encontrado")
                        {

                            var Cliente = contentSyncCedula.Result;
                            Cliente = Cliente.Replace("<div><h1 style=\"text-align: center; font-size: 26px; margin-bottom: 8px;\">", "");
                            var index = Cliente.IndexOf("</h1><p style=\"text-align: center; font-size: 26px; font-weight: bold;");
                            Cliente = Cliente.Substring(0, index);

                            var Puntos = contentSyncCedula.Result;
                            var indexpuntos = Puntos.IndexOf("</strong>") + 9;
                            var endpuntos = Puntos.IndexOf("</p></div>");
                            Puntos = Puntos.Substring(indexpuntos, endpuntos - indexpuntos);

                            persona.Nombre = Cliente;
                            persona.PuntosAcumulados = Puntos;


                            await Navigation.PushModalAsync(new PuntosDatosPage(persona));
                        }
                        else
                        {
                            persona.Nombre = "Cliente no encontrado.";
                            persona.PuntosAcumulados = "0";
                            await Navigation.PushModalAsync(new PuntosDatosPage(persona));
                        }


                    }
                    else
                    {
                        //producto no encontrado
                        await Navigation.PushModalAsync(new PrecioDatosPage(producto));
                    }

                    //await Navigation.PushAsync(new PrecioDatosPage(producto));
                }
                else if (contentSync.Result.Contains("<h4>Ofertas sugeridas:</h4>"))
                {
                    var productoDes = contentSync.Result;

                    productoDes = productoDes.Replace("<div id=\"itemDesc\">", "");
                    var index = productoDes.IndexOf("</div><div id=\"regularPrice\"><span class=\"label\">");
                    productoDes = productoDes.Substring(0, index);

                    var precio = contentSync.Result;
                    precio = precio.Replace("</div><div id=\"regularPrice\"><span class=\"label\">", "");
                    var indexprecio = precio.IndexOf("</span>") + 7;

                    var endprecio = precio.IndexOf("</div>");
                    precio = precio.Substring(indexprecio, endprecio - indexprecio);

                    var ofertaDescripcion = contentSync.Result;
                    int counter = Regex.Matches(ofertaDescripcion, "<li>").Count;
                    producto.Descripcion = productoDes;
                    producto.Precio = precio;
                    producto.OfertaDescripcion = new List<string>();

                    var oferta = "";
                    for (int i = 0; i < counter; i++)
                    {
                        oferta = "";
                        var OfertaIndex = ofertaDescripcion.IndexOf("<li>");
                        var OfertaEND = ofertaDescripcion.IndexOf("</li>");
                        oferta = ofertaDescripcion.Substring(OfertaIndex + 4, OfertaEND - (OfertaIndex + 4));
                        ofertaDescripcion = ofertaDescripcion.Remove(OfertaIndex, (OfertaEND + 4) - (OfertaIndex));
                        producto.OfertaDescripcion.Add(oferta);
                    }

                    await Navigation.PushModalAsync(new PrecioDatosOfertaPage(producto));




                }
                else
                {
                    if (entCodigo.Text.Length == 13 && entCodigo.Text.StartsWith("25"))
                    {
                        Puntos persona = new Puntos();
                        string urlcedula = $"http://{Preferences.Get("ServerIP", "192.168.1.158") }:{Preferences.Get("ServerPORT", "4441")}/verifier?data={entCodigo.Text}&source=UPCA";
                        var contentSyncCedula = _client.GetStringAsync(urlcedula);
                        contentSyncCedula.Wait();
                        var Cliente = contentSyncCedula.Result;
                        Cliente = Cliente.Replace("<div><h1 style=\"text-align: center; font-size: 26px; margin-bottom: 8px;\">", "");
                        var index = Cliente.IndexOf("</h1><p style=\"text-align: center; font-size: 26px; font-weight: bold;");
                        Cliente = Cliente.Substring(0, index);

                        var Puntos = contentSyncCedula.Result;
                        var indexpuntos = Puntos.IndexOf("</strong>") + 9;
                        var endpuntos = Puntos.IndexOf("</p></div>");
                        Puntos = Puntos.Substring(indexpuntos, endpuntos - indexpuntos);

                        persona.Nombre = Cliente;
                        persona.PuntosAcumulados = Puntos;


                        await Navigation.PushModalAsync(new PuntosDatosPage(persona));
                    }
                    else
                    {
                        var productoDes = contentSync.Result;

                        productoDes = productoDes.Replace("<div id=\"itemDesc\">", "");
                        var index = productoDes.IndexOf("</div><div id=\"regularPrice\"><span class=\"label\">");
                        productoDes = productoDes.Substring(0, index);

                        var precio = contentSync.Result;
                        precio = precio.Replace("</div><div id=\"regularPrice\"><span class=\"label\">", "");
                        var indexprecio = precio.IndexOf("</span>") + 7;

                        var endprecio = precio.IndexOf("</div>");
                        precio = precio.Substring(indexprecio, endprecio - indexprecio);


                        producto.Descripcion = productoDes;
                        producto.Precio = precio;
                        await Navigation.PushModalAsync(new PrecioDatosPage(producto));
                    }
                    
                    //persona.Nombre = Cliente;
                    //persona.PuntosAcumulados = Puntos;
                }







                entCodigo.Text = string.Empty;
            }
            catch (Exception ea)
            {
                Producto producto = new Producto();
                producto.Descripcion = "Producto no encontrado";
                producto.Precio = "0";
                Debug.WriteLine(ea.Message);
                var str = string.Empty;
                entCodigo.Text = string.Empty;
                await Navigation.PushModalAsync(new PrecioDatosPage(producto));
            }
        }

        private async void imgSirena_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new ConfigPage());
            }
            catch (Exception ea)
            {
                var x = ea.Message;
                //throw;
            }
           
        }

        private async void Entry_Completed_1(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new ConfigPage());
            }
            catch (Exception ea)
            {
                var x = ea.Message;
                //throw;
            }
        }
    }
}