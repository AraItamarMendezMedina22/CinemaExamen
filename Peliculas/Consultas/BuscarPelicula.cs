using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Peliculas
{
    public static class BuscarPelicula
    {
        public static void Ejecutar(
            ContextoDatos datos)
        {
            string id =
                Validador.LeerTexto(
                    "ID película: ",
                    3,
                    12)
                .ToUpper();

            Pelicula pelicula =
                datos.Peliculas
                .FirstOrDefault(
                    x => x.Id == id);

            if (pelicula == null)
            {
                ConsolaUI.Error(
                    "Película inexistente.");

                return;
            }

            Console.WriteLine(
                "Título: " +
                pelicula.Titulo);

            Console.WriteLine(
                "Duración: " +
                pelicula.Duracion +
                " minutos");

            Console.WriteLine(
                "Género: " +
                pelicula.Genero);

            Console.WriteLine(
                "Clasificación: " +
                pelicula.Clasificacion);

            Console.WriteLine(
                "Estado: " +
                (pelicula.Activo
                    ? "Activo"
                    : "Inactivo"));
        }
    }
}
