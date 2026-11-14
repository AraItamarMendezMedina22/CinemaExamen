using System;
using System.Linq;
using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;

namespace Cinema_Doomsday.Clientes
{
    public static class RenovarMembresia
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
                    "El cliente está inactivo.");
                return;
            }

            Console.WriteLine(
                "Membresía actual: " +
                cliente.MembresiaHasta
                .ToString("dd/MM/yyyy"));

            Console.WriteLine();
            Console.WriteLine("1. Renovar 1 mes");
            Console.WriteLine("2. Renovar 3 meses");
            Console.WriteLine("3. Renovar 6 meses");
            Console.WriteLine("4. Renovar 12 meses");

            int opcion =
                Validador.LeerOpcion(1, 4);

            int meses =
                opcion == 1 ? 1 :
                opcion == 2 ? 3 :
                opcion == 3 ? 6 : 12;

            if (!Validador.Confirmar(
                "¿Confirmar renovación?"))
                return;

            cliente.RenovarMembresia(meses);

            ArchivoDatos.Guardar(datos);

            ArchivoLogs.Registrar(
                usuario,
                "RENOVAR MEMBRESÍA",
                cliente.Id);

            ConsolaUI.Exito(
                "Membresía renovada.");

            Console.WriteLine(
                "Nueva fecha de vencimiento: " +
                cliente.MembresiaHasta
                .ToString("dd/MM/yyyy"));
        }
    }
}