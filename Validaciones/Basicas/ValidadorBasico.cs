using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Utilidades
{
    public static partial class Validador
    {
        public static string LeerTexto(
            string mensaje,
            int minimo,
            int maximo)
        {
            while (true)
            {
                Console.Write(mensaje);

                string valor =
                    Console.ReadLine();

                if (string.IsNullOrWhiteSpace(valor))
                {
                    ConsolaUI.Error(
                        "El dato es obligatorio.");

                    continue;
                }

                valor =
                    valor.Trim();

                if (valor.Length < minimo ||
                    valor.Length > maximo)
                {
                    ConsolaUI.Error(
                        "Debe contener entre " +
                        minimo +
                        " y " +
                        maximo +
                        " caracteres.");

                    continue;
                }

                return valor;
            }
        }

        public static int LeerEntero(
            string mensaje,
            int minimo,
            int maximo)
        {
            while (true)
            {
                Console.Write(mensaje);

                int numero;

                if (!int.TryParse(
                    Console.ReadLine(),
                    out numero))
                {
                    ConsolaUI.Error(
                        "Debe escribir un número válido.");

                    continue;
                }

                if (numero < minimo ||
                    numero > maximo)
                {
                    ConsolaUI.Error(
                        "Debe estar entre " +
                        minimo +
                        " y " +
                        maximo +
                        ".");

                    continue;
                }

                return numero;
            }
        }

        public static decimal LeerDecimal(
            string mensaje,
            decimal minimo,
            decimal maximo)
        {
            while (true)
            {
                Console.Write(mensaje);

                string texto =
                    Console.ReadLine();

                if (texto != null)
                {
                    texto =
                        texto.Replace(',', '.');
                }

                decimal numero;

                if (!decimal.TryParse(
                    texto,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out numero))
                {
                    ConsolaUI.Error(
                        "Monto inválido.");

                    continue;
                }

                if (numero < minimo ||
                    numero > maximo)
                {
                    ConsolaUI.Error(
                        "Monto fuera de rango.");

                    continue;
                }

                return numero;
            }
        }

        public static DateTime LeerFechaHora(
            string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);

                DateTime fecha;

                if (!DateTime.TryParseExact(
                    Console.ReadLine(),
                    "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out fecha))
                {
                    ConsolaUI.Error(
                        "Formato correcto: dd/MM/yyyy HH:mm");

                    continue;
                }

                return fecha;
            }
        }

        public static bool Confirmar(
            string mensaje)
        {
            while (true)
            {
                Console.Write(
                    mensaje +
                    " (S/N): ");

                string respuesta =
                    Console.ReadLine();

                if (string.IsNullOrWhiteSpace(
                    respuesta))
                {
                    ConsolaUI.Error(
                        "Debe responder S o N.");

                    continue;
                }

                respuesta =
                    respuesta.Trim()
                    .ToUpperInvariant();

                if (respuesta == "S")
                    return true;

                if (respuesta == "N")
                    return false;

                ConsolaUI.Error(
                    "Respuesta inválida.");
            }
        }

        public static int LeerOpcion(
            int minimo,
            int maximo)
        {
            return LeerEntero(
                "Seleccione una opción: ",
                minimo,
                maximo);
        }
    }
}
