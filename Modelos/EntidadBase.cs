using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Doomsday.Interfaces;


namespace Cinema_Doomsday.Modelos
{
    public abstract class EntidadBase : IActivable
    {
        public string Id
        {
            get;
            protected set;
        }

        public bool Activo
        {
            get;
            protected set;
        }

        protected EntidadBase(string id)
        {
            Id = id;
            Activo = true;
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
