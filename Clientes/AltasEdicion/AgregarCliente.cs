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

            Console.WriteLine();
            Console.WriteLine("TIEMPO DE MEMBRESÍA");
            Console.WriteLine("1. 1 mes");
            Console.WriteLine("2. 3 meses");
            Console.WriteLine("3. 6 meses");
            Console.WriteLine("4. 12 meses");

            int opcionMembresia =
                Validador.LeerOpcion(1, 4);

            int meses =
                opcionMembresia == 1 ? 1 :
                opcionMembresia == 2 ? 3 :
                opcionMembresia == 3 ? 6 : 12;

            DateTime membresiaHasta =
            DateTime.Today.AddMonths(meses);



            string id =
            GeneradorIds.SiguienteCliente(
              datos.Clientes);

            datos.Clientes.Add(
                new Cliente(
                    id,
                    nombre,
                    cedula,
                    Validador.ObtenerEdad(cedula),
                    correo,
                    telefono,
                    membresiaHasta));

            ConsolaUI.Info(
            "Membresía vigente hasta: " +membresiaHasta.ToString("dd/MM/yyyy"));

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
