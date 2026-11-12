using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Excepciones;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;

namespace Cinema_Doomsday.Login
{
    public static class GestionUsuarios
    {
        public static void AsegurarVictor(
            ContextoDatos datos)
        {
            Usuario victor =
                datos.Usuarios
                .FirstOrDefault(
                    x =>
                        x.Id.Equals(
                            ReglasUsuarios.IdVictor,
                            StringComparison.OrdinalIgnoreCase));

            if (victor == null)
            {
                victor =
                    new Usuario(
                        ReglasUsuarios.IdVictor,
                        "Victor",
                        Seguridad.CrearHash(
                            "Vl22050*/"),
                        "Administrador");

                datos.Usuarios.Insert(
                    0,
                    victor);

                ArchivoDatos.Guardar(
                    datos);

                ArchivoLogs.Registrar(
                    "SISTEMA",
                    "CREAR USUARIO INICIAL",
                    victor.Id);

                return;
            }

            if (!string.Equals(
                victor.Rol,
                "Administrador",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new ReglaNegocioException(
                    "El usuario Víctor debe conservar " +
                    "el rol Administrador.");
            }

            if (!victor.Activo)
            {
                victor.Activar();

                ArchivoDatos.Guardar(
                    datos);

                ArchivoLogs.Registrar(
                    "SISTEMA",
                    "REACTIVAR USUARIO INICIAL",
                    victor.Id);
            }
        }

        public static Usuario Crear(
            ContextoDatos datos,
            Usuario usuarioActual,
            string nombreUsuario,
            string contrasena,
            string rol)
        {
            ReglasUsuarios.ValidarGestion(
                usuarioActual);

            if (string.IsNullOrWhiteSpace(
                nombreUsuario))
            {
                throw new ReglaNegocioException(
                    "El nombre de usuario es obligatorio.");
            }

            bool existe =
                datos.Usuarios
                .Any(
                    x =>
                        x.NombreUsuario.Equals(
                            nombreUsuario.Trim(),
                            StringComparison.OrdinalIgnoreCase));

            if (existe)
            {
                throw new ReglaNegocioException(
                    "El nombre de usuario ya existe.");
            }

            string id =
                GeneradorIds.SiguienteUsuario(
                    datos);

            Usuario nuevo =
                new Usuario(
                    id,
                    nombreUsuario.Trim(),
                    Seguridad.CrearHash(
                        contrasena),
                    rol.Trim());

            datos.Usuarios.Add(
                nuevo);

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuarioActual.NombreUsuario,
                "CREAR USUARIO",
                nuevo.Id);

            return nuevo;
        }

        public static void Desactivar(
            ContextoDatos datos,
            Usuario usuarioActual,
            string id)
        {
            ReglasUsuarios.ValidarGestion(
                usuarioActual);

            Usuario usuario =
                BuscarPorId(
                    datos,
                    id);

            ReglasUsuarios.ValidarEliminacionLogica(
                usuario);

            if (!usuario.Activo)
            {
                throw new ReglaNegocioException(
                    "El usuario ya está inactivo.");
            }

            usuario.Desactivar();

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuarioActual.NombreUsuario,
                "DESACTIVAR USUARIO",
                usuario.Id);
        }

        public static void Activar(
            ContextoDatos datos,
            Usuario usuarioActual,
            string id)
        {
            ReglasUsuarios.ValidarGestion(
                usuarioActual);

            Usuario usuario =
                BuscarPorId(
                    datos,
                    id);

            if (usuario.Activo)
            {
                throw new ReglaNegocioException(
                    "El usuario ya está activo.");
            }

            usuario.Activar();

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuarioActual.NombreUsuario,
                "ACTIVAR USUARIO",
                usuario.Id);
        }

        public static List<Usuario> ObtenerTodos(
            ContextoDatos datos,
            Usuario usuarioActual)
        {
            ReglasUsuarios.ValidarGestion(
                usuarioActual);

            return datos.Usuarios
                .OrderBy(
                    x => x.Id)
                .ToList();
        }

        static Usuario BuscarPorId(
            ContextoDatos datos,
            string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ReglaNegocioException(
                    "El identificador es obligatorio.");
            }

            Usuario usuario =
                datos.Usuarios
                .FirstOrDefault(
                    x =>
                        x.Id.Equals(
                            id.Trim(),
                            StringComparison.OrdinalIgnoreCase));

            if (usuario == null)
            {
                throw new ReglaNegocioException(
                    "El usuario no existe.");
            }

            return usuario;
        }
    }
}
