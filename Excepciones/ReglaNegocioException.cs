using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Excepciones
{
    public class ReglaNegocioException :
       Exception
    {
        public ReglaNegocioException(
            string mensaje)
            : base(mensaje)
        {
        }
    }
}
