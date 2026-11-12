using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;

namespace Cinema_Doomsday.Utilidades
{
    public static class GeneradorIds
    {
        public static string SiguienteCliente(
            IEnumerable<Cliente> clientes)
        {
            return Siguiente(
                clientes,
                "CLI");
        }

        public static string SiguientePelicula(
            IEnumerable<Pelicula> peliculas)
        {
            return Siguiente(
                peliculas,
                "PEL");
        }

        public static string SiguienteSala(
            IEnumerable<Sala> salas)
        {
            return Siguiente(
                salas,
                "SAL");
        }

        public static string SiguienteFuncion(
            IEnumerable<Funcion> funciones)
        {
            return Siguiente(
                funciones,
                "FUN");
        }

        static string Siguiente<T>(
            IEnumerable<T> entidades,
            string prefijo)
            where T : EntidadBase
        {
            int siguiente =
                entidades
                .Select(
                    x =>
                    ObtenerNumero(
                        x.Id,
                        prefijo))
                .DefaultIfEmpty(0)
                .Max() + 1;

            return
                prefijo +
                siguiente.ToString(
                    "D3");
        }

        static int ObtenerNumero(
            string id,
            string prefijo)
        {
            if (string.IsNullOrWhiteSpace(id) ||
                string.IsNullOrWhiteSpace(prefijo))
            {
                return 0;
            }

            if (!id.StartsWith(
                prefijo,
                StringComparison.OrdinalIgnoreCase))
            {
                return 0;
            }

            string parteNumerica =
                id.Substring(
                    prefijo.Length);

            int numero;

            if (int.TryParse(
                parteNumerica,
                out numero))
            {
                return numero;
            }

            return 0;
        }
    }
}
