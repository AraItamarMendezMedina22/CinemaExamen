using Cinema_Doomsday.Clientes;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Peliculas;
using Cinema_Doomsday.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Menus
{
    public static class MenuClientes
    {
        public static void Mostrar(
            ContextoDatos datos,
            Usuario usuario)
        {

      bool volver = false;

      ListarClientes.Ejecutar(
         datos);

      List<string> opciones =
               new List<string>
               {
                        "Agregar",
                        "Editar",
                        "Buscar",
                        "Listar",
                        "Actualizar teléfono",
                        "Activar / Inactivar"
               };

      while (!volver)
            {
                ConsolaUI.Encabezado(
                    "GESTIÓN DE CLIENTES");

           
                int opcion =
                    ConsolaUI.LeerMenu(
                        opciones,
                        true);

                switch (opcion)
                {
                    case 1:
                        AgregarCliente.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 2:
                        EditarCliente.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 3:
                        BuscarCliente.Ejecutar(
                            datos);
                        break;

                    case 4:
                        ActualizarCliente.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 5:
                        EstadoCliente.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 0:
                        volver = true;
                        continue;
                }

                ConsolaUI.Pausa();
            }
        }
    }
}
