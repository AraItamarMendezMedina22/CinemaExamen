using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;

using System.Text;
using System.Threading.Tasks;

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
                    x.NombreUsuario
                    .ToLower() ==
                    "víctor");

            if (victor == null)
            {
                victor =
                    new Usuario(
                        "USR001",
                        "Víctor",
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
                    "Víctor");
            }
            else if (!victor.Activo)
            {
                victor.Activar();

                ArchivoDatos.Guardar(
                    datos);
            }
        }
    }
}
