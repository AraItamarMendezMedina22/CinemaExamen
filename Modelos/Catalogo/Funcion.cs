using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Modelos
{
    public class Funcion : EntidadBase
    {
        public string PeliculaId
        {
            get;
            private set;
        }

        public string SalaId
        {
            get;
            private set;
        }

        public DateTime HoraInicio
        {
            get;
            private set;
        }

        public decimal Precio
        {
            get;
            private set;
        }

        public Funcion(
            string id,
            string peliculaId,
            string salaId,
            DateTime horaInicio,
            decimal precio)
            : base(id)
        {
            PeliculaId = peliculaId;
            SalaId = salaId;
            HoraInicio = horaInicio;
            Precio = precio;
        }

        public void Editar(
            string peliculaId,
            string salaId,
            DateTime horaInicio)
        {
            PeliculaId = peliculaId;
            SalaId = salaId;
            HoraInicio = horaInicio;
        }

        public void ActualizarPrecio(
            decimal precio)
        {
            Precio = precio;
        }
    }
}
