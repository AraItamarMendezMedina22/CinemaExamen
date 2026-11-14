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
      List<Cliente> resultado =
  datos.Clientes
  .OrderBy(
      x => x.Id)
  .ToList();

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

                Console.WriteLine(
                " | Membresía: " +
                  (cliente.MembresiaVigente
                        ? "Vigente"
                        : "Vencida"));

            }
        }
    }
}
