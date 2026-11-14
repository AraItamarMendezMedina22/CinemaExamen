using Cinema_Doomsday.Utilidades.Cedulas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Utilidades
{
    public static partial class Validador
    {
        public static string LeerCedula(string mensaje)
        {
            while (true)
            {
                string cedula = LeerTexto(mensaje, 16, 16).ToUpperInvariant();

                if (!Regex.IsMatch(cedula, @"^\d{3}-\d{6}-\d{4}[A-Z]$"))
                {
                    ConsolaUI.Error("Formato esperado: 000-000000-0000A.");
                    continue;
                }

                string codigo = cedula.Substring(0, 3);
                string origen;

                if (!CatalogoMunicipios.Municipios.TryGetValue(codigo, out origen))
                {
                    ConsolaUI.Error("El código de municipio no existe.");
                    continue;
                }

                DateTime fecha;

                if (!TryObtenerFechaNacimiento(cedula, out fecha))
                {
                    ConsolaUI.Error("La fecha incluida en la cédula no es válida.");
                    continue;
                }

                if (CalcularEdad(fecha) < 0)
                {
                    ConsolaUI.Error("La fecha de nacimiento no puede ser futura.");
                    continue;
                }

                return cedula;
            }
        }

        public static int ObtenerEdad(string cedula)
        {
            DateTime fecha;

            if (!TryObtenerFechaNacimiento(cedula, out fecha))
                throw new FormatException("La cédula tiene un formato o fecha incorrecta.");

            return CalcularEdad(fecha);
        }

        public static string ObtenerOrigenCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                return "Cédula inválida";

            string limpia = cedula.Replace("-", "").Trim();

            if (limpia.Length < 3)
                return "Cédula inválida";

            string origen;

            return CatalogoMunicipios.Municipios.TryGetValue(
                limpia.Substring(0, 3),
                out origen)
                ? origen
                : "Municipio inexistente";
        }

        private static bool TryObtenerFechaNacimiento(
            string cedula,
            out DateTime fecha)
        {
            fecha = DateTime.MinValue;

            if (string.IsNullOrWhiteSpace(cedula) ||
                cedula.Length != 16)
                return false;

            string datosFecha = cedula.Substring(4, 6);

            int dia;
            int mes;
            int anioCorto;

            if (!int.TryParse(datosFecha.Substring(0, 2), out dia) ||
                !int.TryParse(datosFecha.Substring(2, 2), out mes) ||
                !int.TryParse(datosFecha.Substring(4, 2), out anioCorto))
                return false;

            int actual = DateTime.Today.Year;

            int anio =
                anioCorto <= actual % 100
                ? 2000 + anioCorto
                : 1900 + anioCorto;

            try
            {
                fecha = new DateTime(anio, mes, dia);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        private static int CalcularEdad(DateTime nacimiento)
        {
            DateTime hoy = DateTime.Today;
            int edad = hoy.Year - nacimiento.Year;

            if (nacimiento.Date > hoy.AddYears(-edad))
                edad--;

            return edad;
        }
    }
}
