using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema_Doomsday.Utilidades.Cedulas;

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
                    .ToLowerInvariant();

                if (!Regex.IsMatch(
                    correo,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    ConsolaUI.Error(
                        "Correo inválido.");

                    continue;
                }

                int posicionArroba =
                    correo.IndexOf(
                        '@');

                string dominio =
                    correo.Substring(
                        posicionArroba + 1);

                if (!DominiosGratuitos.Contains(
                    dominio))
                {
                    ConsolaUI.Error(
                        "Solo se permiten correos " +
                        "de dominios gratuitos. ");

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
                    @"^[2578]\d{7}$"))
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

        public static string LeerCedula(
            string mensaje)
        {
            while (true)
            {
                string cedula =
                    LeerTexto(
                        mensaje,
                        15,
                        15)
                    .ToUpperInvariant();

                if (!Regex.IsMatch(
                    cedula,
                    @"^\d{3}-\d{6}-\d{4}[A-Z]$"))
                {
                    ConsolaUI.Error(
                        "Formato esperado: " +
                        "000-000000-0000A.");

                    continue;
                }

                DateTime fechaNacimiento;

                if (!TryObtenerFechaNacimiento(
                    cedula,
                    out fechaNacimiento))
                {
                    ConsolaUI.Error(
                        "la cédula no es válida.");

                    continue;
                }

                if (CalcularEdad(
                    fechaNacimiento) < 0)
                {
                    ConsolaUI.Error(
                        "Aun no naces, Maquina <3 ");
                    
                    continue;
                }

                return cedula;
            }
        }

        public static int ObtenerEdad(
            string cedula)
        {
            DateTime fechaNacimiento;

            if (!TryObtenerFechaNacimiento(
                cedula,
                out fechaNacimiento))
            {
                throw new FormatException(
                    "Tu cedula esta en formato incorrecto.");
            }

            return CalcularEdad(
                fechaNacimiento);
        }


    public static string ObtenerOrigenCedula(string cedula)
    {
      string cedulaLimpia = cedula.Replace("-", "").Trim();

      if (cedulaLimpia.Length < 3)
        return "Cédula inválida";

      string codigoMunicipio = cedulaLimpia.Substring(0, 3);

      if (CatalogoMunicipios.Municipios.TryGetValue(codigoMunicipio, out string origen))
      {
        return origen;
      }

      return "Municipio inexistente";
    }
    static bool TryObtenerFechaNacimiento(
            string cedula,
            out DateTime fechaNacimiento)
        {
            fechaNacimiento =
                DateTime.MinValue;

            if (string.IsNullOrWhiteSpace(
                cedula))
            {
                return false;
            }

            string fechaTexto =
                cedula.Substring(
                    4,
                    6);

            int dia =
                int.Parse(
                    fechaTexto.Substring(
                        0,
                        2));

            int mes =
                int.Parse(
                    fechaTexto.Substring(
                        2,
                        2));

            int anioCorto =
                int.Parse(
                    fechaTexto.Substring(
                        4,
                        2));

            int anioActual =
                DateTime.Now.Year;

            int anioActualCorto =
                anioActual % 100;

            int anio =
                anioCorto <= anioActualCorto
                    ? 2000 + anioCorto
                    : 1900 + anioCorto;

            try
            {
                fechaNacimiento =
                    new DateTime(
                        anio,
                        mes,
                        dia);

                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        static int CalcularEdad(
            DateTime fechaNacimiento)
        {
            DateTime hoy =
                DateTime.Today;

            int edad =
                hoy.Year -
                fechaNacimiento.Year;

            if (fechaNacimiento.Date >
                hoy.AddYears(-edad))
            {
                edad--;
            }

            return edad;
        }

        static readonly HashSet<string> DominiosGratuitos =
    new HashSet<string>(
        StringComparer.OrdinalIgnoreCase)
    {
        "gmail.com",
        "googlemail.com",
        "hotmail.com",
        "hotmail.es",
        "outlook.com",
        "outlook.es",
        "live.com",
        "msn.com",
        "yahoo.com",
        "yahoo.es",
        "icloud.com",
        "me.com",
        "mac.com",
        "proton.me",
        "protonmail.com",
        "zoho.com",
        "gmx.com",
        "mail.com",
        "aol.com",
        "yandex.com",
        "tutanota.com"
    };
    }
}
