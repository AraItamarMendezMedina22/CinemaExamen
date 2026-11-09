using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Utilidades
{
    public static class Validador
    {
        public static string LeerTexto(
            string mensaje,
            int minimo,
            int maximo)
        {
            while (true)
            {
                Console.Write(
                    mensaje);

                string valor =
                    Console.ReadLine();

                if (valor == null)
                {
                    ConsolaUI.Error(
                        "El dato no puede ser nulo.");

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

        public static string LeerNombre(
            string mensaje)
        {
            while (true)
            {
                string nombre =
                    LeerTexto(
                        mensaje,
                        2,
                        80);

                if (!Regex.IsMatch(
                    nombre,
                    @"^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s]+$"))
                {
                    ConsolaUI.Error(
                        "El nombre solo puede contener letras.");

                    continue;
                }

                return nombre;
            }
        }

        public static int LeerEntero(
            string mensaje,
            int minimo,
            int maximo)
        {
            while (true)
            {
                Console.Write(
                    mensaje);

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
                Console.Write(
                    mensaje);

                string texto =
                    Console.ReadLine();

                if (texto != null)
                    texto =
                        texto.Replace(
                            ',',
                            '.');

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
                Console.Write(
                    mensaje);

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

        public static string LeerCorreo(
            string mensaje)
        {
            while (true)
            {
                string correo =
                    LeerTexto(
                        mensaje,
                        5,
                        100)
                    .ToLower();

                if (!Regex.IsMatch(
                    correo,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    ConsolaUI.Error(
                        "Correo inválido.");

                    continue;
                }

                return correo;
            }
        }

        public static string LeerTelefono(
            string mensaje)
        {
            while (true)
            {
                string telefono =
                    LeerTexto(
                        mensaje,
                        8,
                        8);

                if (!Regex.IsMatch(
                    telefono,
                    @"^[578]\d{7}$"))
                {
                    ConsolaUI.Error(
                        "Celular inválido.");

                    continue;
                }

                return telefono;
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

                if (respuesta == null)
                {
                    ConsolaUI.Error(
                        "Debe responder S o N.");

                    continue;
                }

                respuesta =
                    respuesta.Trim()
                    .ToUpper();

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
