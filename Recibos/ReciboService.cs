using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Recibos
{
    public static class ReciboService
    {
        public static string Generar(
            ContextoDatos datos,
            Reserva reserva)
        {
            Cliente cliente =
                ReglasCine.BuscarCliente(
                    datos,
                    reserva.ClienteId);

            Funcion funcion =
                ReglasCine.BuscarFuncion(
                    datos,
                    reserva.FuncionId);

            Pelicula pelicula =
                ReglasCine.BuscarPelicula(
                    datos,
                    funcion.PeliculaId);

            Sala sala =
                ReglasCine.BuscarSala(
                    datos,
                    funcion.SalaId);

            StringBuilder recibo =
                new StringBuilder();

            recibo.AppendLine(
                "====================================================");

            recibo.AppendLine(
                "                  CINEMA DOOMSDAY");

            recibo.AppendLine(
                "                   RECIBO / BOLETO");

            recibo.AppendLine(
                "====================================================");

            recibo.AppendLine(
                "Boleto: " +
                reserva.Id);

            recibo.AppendLine(
                "Compra: " +
                reserva.FechaCompra
                .ToString(
                    "dd/MM/yyyy HH:mm"));

            recibo.AppendLine(
                "Cliente: " +
                cliente.Nombre);

            recibo.AppendLine(
                "Correo: " +
                cliente.Correo);

            recibo.AppendLine(
                "Teléfono: " +
                cliente.Telefono);

            recibo.AppendLine(
                "Membresía: " +
              (cliente.MembresiaVigente
                ? "VIGENTE"
                : "VENCIDA"));

            recibo.AppendLine(
                "Vence: " +
                cliente.MembresiaHasta
                .ToString("dd/MM/yyyy"));

            if (cliente.MembresiaVigente)
            {
                recibo.AppendLine(
                    "Beneficio: 5% de descuento");
            }

            recibo.AppendLine(
                "Película: " +
                pelicula.Titulo);

            recibo.AppendLine(
                "Sala: " +
                sala.NumeroSala +
                " - " +
                sala.TipoSala);

            recibo.AppendLine(
                "Función: " +
                funcion.HoraInicio
                .ToString(
                    "dd/MM/yyyy HH:mm"));

            recibo.AppendLine(
                "Asientos: " +
                string.Join(
                    ", ",
                    reserva.ListaDeAsientos));

            recibo.AppendLine(
                "Pago: " +
                reserva.MetodoPago);

            if (!string.IsNullOrWhiteSpace(
                reserva.DetallePago))
            {
                recibo.AppendLine(
                    "Detalle: " +
                    reserva.DetallePago);
            }

            recibo.AppendLine(
                "TOTAL: C$" +
                reserva.TotalPagar
                .ToString("N2"));

            if (reserva.Cambio > 0)
            {
                recibo.AppendLine(
                    "Cambio: C$" +
                    reserva.Cambio
                    .ToString("N2"));
            }

            recibo.AppendLine(
                "Estado: " +
                (reserva.Activo
                    ? "VÁLIDO"
                    : "CANCELADO"));

            recibo.AppendLine(
                "====================================================");

            string carpeta =
                Path.Combine(
                    AppDomain.CurrentDomain
                    .BaseDirectory,
                    "Recibos");

            Directory.CreateDirectory(
                carpeta);

            string ruta =
                Path.Combine(
                    carpeta,
                    reserva.Id +
                    ".txt");

            File.WriteAllText(
                ruta,
                recibo.ToString(),
                Encoding.UTF8);

            Console.WriteLine(
                recibo.ToString());

            Console.WriteLine(
                "Recibo guardado en:");

            Console.WriteLine(
                ruta);

            return ruta;
        }
    }
}
