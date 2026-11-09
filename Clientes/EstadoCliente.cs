using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Clientes
{
    public static class EstadoCliente
    {
        public static void Ejecutar(
            ContextoDatos datos,
            string usuario)
        {
            string id =
                Validador.LeerTexto(
                    "ID cliente: ",
                    3,
                    15)
                .ToUpper();

            Cliente cliente =
                datos.Clientes
                .FirstOrDefault(
                    x => x.Id == id);

            if (cliente == null)
            {
                ConsolaUI.Error(
                    "Cliente inexistente.");

                return;
            }

            bool boletoFuturo =
                datos.Reservas.Any(
                    r =>
                    r.Activo &&
                    r.ClienteId == id &&
                    ReglasCine.BuscarFuncion(
                        datos,
                        r.FuncionId) != null &&
                    ReglasCine.BuscarFuncion(
                        datos,
                        r.FuncionId)
                    .HoraInicio >
                    DateTime.Now);

            if (cliente.Activo &&
                boletoFuturo)
            {
                ConsolaUI.Error(
                    "El cliente tiene boletos vigentes.");

                return;
            }

            if (!Validador.Confirmar(
                cliente.Activo
                ? "¿Inactivar cliente?"
                : "¿Reactivar cliente?"))
                return;

            if (cliente.Activo)
                cliente.Desactivar();
            else
                cliente.Activar();

            ArchivoDatos.Guardar(
                datos);

            ArchivoLogs.Registrar(
                usuario,
                cliente.Activo
                ? "REACTIVAR CLIENTE"
                : "INACTIVAR CLIENTE",
                id);

            ConsolaUI.Exito(
                "Estado actualizado.");
        }
    }
}
