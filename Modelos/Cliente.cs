using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Modelos
{
    public class Cliente : EntidadBase
    {
        public string Nombre
        {
            get;
            private set;
        }

        public string Correo
        {
            get;
            private set;
        }

        public string Telefono
        {
            get;
            private set;
        }

        public Cliente(
            string id,
            string nombre,
            string correo,
            string telefono)
            : base(id)
        {
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
        }

        public void Editar(
            string nombre,
            string correo)
        {
            Nombre = nombre;
            Correo = correo;
        }

        public void ActualizarTelefono(
            string telefono)
        {
            Telefono = telefono;
        }
    }
}
