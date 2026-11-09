using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Modelos
{
    public class Usuario : EntidadBase
    {
        public string NombreUsuario
        {
            get;
            private set;
        }

        public string ContrasenaHash
        {
            get;
            private set;
        }

        public string Rol
        {
            get;
            private set;
        }

        public Usuario(
            string id,
            string nombreUsuario,
            string contrasenaHash,
            string rol)
            : base(id)
        {
            NombreUsuario = nombreUsuario;
            ContrasenaHash = contrasenaHash;
            Rol = rol;
        }
    }
}
