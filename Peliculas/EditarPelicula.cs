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
    public static class EditarPelicula
    {
        public static void Ejecutar(
            ContextoDatos datos,
            string usuario)
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

            if (!pelicula.Activo)
            {
                ConsolaUI.Error(
                    "Debe reactivarla primero.");

                return;
            }

            string titulo =
                Validador.LeerTexto(
                    "Nuevo título: ",
                    2,
                    100);

            if (datos.Peliculas.Any(
                x =>
                x.Id != id &&
                x.Titulo.Equals(
                    titulo,
                    StringComparison
                        .OrdinalIgnoreCase)))
            {
                ConsolaUI.Error(
                    "Ese título ya existe.");

                return;
            }

            pelicula.Editar(
                titulo,
                Validador.LeerTexto(
                    "Género: ",
                    2,
                    40),
                Validador.LeerTexto(
                    "Clasificación: ",
                    1,
                    15));

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "EDITAR PELÍCULA",
                id);

            ConsolaUI.Exito(
                "Película actualizada.");
        }
    }
}
