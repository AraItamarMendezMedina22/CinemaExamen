using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Reservas
{
    public static class ListarReservas
    {
        public static void Ejecutar(
            ContextoDatos datos)
        {
            if (datos.Reservas.Count == 0)
            {
                ConsolaUI.Info(
                    "No existen boletos.");

                return;
            }

            foreach (
                Reserva reserva
                in datos.Reservas
                .OrderByDescending(
                    x => x.FechaCompra))
            {
                Console.WriteLine(
                    reserva.Id +
                    " | Cliente: " +
                    reserva.ClienteId +
                    " | Función: " +
                    reserva.FuncionId +
                    " | C$" +
                    reserva.TotalPagar +
                    " | " +
                    reserva.MetodoPago +
                    " | " +
                    (reserva.Activo
                        ? "Válido"
                        : "Cancelado"));
            }
        }
    }
}