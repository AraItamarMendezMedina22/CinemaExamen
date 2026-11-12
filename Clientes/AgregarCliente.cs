using System;
using System.Collections.Generic;
using System.IO;
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

            string nombre =
                Validador.LeerNombre(
                    "Nombre: ");

            string cedula =
            Validador.LeerCedula(
              "Cédula: ");

            if (datos.Clientes.Any(
            x =>
              x.Cedula.Equals(
                  cedula,
                  StringComparison.OrdinalIgnoreCase)))
                  {
                    ConsolaUI.Error(
                        "La cédula ya está registrada.");

                    return;
                  }
              string origen =
              Validador.ObtenerOrigenCedula(
                cedula);

                    Console.WriteLine(
                        "Origen: " +
                        origen);

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

            string id =
    GeneradorIds.SiguienteCliente(
        datos.Clientes);

            datos.Clientes.Add(
                new Cliente(
                    id,
                    nombre,
                    cedula,
                    Validador.ObtenerEdad(
                        cedula),
                    correo,
                    telefono));

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "AGREGAR CLIENTE",
                id);

            ConsolaUI.Exito(
                "Cliente registrado" + id +".");


        }
    }
}
