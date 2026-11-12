using System;
using System.Collections.Generic;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Reservas;
using Cinema_Doomsday.Utilidades;

namespace Cinema_Doomsday.Menus
{
    public static class MenuReservas
    {
        public static void Mostrar(
            ContextoDatos datos,
            Usuario usuario)
        {
            bool volver =
                false;
    

      ConsolaUI.Pausa();
      ListarReservas.Ejecutar(
      datos);
      List<string> opciones =
          new List<string>
          {
                    "Vender boleto",
                    "Buscar boleto",
                    "Listar boletos",
                    "Cancelar boleto",
                    "Reimprimir recibo"
          };

      while (!volver)
            {
                ConsolaUI.Encabezado(
                    "BOLETERÍA / RESERVAS");

                int opcion =
                    ConsolaUI.LeerMenu(
                        opciones,
                        true);

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
                        CancelarReserva.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 4:
                        ReimprimirRecibo.Ejecutar(
                            datos);
                        break;

                    case 0:
                        volver =
                            true;
                        continue;
                }

                ConsolaUI.Pausa();
            }
        }
    }
}
