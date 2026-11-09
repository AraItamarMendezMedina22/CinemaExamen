using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Reservas;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Menus
{
    public static class MenuReservas
    {
        public static void Mostrar(
            ContextoDatos datos,
            Usuario usuario)
        {
            bool volver = false;

            while (!volver)
            {
                ConsolaUI.Encabezado(
                    "BOLETERÍA / RESERVAS");

                System.Console.WriteLine(
                    "1. Vender boleto");
                System.Console.WriteLine(
                    "2. Buscar boleto");
                System.Console.WriteLine(
                    "3. Listar boletos");
                System.Console.WriteLine(
                    "4. Cancelar boleto");
                System.Console.WriteLine(
                    "5. Reimprimir recibo");
                System.Console.WriteLine(
                    "0. Volver");

                int opcion =
                    Validador.LeerOpcion(
                        0,
                        5);

                switch (opcion)
                {
                    case 1:
                        VentaBoleto.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 2:
                        BuscarReserva.Ejecutar(
                            datos);
                        break;

                    case 3:
                        ListarReservas.Ejecutar(
                            datos);
                        break;

                    case 4:
                        CancelarReserva.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 5:
                        ReimprimirRecibo.Ejecutar(
                            datos);
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
