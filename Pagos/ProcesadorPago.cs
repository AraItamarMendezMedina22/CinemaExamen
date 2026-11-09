using Cinema_Doomsday.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Pagos
{
    public static class ProcesadorPago
    {
        public static ResultadoPago Procesar(
            decimal total)
        {
            Console.WriteLine();
            Console.WriteLine(
                "TOTAL A PAGAR: C$" +
                total.ToString("N2"));

            Console.WriteLine();
            Console.WriteLine(
                "1. Efectivo");

            Console.WriteLine(
                "2. Tarjeta");

            Console.WriteLine(
                "3. Entrada prepagada");

            int opcion =
                Validador.LeerOpcion(
                    1,
                    3);

            switch (opcion)
            {
                case 1:
                    return PagoEfectivo(
                        total);

                case 2:
                    return PagoTarjeta();

                default:
                    return EntradaPrepagada();
            }
        }

        private static ResultadoPago PagoEfectivo(
            decimal total)
        {
            decimal recibido =
                Validador.LeerDecimal(
                    "Monto recibido C$: ",
                    total,
                    100000);

            decimal cambio =
                recibido - total;

            return new ResultadoPago(
                "Efectivo",
                "Monto recibido: C$" +
                recibido.ToString("N2"),
                cambio);
        }

        private static ResultadoPago PagoTarjeta()
        {
            while (true)
            {
                string ultimos =
                    Validador.LeerTexto(
                        "Últimos 4 dígitos: ",
                        4,
                        4);

                int numero;

                if (!int.TryParse(
                    ultimos,
                    out numero))
                {
                    ConsolaUI.Error(
                        "Los 4 caracteres deben ser números.");

                    continue;
                }

                return new ResultadoPago(
                    "Tarjeta",
                    "Tarjeta terminada en " +
                    ultimos,
                    0);
            }
        }

        private static ResultadoPago EntradaPrepagada()
        {
            string codigo =
                Validador.LeerTexto(
                    "Código de entrada: ",
                    6,
                    20)
                .ToUpper();

            return new ResultadoPago(
                "Entrada prepagada",
                "Código: " + codigo,
                0);
        }
    }
}
