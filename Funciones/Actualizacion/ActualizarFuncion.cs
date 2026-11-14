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
    public static class ActualizarFuncion
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

            if (!funcion.Activo)
            {
                ConsolaUI.Error(
                    "Debe reactivarla primero.");

                return;
            }

            funcion.ActualizarPrecio(
                Validador.LeerDecimal(
                    "Nuevo precio C$: ",
                    50,
                    1000));

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "ACTUALIZAR PRECIO",
                id);

            ConsolaUI.Exito(
                "Precio actualizado.");
        }
    }
}
