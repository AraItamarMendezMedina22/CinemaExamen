using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Clientes
{
    public static class BuscarCliente
    {
        public static void Ejecutar(
            ContextoDatos datos)
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

            Console.WriteLine(
                "Nombre: " +
                cliente.Nombre);

            Console.WriteLine(
                "Correo: " +
                cliente.Correo);

            Console.WriteLine(
                "Teléfono: " +
                cliente.Telefono);

            Console.WriteLine(
                "Estado: " +
                (cliente.Activo
                    ? "Activo"
                    : "Inactivo"));
        }
    }
}
