using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Modelos
{
    public class Cliente : EntidadBase
    {
        public string Nombre { get; private set; }
        public string Cedula { get; private set; }
        public int Edad { get; private set; }
        public string Correo { get; private set; }
        public string Telefono { get; private set; }
        public DateTime MembresiaHasta { get; private set; }

        public bool MembresiaVigente =>
            MembresiaHasta.Date >= DateTime.Today;

        public Cliente(
            string id,
            string nombre,
            string cedula,
            int edad,
            string correo,
            string telefono,
            DateTime membresiaHasta) : base(id)
        {
            Nombre = nombre;
            Cedula = cedula;
            Edad = edad;
            Correo = correo;
            Telefono = telefono;
            MembresiaHasta = membresiaHasta;
        }

        public void Editar(string nombre, string correo)
        {
            Nombre = nombre;
            Correo = correo;
        }

        public void ActualizarTelefono(string telefono)
        {
            Telefono = telefono;
        }

        public void RenovarMembresia(int meses)
        {
            DateTime inicio =
                MembresiaVigente
                ? MembresiaHasta
                : DateTime.Today;

            MembresiaHasta =
                inicio.AddMonths(meses);
        }
    }
}
