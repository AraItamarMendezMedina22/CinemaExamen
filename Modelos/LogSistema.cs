using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Modelos
{
    public class LogSistema
    {
        public DateTime Fecha
        {
            get;
            private set;
        }

        public string Usuario
        {
            get;
            private set;
        }

        public string Accion
        {
            get;
            private set;
        }

        public string Registro
        {
            get;
            private set;
        }

        public LogSistema(
            DateTime fecha,
            string usuario,
            string accion,
            string registro)
        {
            Fecha = fecha;
            Usuario = usuario;
            Accion = accion;
            Registro = registro;
        }
    }
}
