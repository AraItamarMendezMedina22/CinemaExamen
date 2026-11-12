using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cinema_Doomsday.Logs;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Exportacion
{
    public static class Exportador
    {
        public static void Ejecutar(
            ContextoDatos datos,
            string usuario)
        {
            ConsolaUI.Encabezado(
                "EXPORTACIÓN DE DATOS");

            List<string> opciones =
                new List<string>
                {
                    "Películas",
                    "Salas",
                    "Funciones",
                    "Clientes",
                    "Reservas",
                    "Todo"
                };

            int modulo =
                ConsolaUI.LeerMenu(
                    opciones,
                    true);

            if (modulo == 0)
                return;

            ConsolaUI.Encabezado(
                "FILTRO DE EXPORTACIÓN");

            List<string> filtros =
                new List<string>
                {
                    "Activos",
                    "Inactivos",
                    "Todos"
                };

            int filtro =
                ConsolaUI.LeerMenu(
                    filtros,
                    true);

            if (filtro == 0)
                return;

            try
            {
                List<string> lineas =
                    new List<string>();

                if (modulo == 1 ||
                    modulo == 6)
                {
                    AgregarPeliculas(
                        datos,
                        filtro,
                        lineas);
                }

                if (modulo == 2 ||
                    modulo == 6)
                {
                    AgregarSalas(
                        datos,
                        filtro,
                        lineas);
                }

                if (modulo == 3 ||
                    modulo == 6)
                {
                    AgregarFunciones(
                        datos,
                        filtro,
                        lineas);
                }

                if (modulo == 4 ||
                    modulo == 6)
                {
                    AgregarClientes(
                        datos,
                        filtro,
                        lineas);
                }

                if (modulo == 5 ||
                    modulo == 6)
                {
                    AgregarReservas(
                        datos,
                        filtro,
                        lineas);
                }

                if (lineas.Count == 0)
                {
                    ConsolaUI.Info(
                        "No hay datos para exportar.");

                    return;
                }

                string carpeta =
                    Path.Combine(
                        AppDomain.CurrentDomain
                            .BaseDirectory,
                        "Exportaciones");

                Directory.CreateDirectory(
                    carpeta);

                string ruta =
                    Path.Combine(
                        carpeta,
                        "CinemaDoomsday_" +
                        DateTime.Now.ToString(
                            "yyyyMMdd_HHmmss") +
                        ".csv");

                File.WriteAllLines(
                    ruta,
                    lineas,
                    Encoding.UTF8);

                ArchivoLogs.Registrar(
                    usuario,
                    "EXPORTAR DATOS",
                    ruta);

                ConsolaUI.Exito(
                    "Exportación realizada.");

                Console.WriteLine(
                    ruta);
            }
            catch (Exception ex)
            {
                ConsolaUI.Error(
                    "No fue posible exportar: " +
                    ex.Message);
            }
        }

        static bool Pasa(
            bool activo,
            int filtro)
        {
            return filtro == 3 ||
                filtro == 1 &&
                activo ||
                filtro == 2 &&
                !activo;
        }

        static void AgregarPeliculas(
            ContextoDatos d,
            int filtro,
            List<string> lineas)
        {
            foreach (
                Pelicula x
                in d.Peliculas.Where(
                    x =>
                    Pasa(
                        x.Activo,
                        filtro)))
            {
                lineas.Add(
                    "PELICULA;" +
                    x.Id + ";" +
                    x.Titulo + ";" +
                    x.Duracion + ";" +
                    x.Genero + ";" +
                    x.Clasificacion);
            }
        }

        static void AgregarSalas(
            ContextoDatos d,
            int filtro,
            List<string> lineas)
        {
            foreach (
                Sala x
                in d.Salas.Where(
                    x =>
                    Pasa(
                        x.Activo,
                        filtro)))
            {
                lineas.Add(
                    "SALA;" +
                    x.Id + ";" +
                    x.NumeroSala + ";" +
                    x.Capacidad + ";" +
                    x.TipoSala);
            }
        }

        static void AgregarFunciones(
            ContextoDatos d,
            int filtro,
            List<string> lineas)
        {
            foreach (
                Funcion x
                in d.Funciones.Where(
                    x =>
                    Pasa(
                        x.Activo,
                        filtro)))
            {
                lineas.Add(
                    "FUNCION;" +
                    x.Id + ";" +
                    x.PeliculaId + ";" +
                    x.SalaId + ";" +
                    x.HoraInicio + ";" +
                    x.Precio);
            }
        }

        static void AgregarClientes(
            ContextoDatos d,
            int filtro,
            List<string> lineas)
        {
            foreach (
                Cliente x
                in d.Clientes.Where(
                    x =>
                    Pasa(
                        x.Activo,
                        filtro)))
            {
                lineas.Add(
                    "CLIENTE;" +
                    x.Id + ";" +
                    x.Nombre + ";" +
                    x.Correo + ";" +
                    x.Telefono);
            }
        }

        static void AgregarReservas(
            ContextoDatos d,
            int filtro,
            List<string> lineas)
        {
            foreach (
                Reserva x
                in d.Reservas.Where(
                    x =>
                    Pasa(
                        x.Activo,
                        filtro)))
            {
                lineas.Add(
                    "BOLETO;" +
                    x.Id + ";" +
                    x.ClienteId + ";" +
                    x.FuncionId + ";" +
                    string.Join(
                        "|",
                        x.ListaDeAsientos) +
                    ";" +
                    x.TotalPagar + ";" +
                    x.MetodoPago);
            }
        }
    }
}
