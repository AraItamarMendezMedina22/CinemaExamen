using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Exportacion;
using Cinema_Doomsday.Login;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Menus
{
    public static class MenuPrincipal
    {
        public static void Mostrar(
            ContextoDatos datos,
            Usuario usuario)
        {
            bool cerrar =
                false;

            while (!cerrar)
            {
                try
                {
                    ConsolaUI.Encabezado(
                        "MENÚ PRINCIPAL");

                    Console.WriteLine(
                        "Usuario: " +
                        usuario.NombreUsuario);

                    Console.WriteLine(
                        "Rol: " +
                        usuario.Rol);

                    Console.WriteLine();

                    Console.WriteLine(
                        "1. Películas");

                    Console.WriteLine(
                        "2. Salas");

                    Console.WriteLine(
                        "3. Funciones / Cartelera");

                    Console.WriteLine(
                        "4. Clientes");

                    Console.WriteLine(
                        "5. Boletería / Reservas");

                    Console.WriteLine(
                        "6. Exportar datos");

                    Console.WriteLine(
                        "7. Consultar logs");

                    Console.WriteLine(
                        "8. Cerrar sesión");

                    int opcion =
                        Validador.LeerOpcion(
                            1,
                            8);

                    switch (opcion)
                    {
                        case 1:
                            MenuPeliculas.Mostrar(
                                datos,
                                usuario);
                            break;

                        case 2:
                            MenuSalas.Mostrar(
                                datos,
                                usuario);
                            break;

                        case 3:
                            MenuFunciones.Mostrar(
                                datos,
                                usuario);
                            break;

                        case 4:
                            MenuClientes.Mostrar(
                                datos,
                                usuario);
                            break;

                        case 5:
                            MenuReservas.Mostrar(
                                datos,
                                usuario);
                            break;

                        case 6:
                            Exportador.Ejecutar(
                                datos,
                                usuario.NombreUsuario);

                            ConsolaUI.Pausa();
                            break;

                        case 7:
                            ConsultarLogs.Ejecutar(
                                usuario);

                            ConsolaUI.Pausa();
                            break;

                        case 8:
                            Autenticacion.Cerrar(
                                usuario);

                            cerrar = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ConsolaUI.Error(
                        "Error controlado: " +
                        ex.Message);

                    ConsolaUI.Pausa();
                }
            }
        }
    }
}
