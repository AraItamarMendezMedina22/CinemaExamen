using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Salas
{
    public static class BuscarSala
    {
        public static void Ejecutar(
            ContextoDatos datos)
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

            Console.WriteLine(
                "Sala número: " +
                sala.NumeroSala);

            Console.WriteLine(
                "Capacidad: " +
                sala.Capacidad);

            Console.WriteLine(
                "Tipo: " +
                sala.TipoSala);

            Console.WriteLine(
                "Estado: " +
                (sala.Activo
                    ? "Activo"
                    : "Inactivo"));
        }
    }
}
