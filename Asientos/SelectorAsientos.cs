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
            Console.WriteLine();
            Console.WriteLine(
                "               PANTALLA");
            Console.WriteLine(
                "====================================");
            Console.WriteLine(
                "[X] Ocupado     [Número] Disponible");
            Console.WriteLine();

            var filas =
                datos.Asientos
                .Where(
                    x =>
                    x.FuncionId ==
                    funcionId)
                .OrderBy(
                    x => x.Fila)
                .ThenBy(
                    x => x.Numero)
                .GroupBy(
                    x => x.Fila);

            foreach (
                var fila
                in filas)
            {
                Console.Write(
                    fila.Key + "  ");

                foreach (
                    Asiento asiento
                    in fila)
                {
                    if (asiento.EstaOcupado)
                    {
                        Console.Write(
                            "[X]");
                    }
                    else
                    {
                        Console.Write(
                            "[" +
                            asiento.Numero +
                            "]");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
