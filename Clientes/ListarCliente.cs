using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Clientes
{
    public static class ListarClientes
    {
        public static void Ejecutar(
            ContextoDatos datos)
        {
            Console.WriteLine(
                "1. Activos");

            Console.WriteLine(
                "2. Inactivos");

            Console.WriteLine(
                "3. Todos");

            int opcion =
                Validador.LeerOpcion(
                    1,
                    3);

            IEnumerable<Cliente> lista =
                datos.Clientes;

            if (opcion == 1)
                lista =
                    lista.Where(
                        x => x.Activo);

            if (opcion == 2)
                lista =
                    lista.Where(
                        x => !x.Activo);

            List<Cliente> resultado =
                lista.ToList();

            if (resultado.Count == 0)
            {
                ConsolaUI.Info(
                    "No hay clientes.");

                return;
            }

            foreach (
                Cliente cliente
                in resultado)
            {
                Console.WriteLine(
                    cliente.Id +
                    " | " +
                    cliente.Nombre +
                    " | " +
                    cliente.Correo +
                    " | " +
                    cliente.Telefono +
                    " | " +
                    (cliente.Activo
                        ? "Activo"
                        : "Inactivo"));
            }
        }
    }
}
