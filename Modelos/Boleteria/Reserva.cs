using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Modelos
{
    public class Reserva : EntidadBase
    {
        public string FuncionId
        {
            get;
            private set;
        }

        public string ClienteId
        {
            get;
            private set;
        }

        public List<string> ListaDeAsientos
        {
            get;
            private set;
        }

        public decimal TotalPagar
        {
            get;
            private set;
        }

        public string MetodoPago
        {
            get;
            private set;
        }

        public string DetallePago
        {
            get;
            private set;
        }

        public decimal Cambio
        {
            get;
            private set;
        }

        public DateTime FechaCompra
        {
            get;
            private set;
        }

        public Reserva(
            string id,
            string funcionId,
            string clienteId,
            List<string> listaDeAsientos,
            decimal totalPagar,
            string metodoPago,
            string detallePago,
            decimal cambio)
            : base(id)
        {
            FuncionId = funcionId;
            ClienteId = clienteId;
            ListaDeAsientos = listaDeAsientos;
            TotalPagar = totalPagar;
            MetodoPago = metodoPago;
            DetallePago = detallePago;
            Cambio = cambio;
            FechaCompra = DateTime.Now;
        }

        internal void RestaurarFecha(
            DateTime fecha)
        {
            FechaCompra = fecha;
        }
    }
}
