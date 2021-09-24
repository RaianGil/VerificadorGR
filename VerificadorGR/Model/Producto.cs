using System;
using System.Collections.Generic;
using System.Text;

namespace VerificadorGR.Model
{
    public class Producto
    {
        public string Descripcion { get; set; }
        public string Precio { get; set; }
        public List<string> OfertaDescripcion { get; set; }

    }
}
