using System;
using Cinema_Doomsday.Interfaces;

namespace Cinema_Doomsday.Modelos
{
    public abstract class EntidadBase : IActivable
    {
        public string Id
        {
            get;
            private set;
        }

        public bool Activo
        {
            get;
            private set;
        }

        protected EntidadBase(
            string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(
                    "El identificador es obligatorio.",
                    "id");
            }

            Id =
                id.Trim()
                .ToUpperInvariant();

            Activo =
                true;
        }

        public void Activar()
        {
            Activo = true;
        }

        public void Desactivar()
        {
            Activo = false;
        }
    }
}
