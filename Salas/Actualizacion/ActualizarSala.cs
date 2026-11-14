using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Salas
{
    public static class ActualizarSala
    {
        public static void Ejecutar(
            ContextoDatos datos,
            string usuario)
        {
            string id =
                Validador.LeerTexto(
                    "ID sala: ",
                    3,
                    12)
                .ToUpper();

            Sala sala =
                datos.Salas
                .FirstOrDefault(
                    x => x.Id == id);

            if (sala == null)
            {
                ConsolaUI.Error(
                    "Sala inexistente.");

                return;
            }

            if (!sala.Activo)
            {
                ConsolaUI.Error(
                    "Debe reactivarla primero.");

                return;
            }

            if (datos.Funciones.Any(
                x =>
                x.Activo &&
                x.SalaId == id))
            {
                ConsolaUI.Error(
                    "No cambie la capacidad mientras existan funciones activas.");

                return;
            }

            sala.ActualizarCapacidad(
                Validador.LeerEntero(
                    "Nueva capacidad: ",
                    10,
                    200));

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "ACTUALIZAR SALA",
                id);

            ConsolaUI.Exito(
                "Capacidad actualizada.");
        }
    }
}
