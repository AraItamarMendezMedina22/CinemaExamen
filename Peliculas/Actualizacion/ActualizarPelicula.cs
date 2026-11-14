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
    public static class ActualizarPelicula
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

            pelicula.ActualizarDuracion(
                Validador.LeerEntero(
                    "Nueva duración: ",
                    30,
                    300));

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "ACTUALIZAR PELÍCULA",
                id);

            ConsolaUI.Exito(
                "Duración actualizada.");
        }
    }
}
