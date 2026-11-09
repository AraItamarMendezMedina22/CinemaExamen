using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Salas
{
     public static class ListarSalas
    {
        public static void Ejecutar(
            ContextoDatos datos)
        {
            Console.WriteLine(
                "1. Activas");

            Console.WriteLine(
                "2. Inactivas");

            Console.WriteLine(
                "3. Todas");

            int opcion =
                Validador.LeerOpcion(
                    1,
                    3);

            IEnumerable<Sala> lista =
                datos.Salas;

            if (opcion == 1)
                lista =
                    lista.Where(
                        x => x.Activo);

            if (opcion == 2)
                lista =
                    lista.Where(
                        x => !x.Activo);

            List<Sala> resultado =
                lista.ToList();

            if (resultado.Count == 0)
            {
                ConsolaUI.Info(
                    "No hay salas.");

                return;
            }

            foreach (
                Sala sala
                in resultado)
            {
                Console.WriteLine(
                    sala.Id +
                    " | Sala " +
                    sala.NumeroSala +
                    " | " +
                    sala.TipoSala +
                    " | Capacidad " +
                    sala.Capacidad +
                    " | " +
                    (sala.Activo
                        ? "Activo"
                        : "Inactivo"));
            }
        }
    }
}
