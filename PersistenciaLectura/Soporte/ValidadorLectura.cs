using System;
using System.Collections.Generic;
using System.IO;

namespace Cinema_Doomsday.PersistenciaLectura
{
    public static class ValidadorLectura
    {
        public static int LeerCantidad(BinaryReader r, string entidad)
        {
            int cantidad = r.ReadInt32();

            if (cantidad < 0 || cantidad > 100000)
                throw new InvalidDataException("Cantidad inválida para " + entidad + ": " + cantidad);

            return cantidad;
        }
    }
}
