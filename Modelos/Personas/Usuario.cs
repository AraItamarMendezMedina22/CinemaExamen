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

        public int IntentosFallidos
        {
            get;
            private set;
        }

        public DateTime? BloqueadoHasta
        {
            get;
            private set;
        }

        public bool EstaBloqueado
        {
            get
            {
                return BloqueadoHasta.HasValue &&
                       BloqueadoHasta.Value >
                       DateTime.Now;
            }
        }

        public void RegistrarFallo()
        {
            IntentosFallidos++;

            if (IntentosFallidos >= 3)
            {
                BloqueadoHasta =
                    DateTime.Now.AddMinutes(5);

                IntentosFallidos = 0;
            }
        }

        public void ReiniciarAcceso()
        {
            IntentosFallidos = 0;
            BloqueadoHasta = null;
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
