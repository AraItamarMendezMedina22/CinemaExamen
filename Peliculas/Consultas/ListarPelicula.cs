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
            List<Pelicula> resultado =
                datos.Peliculas
                .OrderBy(
                    x => x.Id)
                .ToList();

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
