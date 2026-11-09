using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Pagos
{
    public class ResultadoPago
    {
        public string MetodoPago { get; private set; }

        public string Detalle { get; private set; }

        public decimal Cambio { get; private set; }

        public ResultadoPago(
            string metodoPago,
            string detalle,
            decimal cambio)
        {
            MetodoPago = metodoPago;
            Detalle = detalle;
            Cambio = cambio;
        }
    }
}
