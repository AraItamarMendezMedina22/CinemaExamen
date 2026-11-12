using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Utilidades
{
    public static class ConsolaUI
    {
        public static void Encabezado(
            string titulo)
        {
            Console.Clear();

            Console.ForegroundColor =
                ConsoleColor.Cyan;

            Console.WriteLine(
                "╔══════════════════════════════════════════════════════╗");

            Console.WriteLine(
                "║                   CINEMA DOOMSDAY                    ║");

            Console.WriteLine(
                "╠══════════════════════════════════════════════════════╣");

            Console.WriteLine(
                "║ " +
                titulo.PadRight(52) +
                " ║");

            Console.WriteLine(
                "╚══════════════════════════════════════════════════════╝");

            Console.ResetColor();

            Console.WriteLine();
        }

        public static void Exito(
            string mensaje)
        {
            Console.ForegroundColor =
                ConsoleColor.Green;

            Console.WriteLine(
                "[OK] " + mensaje);

            Console.ResetColor();
        }

        public static void Error(
            string mensaje)
        {
            Console.ForegroundColor =
                ConsoleColor.Red;

            Console.WriteLine(
                "[ERROR] " + mensaje);

            Console.ResetColor();
        }

        public static void Info(
            string mensaje)
        {
            Console.ForegroundColor =
                ConsoleColor.Yellow;

            Console.WriteLine(
                mensaje);

            Console.ResetColor();
        }

        public static void Pausa()
        {
            Console.ForegroundColor =
                ConsoleColor.DarkGray;

            Console.WriteLine();

            Console.Write(
                "Presione ENTER para continuar...");

            Console.ResetColor();

            Console.ReadLine();
        }

        public static int LeerMenu(
            IList<string> opciones,
            bool incluirVolver)
        {
            if (opciones == null ||
                opciones.Count == 0)
            {
                throw new ArgumentException(
                    "El menú no contiene opciones.",
                    "opciones");
            }

            for (
                int i = 0;
                i < opciones.Count;
                i++)
            {
                Console.WriteLine(
                    (i + 1) +
                    ". " +
                    opciones[i]);
            }

            if (incluirVolver)
            {
                Console.WriteLine(
                    "0. Volver");
            }

            int minimo =
                incluirVolver
                    ? 0
                    : 1;

            return Validador.LeerOpcion(
                minimo,
                opciones.Count);
        }
    }
}
