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
      List<Sala> resultado =
 datos.Salas
 .OrderBy(
     x => x.Id)
 .ToList();

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
