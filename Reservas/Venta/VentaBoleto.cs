using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Asientos;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Pagos;
using Cinema_Doomsday.Recibos;
using Cinema_Doomsday.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Reservas
{
    public static class VentaBoleto
    {
        public static void Ejecutar(
            ContextoDatos datos,
            string usuario)
        {
            ConsolaUI.Encabezado(
                "VENTA DE BOLETOS");

            Cliente cliente =
                ObtenerCliente(
                    datos);

            if (cliente == null)
                return;

            Funcion funcion =
                ObtenerFuncion(
                    datos);

            if (funcion == null)
                return;

            List<Asiento> asientos =
                SelectorAsientos.Seleccionar(
                    datos,
                    funcion.Id);

            if (asientos == null ||
                asientos.Count == 0)
                return;

            decimal subtotal =
                funcion.Precio *
                asientos.Count;

            decimal descuento =
                cliente.MembresiaVigente
                ? subtotal * 0.05m
                : 0m;

            decimal total =
                subtotal - descuento;

            if (cliente.MembresiaVigente)
            {
                ConsolaUI.Info(
                    "Membresía vigente: 5% de descuento.");

                Console.WriteLine(
                    "Descuento: C$" +
                    descuento.ToString("N2"));
            }
            else
            {
                ConsolaUI.Info(
                    "Membresía vencida: sin descuento.");
            }

            ResultadoPago pago =
                ProcesadorPago.Procesar(
                    total);

            Console.WriteLine();

            if (!Validador.Confirmar(
                "¿Confirmar compra?"))
                return;

            RegistrarVenta(
                datos,
                usuario,
                cliente,
                funcion,
                asientos,
                total,
                pago);
        }

        private static Cliente ObtenerCliente(
            ContextoDatos datos)
        {
            string id =
                Validador.LeerTexto(
                    "ID cliente: ",
                    3,
                    15)
                .ToUpper();

            Cliente cliente =
                ReglasCine.BuscarCliente(
                    datos,
                    id);

            if (cliente == null)
            {
                ConsolaUI.Error(
                    "El cliente no existe.");

                return null;
            }

            if (!cliente.Activo)
            {
                ConsolaUI.Error(
                    "El cliente está inactivo.");

                return null;
            }

            return cliente;
        }

        private static Funcion ObtenerFuncion(
            ContextoDatos datos)
        {
            string id =
                Validador.LeerTexto(
                    "ID función: ",
                    3,
                    12)
                .ToUpper();

            Funcion funcion =
                ReglasCine.BuscarFuncion(
                    datos,
                    id);

            if (funcion == null)
            {
                ConsolaUI.Error(
                    "La función no existe.");

                return null;
            }

            if (!funcion.Activo)
            {
                ConsolaUI.Error(
                    "La función está inactiva.");

                return null;
            }

            if (funcion.HoraInicio <=
                DateTime.Now)
            {
                ConsolaUI.Error(
                    "La función ya inició.");

                return null;
            }

            return funcion;
        }

        private static void RegistrarVenta(
            ContextoDatos datos,
            string usuario,
            Cliente cliente,
            Funcion funcion,
            List<Asiento> asientos,
            decimal total,
            ResultadoPago pago)
        {
            foreach (
                Asiento asiento
                in asientos)
            {
                asiento.Ocupar();
            }

            string id =
                "BOL-" +
                Guid.NewGuid()
                .ToString("N")
                .Substring(
                    0,
                    8)
                .ToUpper();

            List<string> codigos =
                asientos
                .Select(
                    x => x.Codigo())
                .ToList();

            Reserva reserva =
                new Reserva(
                    id,
                    funcion.Id,
                    cliente.Id,
                    codigos,
                    total,
                    pago.MetodoPago,
                    pago.Detalle,
                    pago.Cambio);

            datos.Reservas.Add(
                reserva);

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "VENDER BOLETO",
                reserva.Id);

            ConsolaUI.Exito(
                "Venta registrada correctamente.");

            ReciboService.Generar(
                datos,
                reserva);
        }
    }
}
