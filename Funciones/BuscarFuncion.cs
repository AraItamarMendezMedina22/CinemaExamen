using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Funciones
{
    public static class BuscarFuncion
    {
        public static void Ejecutar(
            ContextoDatos datos)
        {
            string id =
                Validador.LeerTexto(
                    "ID función: ",
                    3,
                    12)
                .ToUpper();

            Funcion funcion =
                datos.Funciones
                .FirstOrDefault(
                    x => x.Id == id);

            if (funcion == null)
            {
                ConsolaUI.Error(
                    "Función inexistente.");

                return;
            }

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
                    x.FuncionId == id &&
                    !x.EstaOcupado);

            Console.WriteLine(
                "Película: " +
                pelicula.Titulo);

            Console.WriteLine(
                "Sala: " +
                sala.NumeroSala +
                " - " +
                sala.TipoSala);

            Console.WriteLine(
                "Inicio: " +
                funcion.HoraInicio.ToString(
                    "dd/MM/yyyy HH:mm"));

            Console.WriteLine(
                "Precio: C$" +
                funcion.Precio.ToString(
                    "N2"));

            Console.WriteLine(
                "Asientos libres: " +
                libres);

            Console.WriteLine(
                "Estado: " +
                (funcion.Activo
                    ? "Activo"
                    : "Inactivo"));
        }
    }
}
