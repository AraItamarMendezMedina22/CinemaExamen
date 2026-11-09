using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Modelos
{
    public class ContextoDatos
    {
        public List<Usuario> Usuarios
        {
            get;
            private set;
        }

        public List<Pelicula> Peliculas
        {
            get;
            private set;
        }

        public List<Sala> Salas
        {
            get;
            private set;
        }

        public List<Funcion> Funciones
        {
            get;
            private set;
        }

        public List<Asiento> Asientos
        {
            get;
            private set;
        }

        public List<Cliente> Clientes
        {
            get;
            private set;
        }

        public List<Reserva> Reservas
        {
            get;
            private set;
        }

        public ContextoDatos()
        {
            Usuarios =
                new List<Usuario>();

            Peliculas =
                new List<Pelicula>();

            Salas =
                new List<Sala>();

            Funciones =
                new List<Funcion>();

            Asientos =
                new List<Asiento>();

            Clientes =
                new List<Cliente>();

            Reservas =
                new List<Reserva>();
        }
    }
}
