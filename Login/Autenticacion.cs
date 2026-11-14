using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Login
{
    public static class Autenticacion
    {
        public static Usuario Iniciar(
            ContextoDatos datos)
        {
            for (
                int intento = 1;
                intento <= 3;
                intento++)
            {
                ConsolaUI.Encabezado(
                    "INICIO DE SESIÓN");

                Console.Write(
                    "Usuario: ");

                string usuario =
                    Console.ReadLine();

                Console.Write(
                    "Contraseña: ");

                string clave =
                    LeerClave();

                if (string.IsNullOrWhiteSpace(
                    usuario) ||
                    string.IsNullOrWhiteSpace(
                    clave))
                {
                    ArchivoLogs.Registrar(
                        usuario,
                        "LOGIN FALLIDO",
                        "Campos vacíos");

                    ConsolaUI.Error(
                        "Credenciales incorrectas.");

                    ConsolaUI.Pausa();

                    continue;
                }

                string hash =
                    Seguridad.CrearHash(
                        clave);

                string usuarioNormalizado =
                    NormalizarUsuario(
                        usuario);

                Usuario encontrado =
                    datos.Usuarios
                    .FirstOrDefault(
                        x =>
                        NormalizarUsuario(
                            x.NombreUsuario) ==
                        usuarioNormalizado
                        &&
                        x.ContrasenaHash ==
                        hash);

                if (encontrado != null &&
                    encontrado.EstaBloqueado)
                {
                    ArchivoLogs.Registrar(
                        usuario,
                        "LOGIN BLOQUEADO",
                        "Bloqueo temporal");

                    ConsolaUI.Error(
                        "Acceso temporalmente bloqueado.");

                    ConsolaUI.Pausa();

                    return null;
                }


                if (encontrado != null &&
                    encontrado.Activo &&
                    encontrado.ContrasenaHash ==
                    hash)
                {
                    encontrado.ReiniciarAcceso();

                    ArchivoLogs.Registrar(
                        encontrado.NombreUsuario,
                        "LOGIN EXITOSO",
                        "Inicio de sesión");

                    return encontrado;
                }

                if (encontrado != null)
                {
                    encontrado.RegistrarFallo();
                }

                ArchivoLogs.Registrar(
                    usuario.Trim(),
                    "LOGIN FALLIDO",
                    "Credenciales incorrectas");

                ConsolaUI.Error(
                    "Credenciales incorrectas.");

                Console.WriteLine(
                    "Intentos restantes: " +
                    (3 - intento));

                ConsolaUI.Pausa();
            }

            ConsolaUI.Error(
                "Se agotaron los intentos.");

            ConsolaUI.Pausa();

            return null;
        }

        public static void Cerrar(
            Usuario usuario)
        {
            ArchivoLogs.Registrar(
                usuario.NombreUsuario,
                "CIERRE DE SESIÓN",
                "Sesión finalizada");
        }

        static string NormalizarUsuario(
            string valor)
        {
            if (string.IsNullOrWhiteSpace(
                valor))
            {
                return "";
            }

            string normalizado =
                valor.Trim()
                .Normalize(
                    NormalizationForm.FormD);

            StringBuilder resultado =
                new StringBuilder();

            foreach (
                char caracter
                in normalizado)
            {
                UnicodeCategory categoria =
                    CharUnicodeInfo.GetUnicodeCategory(
                        caracter);

                if (categoria !=
                    UnicodeCategory.NonSpacingMark)
                {
                    resultado.Append(
                        caracter);
                }
            }

            return resultado
                .ToString()
                .Normalize(
                    NormalizationForm.FormC)
                .ToUpperInvariant();
        }

        static string LeerClave()
        {
            string texto =
                "";

            ConsoleKeyInfo tecla;

            do
            {
                tecla =
                    Console.ReadKey(
                        true);

                if (tecla.Key ==
                    ConsoleKey.Backspace)
                {
                    if (texto.Length > 0)
                    {
                        texto =
                            texto.Substring(
                                0,
                                texto.Length - 1);

                        Console.Write(
                            "\b \b");
                    }
                }
                else if (
                    tecla.Key !=
                    ConsoleKey.Enter &&
                    !char.IsControl(
                        tecla.KeyChar))
                {
                    texto +=
                        tecla.KeyChar;

                    Console.Write(
                        "*");
                }
            }
            while (
                tecla.Key !=
                ConsoleKey.Enter);

            Console.WriteLine();

            return texto;
        }
    }
}
