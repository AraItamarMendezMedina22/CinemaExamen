using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Asientos
{
    public static class SelectorAsientos
    {
        public static List<Asiento> Seleccionar(
            ContextoDatos datos,
            string funcionId)
        {
            List<Asiento> disponibles =
                datos.Asientos
                .Where(
                    x =>
                    x.FuncionId == funcionId &&
                    !x.EstaOcupado)
                .ToList();

            if (disponibles.Count == 0)
            {
                ConsolaUI.Error(
                    "La función está agotada.");

                return null;
            }

            MostrarMapa(
                datos,
                funcionId);

            int cantidad =
                Validador.LeerEntero(
                    "Cantidad de entradas (1-8): ",
                    1,
                    8);

            if (cantidad >
                disponibles.Count)
            {
                ConsolaUI.Error(
                    "No existen suficientes asientos.");

                return null;
            }

            List<Asiento> seleccionados =
                new List<Asiento>();

            for (
                int i = 1;
                i <= cantidad;
                i++)
            {
                string codigo =
                    Validador.LeerTexto(
                        "Asiento " +
                        i +
                        " (Ej. A1): ",
                        2,
                        4)
                    .ToUpper();

                Asiento asiento =
                    datos.Asientos
                    .FirstOrDefault(
                        x =>
                        x.FuncionId ==
                        funcionId &&
                        x.Codigo()
                        .ToUpper() ==
                        codigo);

                if (asiento == null)
                {
                    ConsolaUI.Error(
                        "Ese asiento no existe.");

                    i--;
                    continue;
                }

                if (asiento.EstaOcupado)
                {
                    ConsolaUI.Error(
                        "Ese asiento ya está ocupado.");

                    i--;
                    continue;
                }

                if (seleccionados.Contains(
                    asiento))
                {
                    ConsolaUI.Error(
                        "Ya seleccionó ese asiento.");

                    i--;
                    continue;
                }

                seleccionados.Add(
                    asiento);
            }

            return seleccionados;
        }

        public static void MostrarMapa(
    ContextoDatos datos,
    string funcionId)
        {
            List<Asiento> asientos =
                datos.Asientos
                .Where(
                    x => x.FuncionId ==
                         funcionId)
                .OrderBy(
                    x => x.Fila)
                .ThenBy(
                    x => x.Numero)
                .ToList();

            if (asientos.Count == 0)
            {
                ConsolaUI.Info(
                    "No existen asientos.");

                return;
            }

            List<string> filas =
                asientos
                .Select(x => x.Fila)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            int columnas =
                asientos.Max(
                    x => x.Numero);

            Asiento[,] matriz =
                new Asiento[
                    filas.Count,
                    columnas];

            foreach (Asiento asiento
                     in asientos)
            {
                int fila =
                    filas.IndexOf(
                        asiento.Fila);

                matriz[
                    fila,
                    asiento.Numero - 1] =
                    asiento;
            }

            Console.WriteLine();
            Console.WriteLine(
                "              PANTALLA");
            Console.WriteLine(
                "================================");

            Console.Write("    ");

            for (int c = 1;
                 c <= columnas;
                 c++)
            {
                Console.Write(
                    c.ToString()
                    .PadLeft(3));
            }

            Console.WriteLine();

            for (int f = 0;
                 f < filas.Count;
                 f++)
            {
                Console.Write(
                    filas[f]
                    .PadRight(4));

                for (int c = 0;
                     c < columnas;
                     c++)
                {
                    Asiento asiento =
                        matriz[f, c];

                    if (asiento == null)
                    {
                        Console.Write(
                            "   ");

                        continue;
                    }

                    Console.Write(
                        asiento.EstaOcupado
                            ? "[X]"
                            : "[ ]");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine(
                "[ ] Disponible   [X] Ocupado");
        }
    }
}
