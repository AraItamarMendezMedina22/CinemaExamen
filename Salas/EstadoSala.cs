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
    public static class EstadoSala
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

            if (sala.Activo &&
                datos.Funciones.Any(
                    x =>
                    x.Activo &&
                    x.SalaId == id &&
                    x.HoraInicio >
                    DateTime.Now))
            {
                ConsolaUI.Error(
                    "La sala tiene funciones futuras.");

                return;
            }

            if (!Validador.Confirmar(
                sala.Activo
                ? "¿Inactivar sala?"
                : "¿Reactivar sala?"))
                return;

            if (sala.Activo)
                sala.Desactivar();
            else
                sala.Activar();

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                sala.Activo
                ? "REACTIVAR SALA"
                : "INACTIVAR SALA",
                id);

            ConsolaUI.Exito(
                "Estado actualizado.");
        }
    }
}
