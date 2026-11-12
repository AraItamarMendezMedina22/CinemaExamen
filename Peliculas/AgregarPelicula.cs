using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Peliculas
{

    public static class AgregarPelicula
    {
        public static void Ejecutar(
            ContextoDatos datos,
            string usuario)
        {
            ConsolaUI.Encabezado(
                "AGREGAR PELÍCULA");

             

                string titulo =
                Validador.LeerTexto(
                    "Título: ",
                    2,
                    100);

            if (datos.Peliculas.Any(
                x =>
                x.Titulo.Equals(
                    titulo,
                    StringComparison
                        .OrdinalIgnoreCase)))
            {
                ConsolaUI.Error(
                    "La película ya existe.");

                return;
            }

            int duracion =
                Validador.LeerEntero(
                    "Duración en minutos: ",
                    30,
                    300);

            string genero =
                Validador.LeerTexto(
                    "Género: ",
                    2,
                    40);

            string clasificacion =
                Validador.LeerTexto(
                    "Clasificación: ",
                    1,
                    15);

            string id =
    GeneradorIds.SiguientePelicula(
        datos.Peliculas);

            datos.Peliculas.Add(
                new Pelicula(
                    id,
                    titulo,
                    duracion,
                    genero,
                    clasificacion));

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "AGREGAR PELÍCULA",
                id);

            ConsolaUI.Exito(
                "Película registrada" + id + ".");

                
        }
    }
}
