using Cinema_Doomsday.Clientes;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Peliculas;
using Cinema_Doomsday.Salas;
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
                        "Activar / Inactivar",
                        "Renovar membresía",
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
                        BuscarCliente.Ejecutar(datos);
                        break;

                    case 4:
                        ListarClientes.Ejecutar(datos);
                        break;

                    case 5:
                        ActualizarCliente.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 6:
                        EstadoCliente.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 7:
                        RenovarMembresia.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 0:
                        volver = true;
                        break;
                }
            }


        }
    }

}