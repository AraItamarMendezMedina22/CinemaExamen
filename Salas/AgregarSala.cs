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
    public static class AgregarSala
    {
        public static void Ejecutar(
            ContextoDatos datos,
            string usuario)
        {
         

                int numero =
                Validador.LeerEntero(
                    "Número de sala: ",
                    1,
                    99);

            if (datos.Salas.Any(
                x =>
                x.NumeroSala ==
                numero))
            {
                ConsolaUI.Error(
                    "Ese número de sala ya existe.");

                return;
            }

            int capacidad =
                Validador.LeerEntero(
                    "Capacidad: ",
                    10,
                    200);

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

            string nombreTipo =
                tipo == 1
                ? "2D"
                : tipo == 2
                ? "3D"
                : "IMAX";

            string id =
    GeneradorIds.SiguienteSala(
        datos.Salas);

            datos.Salas.Add(
                new Sala(
                    id,
                    numero,
                    capacidad,
                    nombreTipo));

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "AGREGAR SALA",
                id);

            ConsolaUI.Exito(
                "Sala registrada" + id + ".");
        }
    }
}
