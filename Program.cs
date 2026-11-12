using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinema_Doomsday.Archivos;
using Cinema_Doomsday.Login;
using Cinema_Doomsday.Menus;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;

using System.Threading.Tasks;

namespace Cinema_Doomsday
{
    class Program
    {
        static void Main(
            string[] args)
        {
            Console.OutputEncoding =
                Encoding.UTF8;

            Console.Title =
                "Cinema Doomsday";

            ContextoDatos datos;

            try
            {
                datos =
                    ArchivoDatos.Cargar();
            }
            catch (Exception ex)
            {
                ConsolaUI.Error(
                    "No se pudo cargar la información.");

                ConsolaUI.Error(
                    ex.Message);

                ConsolaUI.Pausa();

                return;
            }

            try
            {
                GestionUsuarios
                    .AsegurarVictor(
                        datos);
            }
            catch (Exception ex)
            {
                ConsolaUI.Error(
                    "Error al preparar el sistema: " +
                    ex.Message);

                ConsolaUI.Pausa();
            }

            while (true)
            {
                Usuario usuario =
                    Autenticacion.Iniciar(
                        datos);

                if (usuario == null)
                    continue;

                MenuPrincipal.Mostrar(
                    datos,
                    usuario);
            }
        }
    }
}
