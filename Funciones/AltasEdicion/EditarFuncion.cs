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
    public static class EditarFuncion
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

            if (!funcion.Activo)
            {
                ConsolaUI.Error(
                    "Debe reactivarla primero.");

                return;
            }

            if (datos.Reservas.Any(
                x =>
                x.Activo &&
                x.FuncionId == id))
            {
                ConsolaUI.Error(
                    "No se puede editar una función con boletos vendidos.");

                return;
            }

            string peliculaId =
                Validador.LeerTexto(
                    "ID película: ",
                    3,
                    12)
                .ToUpper();

            string salaId =
                Validador.LeerTexto(
                    "ID sala: ",
                    3,
                    12)
                .ToUpper();

            Pelicula pelicula =
                ReglasCine.BuscarPelicula(
                    datos,
                    peliculaId);

            Sala sala =
                ReglasCine.BuscarSala(
                    datos,
                    salaId);

            if (pelicula == null ||
                !pelicula.Activo ||
                sala == null ||
                !sala.Activo)
            {
                ConsolaUI.Error(
                    "Película o sala inválida.");

                return;
            }

            DateTime inicio =
                Validador.LeerFechaHora(
                    "Nueva fecha/hora: ");

            if (inicio <=
                DateTime.Now)
            {
                ConsolaUI.Error(
                    "La fecha debe ser futura.");

                return;
            }

            if (ReglasCine.HayChoqueSala(
                datos,
                salaId,
                peliculaId,
                inicio,
                id))
            {
                ConsolaUI.Error(
                    "Existe un choque de horario.");

                return;
            }

            datos.Asientos.RemoveAll(
                x =>
                x.FuncionId == id);

            funcion.Editar(
                peliculaId,
                salaId,
                inicio);

            ReglasCine.GenerarAsientos(
                datos,
                funcion,
                sala);

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                "EDITAR FUNCIÓN",
                id);

            ConsolaUI.Exito(
                "Función editada.");
        }
    }
}
