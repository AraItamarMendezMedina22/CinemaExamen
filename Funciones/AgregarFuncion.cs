using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Funciones
{
    public static class AgregarFuncion
    {
        public static void Ejecutar(
            ContextoDatos datos,
            string usuario)
        {
            string id =
                Validador.LeerTexto(
                    "ID función: ",
                    3,
                    12)
                .ToUpper();

            if (datos.Funciones.Any(
                x => x.Id == id))
            {
                ConsolaUI.Error(
                    "ID ocupado.");

                return;
            }

            string peliculaId =
                Validador.LeerTexto(
                    "ID película: ",
                    3,
                    12)
                .ToUpper();

            Pelicula pelicula =
                ReglasCine.BuscarPelicula(
                    datos,
                    peliculaId);

            if (pelicula == null ||
                !pelicula.Activo)
            {
                ConsolaUI.Error(
                    "Película inexistente o inactiva.");

                return;
            }

            string salaId =
                Validador.LeerTexto(
                    "ID sala: ",
                    3,
                    12)
                .ToUpper();

            Sala sala =
                ReglasCine.BuscarSala(
                    datos,
                    salaId);

            if (sala == null ||
                !sala.Activo)
            {
                ConsolaUI.Error(
                    "Sala inexistente o inactiva.");

                return;
            }

            DateTime inicio =
                Validador.LeerFechaHora(
                    "Fecha/hora dd/MM/yyyy HH:mm: ");

            if (inicio <=
                DateTime.Now)
            {
                ConsolaUI.Error(
                    "La función debe ser futura.");

                return;
            }

            if (ReglasCine.HayChoqueSala(
                datos,
                salaId,
                peliculaId,
                inicio,
                null))
            {
                ConsolaUI.Error(
                    "La sala está ocupada en ese horario.");

                return;
            }

            decimal precio =
                Validador.LeerDecimal(
                    "Precio C$: ",
                    50,
                    1000);

            Funcion funcion =
                new Funcion(
                    id,
                    peliculaId,
                    salaId,
                    inicio,
                    precio);

            datos.Funciones.Add(
                funcion);

            ReglasCine.GenerarAsientos(
                datos,
                funcion,
                sala);

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "AGREGAR FUNCIÓN",
                id);

            ConsolaUI.Exito(
                "Función y asientos creados.");
        }
    }
    
}
