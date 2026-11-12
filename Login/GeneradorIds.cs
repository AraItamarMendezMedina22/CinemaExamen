using System;
using System.Linq;
using Cinema_Doomsday.Modelos;

namespace Cinema_Doomsday.Login
{
    public static class GeneradorIds
    {
        public static string SiguienteUsuario(
            ContextoDatos datos)
        {
            int siguiente =
                datos.Usuarios
                .Select(
                    x => ObtenerNumero(x.Id))
                .DefaultIfEmpty(0)
                .Max() + 1;

            return
                "USR" +
                siguiente.ToString("D3");
        }

        static int ObtenerNumero(
            string id)
        {
            if (string.IsNullOrWhiteSpace(id) ||
                id.Length <= 3)
            {
                return 0;
            }

            int numero;

            if (int.TryParse(
                id.Substring(3),
                out numero))
            {
                return numero;
            }

            return 0;
        }
    }
}
