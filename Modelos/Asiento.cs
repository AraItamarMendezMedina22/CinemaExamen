using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Modelos
{
    public class Asiento
    {
        public string Id
        {
            get;
            private set;
        }

        public string FuncionId
        {
            get;
            private set;
        }

        public string Fila
        {
            get;
            private set;
        }

        public int Numero
        {
            get;
            private set;
        }

        public bool EstaOcupado
        {
            get;
            private set;
        }

        public Asiento(
            string id,
            string funcionId,
            string fila,
            int numero)
        {
            Id = id;
            FuncionId = funcionId;
            Fila = fila;
            Numero = numero;
            EstaOcupado = false;
        }

        public void Ocupar()
        {
            EstaOcupado = true;
        }

        public void Liberar()
        {
            EstaOcupado = false;
        }

        public string Codigo()
        {
            return Fila + Numero;
        }

        internal void RestaurarEstado(
            bool ocupado)
        {
            EstaOcupado = ocupado;
        }
    }
}
