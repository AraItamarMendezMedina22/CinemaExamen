using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Funciones
{
    public static class EstadoFuncion
    {
        public static void Ejecutar(
            ContextoDatos datos,
            string usuario)
        {
            string id =
                Validador.LeerTexto(
                    "ID función: ",
                    3,
                    12)
                .ToUpper();

            Funcion funcion =
                datos.Funciones
                .FirstOrDefault(
                    x => x.Id == id);

            if (funcion == null)
            {
                ConsolaUI.Error(
                    "Función inexistente.");

                return;
            }

            if (funcion.Activo &&
                datos.Reservas.Any(
                    x =>
                    x.Activo &&
                    x.FuncionId == id))
            {
                ConsolaUI.Error(
                    "No se puede inactivar una función con boletos vigentes.");

                return;
            }

            if (!Validador.Confirmar(
                funcion.Activo
                ? "¿Inactivar función?"
                : "¿Reactivar función?"))
                return;

            if (funcion.Activo)
                funcion.Desactivar();
            else
                funcion.Activar();

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                funcion.Activo
                ? "REACTIVAR FUNCIÓN"
                : "INACTIVAR FUNCIÓN",
                id);

            ConsolaUI.Exito(
                "Estado actualizado.");
        }
    }
}
