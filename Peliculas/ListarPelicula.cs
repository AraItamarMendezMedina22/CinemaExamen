using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Peliculas
{
    public static class ListarPeliculas
    {
        public static void Ejecutar(
            ContextoDatos datos)
        {
            Console.WriteLine(
                "1. Activas");

            Console.WriteLine(
                "2. Inactivas");

            Console.WriteLine(
                "3. Todas");

            int opcion =
                Validador.LeerOpcion(
                    1,
                    3);

            IEnumerable<Pelicula> lista =
                datos.Peliculas;

            if (opcion == 1)
                lista =
                    lista.Where(
                        x => x.Activo);

            if (opcion == 2)
                lista =
                    lista.Where(
                        x => !x.Activo);

            List<Pelicula> resultado =
                lista.ToList();

            if (resultado.Count == 0)
            {
                ConsolaUI.Info(
                    "No hay películas.");

                return;
            }

            foreach (
                Pelicula pelicula
                in resultado)
            {
                Console.WriteLine(
                    pelicula.Id +
                    " | " +
                    pelicula.Titulo +
                    " | " +
                    pelicula.Duracion +
                    " min | " +
                    pelicula.Clasificacion +
                    " | " +
                    (pelicula.Activo
                        ? "Activo"
                        : "Inactivo"));
            }
        }
    }
}
