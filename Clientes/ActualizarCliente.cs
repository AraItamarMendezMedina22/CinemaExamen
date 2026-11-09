using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Clientes
{
    public static class ActualizarCliente
    {
        public static void Ejecutar(
            ContextoDatos datos,
            string usuario)
        {
            string id =
                Validador.LeerTexto(
                    "ID cliente: ",
                    3,
                    15)
                .ToUpper();

            Cliente cliente =
                datos.Clientes
                .FirstOrDefault(
                    x => x.Id == id);

            if (cliente == null)
            {
                ConsolaUI.Error(
                    "Cliente inexistente.");

                return;
            }

            if (!cliente.Activo)
            {
                ConsolaUI.Error(
                    "Debe reactivarlo primero.");

                return;
            }

            cliente.ActualizarTelefono(
                Validador.LeerTelefono(
                    "Nuevo teléfono: "));

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "ACTUALIZAR CLIENTE",
                id);

            ConsolaUI.Exito(
                "Teléfono actualizado.");
        }
    }
}
