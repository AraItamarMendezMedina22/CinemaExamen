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
        private static readonly HashSet<string>
            DominiosGratuitos =
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

                if (Regex.IsMatch(
                    nombre,
                    @"^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s]+$"))
                {
                    return nombre;
                }

                ConsolaUI.Error(
                    "El nombre solo puede contener letras.");
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

                string dominio =
                    correo.Split('@')[1];

                if (!DominiosGratuitos.Contains(
                    dominio))
                {
                    ConsolaUI.Error(
                        "Solo se permiten correos " +
                        "de dominios gratuitos.");

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

                if (Regex.IsMatch(
                    telefono,
                    @"^[2578]\d{7}$"))
                {
                    return telefono;
                }

                ConsolaUI.Error(
                    "Teléfono inválido. Debe tener " +
                    "8 dígitos e iniciar con 2, 5, 7 u 8.");
            }
        }

        public static bool ContrasenaSegura(
            string clave)
        {
            return
                !string.IsNullOrWhiteSpace(clave) &&
                clave.Length >= 8 &&
                clave.Length <= 20 &&
                clave.Any(char.IsUpper) &&
                clave.Any(char.IsLower) &&
                clave.Any(char.IsDigit) &&
                clave.Any(
                    c =>
                    !char.IsLetterOrDigit(c));
        }
    }
}