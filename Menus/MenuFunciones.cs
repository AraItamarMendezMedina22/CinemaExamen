using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Funciones;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Menus
{
    public static class MenuFunciones
    {
        public static void Mostrar(
            ContextoDatos datos,
            Usuario usuario)
        {
            bool volver = false;

            while (!volver)
            {
                ConsolaUI.Encabezado(
                    "FUNCIONES / CARTELERA");

                System.Console.WriteLine(
                    "1. Agregar");
                System.Console.WriteLine(
                    "2. Editar");
                System.Console.WriteLine(
                    "3. Buscar");
                System.Console.WriteLine(
                    "4. Listar");
                System.Console.WriteLine(
                    "5. Actualizar precio");
                System.Console.WriteLine(
                    "6. Activar / Inactivar");
                System.Console.WriteLine(
                    "0. Volver");

                int opcion =
                    Validador.LeerOpcion(
                        0,
                        6);

                switch (opcion)
                {
                    case 1:
                        AgregarFuncion.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 2:
                        EditarFuncion.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 3:
                        BuscarFuncion.Ejecutar(
                            datos);
                        break;

                    case 4:
                        ListarFunciones.Ejecutar(
                            datos);
                        break;

                    case 5:
                        ActualizarFuncion.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 6:
                        EstadoFuncion.Ejecutar(
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
