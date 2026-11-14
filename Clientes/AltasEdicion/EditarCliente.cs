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
    public static class EditarCliente
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

            string nombre =
                Validador.LeerNombre(
                    "Nuevo nombre: ");

            string correo =
                Validador.LeerCorreo(
                    "Nuevo correo: ");

            if (datos.Clientes.Any(
                x =>
                x.Id != id &&
                x.Correo ==
                correo))
            {
                ConsolaUI.Error(
                    "Correo duplicado.");

                return;
            }

            cliente.Editar(
                nombre,
                correo);

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "EDITAR CLIENTE",
                id);

            ConsolaUI.Exito(
                "Cliente actualizado.");
        }
    }
}
