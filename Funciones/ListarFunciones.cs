using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Funciones
{
    public static class ListarFunciones
    {
        public static void Ejecutar(
            ContextoDatos datos)
        {
      List<Funcion> resultado =
  datos.Funciones
  .OrderBy(
      x => x.HoraInicio)
  .ToList();

      if (resultado.Count == 0)
      {
        ConsolaUI.Info(
            "No hay funciones.");

        return;
      }
    

            foreach (
                Funcion funcion
                in resultado)
            {
                Pelicula pelicula =
                    ReglasCine.BuscarPelicula(
                        datos,
                        funcion.PeliculaId);

                Sala sala =
                    ReglasCine.BuscarSala(
                        datos,
                        funcion.SalaId);

                int libres =
                    datos.Asientos.Count(
                        x =>
                        x.FuncionId ==
                        funcion.Id &&
                        !x.EstaOcupado);

                Console.WriteLine(
                    funcion.Id +
                    " | " +
                    pelicula.Titulo +
                    " | Sala " +
                    sala.NumeroSala +
                    " " +
                    sala.TipoSala +
                    " | " +
                    funcion.HoraInicio
                    .ToString(
                        "dd/MM HH:mm") +
                    " | C$" +
                    funcion.Precio +
                    " | Libres: " +
                    libres);
            }
        }
    }
}
