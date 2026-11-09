using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Salas
{
    public static class EditarSala
    {
        public static void Ejecutar(
            ContextoDatos datos,
            string usuario)
        {
            string id =
                Validador.LeerTexto(
                    "ID sala: ",
                    3,
                    12)
                .ToUpper();

            Sala sala =
                datos.Salas
                .FirstOrDefault(
                    x => x.Id == id);

            if (sala == null)
            {
                ConsolaUI.Error(
                    "Sala inexistente.");

                return;
            }

            if (!sala.Activo)
            {
                ConsolaUI.Error(
                    "Debe reactivarla primero.");

                return;
            }

            int numero =
                Validador.LeerEntero(
                    "Nuevo número: ",
                    1,
                    99);

            if (datos.Salas.Any(
                x =>
                x.Id != id &&
                x.NumeroSala ==
                numero))
            {
                ConsolaUI.Error(
                    "Ese número ya existe.");

                return;
            }

            Console.WriteLine(
                "1. 2D");
            Console.WriteLine(
                "2. 3D");
            Console.WriteLine(
                "3. IMAX");

            int tipo =
                Validador.LeerOpcion(
                    1,
                    3);

            sala.Editar(
                numero,
                tipo == 1
                ? "2D"
                : tipo == 2
                ? "3D"
                : "IMAX");

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "EDITAR SALA",
                id);

            ConsolaUI.Exito(
                "Sala editada.");
        }
    }


}
