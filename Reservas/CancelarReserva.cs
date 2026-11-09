using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Recibos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Reservas
{
    public static class CancelarReserva
    {
        public static void Ejecutar(
            ContextoDatos datos,
            string usuario)
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

            if (!reserva.Activo)
            {
                ConsolaUI.Error(
                    "El boleto ya está cancelado.");

                return;
            }

            Funcion funcion =
                ReglasCine.BuscarFuncion(
                    datos,
                    reserva.FuncionId);

            if (funcion == null ||
                funcion.HoraInicio <=
                DateTime.Now.AddHours(2))
            {
                ConsolaUI.Error(
                    "Solo se permite cancelar hasta 2 horas antes.");

                return;
            }

            if (!Validador.Confirmar(
                "¿Cancelar boleto?"))
                return;

            foreach (
                string codigo
                in reserva.ListaDeAsientos)
            {
                Asiento asiento =
                    datos.Asientos
                    .FirstOrDefault(
                        x =>
                        x.FuncionId ==
                        reserva.FuncionId &&
                        x.Codigo() ==
                        codigo);

                if (asiento != null)
                    asiento.Liberar();
            }

            reserva.Desactivar();

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "CANCELAR BOLETO",
                id);

            ConsolaUI.Exito(
                "Boleto cancelado y asientos liberados.");

            ReciboService.Generar(
                datos,
                reserva);
        }
    }
}
