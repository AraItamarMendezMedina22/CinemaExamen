using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Interfaces
{
    public interface IActivable
    {
        bool Activo { get; }

        void Activar();

        void Desactivar();
    }
}
