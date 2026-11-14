using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Recibos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Reservas
{
    public static class BuscarReserva
    {
        public static void Ejecutar(
            ContextoDatos datos)
        {
            string id =
                Validador.LeerTexto(
                    "ID boleto: ",
                    5,
                    20)
                .ToUpper();

            Reserva reserva =
                datos.Reservas
                .FirstOrDefault(
                    x => x.Id == id);

            if (reserva == null)
            {
                ConsolaUI.Error(
                    "Boleto inexistente.");

                return;
            }

            ReciboService.Generar(
                datos,
                reserva);
        }
    }
}
