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
    public static class AgregarCliente
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

            if (datos.Clientes.Any(
                x => x.Id == id))
            {
                ConsolaUI.Error(
                    "ID ocupado.");

                return;
            }

            string nombre =
                Validador.LeerNombre(
                    "Nombre: ");

            string correo =
                Validador.LeerCorreo(
                    "Correo: ");

            if (datos.Clientes.Any(
                x =>
                x.Correo ==
                correo))
            {
                ConsolaUI.Error(
                    "Correo duplicado.");

                return;
            }

            string telefono =
                Validador.LeerTelefono(
                    "Celular: ");

            datos.Clientes.Add(
                new Cliente(
                    id,
                    nombre,
                    correo,
                    telefono));

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "AGREGAR CLIENTE",
                id);

            ConsolaUI.Exito(
                "Cliente registrado.");
        }
    }
}
