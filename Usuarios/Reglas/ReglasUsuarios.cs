using Cinema_Doomsday.Excepciones;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System;

namespace Cinema_Doomsday.Login
{
    public static class ReglasUsuarios
    {
        public const string IdVictor =
            "USR001";

        public static bool EsVictor(
            Usuario usuario)
        {
            return usuario != null &&
                string.Equals(
                    usuario.Id,
                    IdVictor,
                    StringComparison.OrdinalIgnoreCase);
        }

        public static bool PuedeGestionarUsuarios(
            Usuario usuario)
        {
            return usuario != null &&
                (
                    EsVictor(usuario) ||
                    string.Equals(
                        usuario.Rol,
                        "Administrador",
                        StringComparison.OrdinalIgnoreCase)
                );
        }

        public static void ValidarGestion(
            Usuario usuario)
        {
            if (!PuedeGestionarUsuarios(usuario))
            {
                throw new ReglaNegocioException(
                    "El usuario no tiene permisos " +
                    "para gestionar usuarios.");
            }
        }

        public static void ValidarEliminacionLogica(
            Usuario usuario)
        {
            if (EsVictor(usuario))
            {
                throw new ReglaNegocioException(
                    "Víctor es el administrador máximo " +
                    "y no puede ser desactivado.");
            }
        }

        public static void ValidarContrasena(
               string clave)
        {
            if (!Validador.ContrasenaSegura(
                clave))
            {
                throw new ReglaNegocioException(
                    "La contraseña debe tener de 8 a 20 " +
                    "caracteres, mayúscula, minúscula, " +
                    "número y símbolo.");
            }
        }
    }
}
