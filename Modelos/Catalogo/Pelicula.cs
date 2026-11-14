using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Modelos
{
    public class Pelicula : EntidadBase
    {
        public string Titulo
        {
            get;
            private set;
        }

        public int Duracion
        {
            get;
            private set;
        }

        public string Genero
        {
            get;
            private set;
        }

        public string Clasificacion
        {
            get;
            private set;
        }

        public Pelicula(
            string id,
            string titulo,
            int duracion,
            string genero,
            string clasificacion)
            : base(id)
        {
            Titulo = titulo;
            Duracion = duracion;
            Genero = genero;
            Clasificacion = clasificacion;
        }

        public void Editar(
            string titulo,
            string genero,
            string clasificacion)
        {
            Titulo = titulo;
            Genero = genero;
            Clasificacion = clasificacion;
        }

        public void ActualizarDuracion(
            int duracion)
        {
            Duracion = duracion;
        }
    }
}
