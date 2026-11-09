using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Doomsday.Modelos;

namespace Cinema_Doomsday.Utilidades
{
    public static class ReglasCine
    {
        public static Pelicula BuscarPelicula(
            ContextoDatos datos,
            string id)
        {
            return datos.Peliculas
                .FirstOrDefault(
                    x => x.Id == id);
        }

        public static Sala BuscarSala(
            ContextoDatos datos,
            string id)
        {
            return datos.Salas
                .FirstOrDefault(
                    x => x.Id == id);
        }

        public static Funcion BuscarFuncion(
            ContextoDatos datos,
            string id)
        {
            return datos.Funciones
                .FirstOrDefault(
                    x => x.Id == id);
        }

        public static Cliente BuscarCliente(
            ContextoDatos datos,
            string id)
        {
            return datos.Clientes
                .FirstOrDefault(
                    x => x.Id == id);
        }

        public static bool HayChoqueSala(
            ContextoDatos datos,
            string salaId,
            string peliculaId,
            DateTime inicio,
            string ignorarFuncion)
        {
            Pelicula nueva =
                BuscarPelicula(
                    datos,
                    peliculaId);

            if (nueva == null)
                return true;

            DateTime finNueva =
                inicio.AddMinutes(
                    nueva.Duracion + 20);

            foreach (
                Funcion funcion
                in datos.Funciones.Where(
                    x =>
                    x.Activo &&
                    x.SalaId == salaId &&
                    x.Id != ignorarFuncion))
            {
                Pelicula pelicula =
                    BuscarPelicula(
                        datos,
                        funcion.PeliculaId);

                if (pelicula == null)
                    continue;

                DateTime finExistente =
                    funcion.HoraInicio
                    .AddMinutes(
                        pelicula.Duracion + 20);

                if (inicio < finExistente &&
                    finNueva >
                    funcion.HoraInicio)
                {
                    return true;
                }
            }

            return false;
        }

        public static void GenerarAsientos(
            ContextoDatos datos,
            Funcion funcion,
            Sala sala)
        {
            if (datos.Asientos.Any(
                x =>
                x.FuncionId ==
                funcion.Id))
                return;

            int creados = 0;
            char fila = 'A';

            while (
                creados <
                sala.Capacidad)
            {
                for (
                    int numero = 1;
                    numero <= 10 &&
                    creados <
                    sala.Capacidad;
                    numero++)
                {
                    string id =
                        funcion.Id +
                        "-" +
                        fila +
                        numero;

                    datos.Asientos.Add(
                        new Asiento(
                            id,
                            funcion.Id,
                            fila.ToString(),
                            numero));

                    creados++;
                }

                fila++;
            }
        }
    }
}
