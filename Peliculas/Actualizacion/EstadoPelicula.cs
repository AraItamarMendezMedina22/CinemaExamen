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
    public static class EstadoPelicula
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

            if (pelicula.Activo &&
                datos.Funciones.Any(
                    x =>
                    x.Activo &&
                    x.PeliculaId == id &&
                    x.HoraInicio >
                    DateTime.Now))
            {
                ConsolaUI.Error(
                    "Tiene funciones futuras activas.");

                return;
            }

            if (!Validador.Confirmar(
                pelicula.Activo
                ? "¿Inactivar película?"
                : "¿Reactivar película?"))
                return;

            if (pelicula.Activo)
                pelicula.Desactivar();
            else
                pelicula.Activar();

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                pelicula.Activo
                ? "REACTIVAR PELÍCULA"
                : "INACTIVAR PELÍCULA",
                id);

            ConsolaUI.Exito(
                "Estado actualizado.");
        }
    }
}
