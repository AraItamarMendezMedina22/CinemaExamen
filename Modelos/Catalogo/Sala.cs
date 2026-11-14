using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Modelos
{
    public class Sala : EntidadBase
    {
        public int NumeroSala
        {
            get;
            private set;
        }

        public int Capacidad
        {
            get;
            private set;
        }

        public string TipoSala
        {
            get;
            private set;
        }

        public Sala(
            string id,
            int numeroSala,
            int capacidad,
            string tipoSala)
            : base(id)
        {
            NumeroSala = numeroSala;
            Capacidad = capacidad;
            TipoSala = tipoSala;
        }

        public void Editar(
            int numeroSala,
            string tipoSala)
        {
            NumeroSala = numeroSala;
            TipoSala = tipoSala;
        }

        public void ActualizarCapacidad(
            int capacidad)
        {
            Capacidad = capacidad;
        }
    }
}
