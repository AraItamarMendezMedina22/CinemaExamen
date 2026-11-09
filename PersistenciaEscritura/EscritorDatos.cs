using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Cinema_Doomsday.Modelos;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.PersistenciaEscritura
{
    public static class EscritorDatos
    {
        public static void Escribir(
            BinaryWriter writer,
            ContextoDatos datos)
        {
            Usuarios(
                writer,
                datos);

            Peliculas(
                writer,
                datos);

            Salas(
                writer,
                datos);

            Funciones(
                writer,
                datos);

            Asientos(
                writer,
                datos);

            Clientes(
                writer,
                datos);

            Reservas(
                writer,
                datos);
        }

        static void Usuarios(
            BinaryWriter w,
            ContextoDatos d)
        {
            w.Write(
                d.Usuarios.Count);

            foreach (
                Usuario x
                in d.Usuarios)
            {
                w.Write(x.Id);
                w.Write(x.Activo);
                w.Write(x.NombreUsuario);
                w.Write(x.ContrasenaHash);
                w.Write(x.Rol);
            }
        }

        static void Peliculas(
            BinaryWriter w,
            ContextoDatos d)
        {
            w.Write(
                d.Peliculas.Count);

            foreach (
                Pelicula x
                in d.Peliculas)
            {
                w.Write(x.Id);
                w.Write(x.Activo);
                w.Write(x.Titulo);
                w.Write(x.Duracion);
                w.Write(x.Genero);
                w.Write(x.Clasificacion);
            }
        }

        static void Salas(
            BinaryWriter w,
            ContextoDatos d)
        {
            w.Write(
                d.Salas.Count);

            foreach (
                Sala x
                in d.Salas)
            {
                w.Write(x.Id);
                w.Write(x.Activo);
                w.Write(x.NumeroSala);
                w.Write(x.Capacidad);
                w.Write(x.TipoSala);
            }
        }

        static void Funciones(
            BinaryWriter w,
            ContextoDatos d)
        {
            w.Write(
                d.Funciones.Count);

            foreach (
                Funcion x
                in d.Funciones)
            {
                w.Write(x.Id);
                w.Write(x.Activo);
                w.Write(x.PeliculaId);
                w.Write(x.SalaId);
                w.Write(
                    x.HoraInicio.Ticks);
                w.Write(x.Precio);
            }
        }

        static void Asientos(
            BinaryWriter w,
            ContextoDatos d)
        {
            w.Write(
                d.Asientos.Count);

            foreach (
                Asiento x
                in d.Asientos)
            {
                w.Write(x.Id);
                w.Write(x.FuncionId);
                w.Write(x.Fila);
                w.Write(x.Numero);
                w.Write(x.EstaOcupado);
            }
        }

        static void Clientes(
            BinaryWriter w,
            ContextoDatos d)
        {
            w.Write(
                d.Clientes.Count);

            foreach (
                Cliente x
                in d.Clientes)
            {
                w.Write(x.Id);
                w.Write(x.Activo);
                w.Write(x.Nombre);
                w.Write(x.Correo);
                w.Write(x.Telefono);
            }
        }

        static void Reservas(
            BinaryWriter w,
            ContextoDatos d)
        {
            w.Write(
                d.Reservas.Count);

            foreach (
                Reserva x
                in d.Reservas)
            {
                w.Write(x.Id);
                w.Write(x.Activo);
                w.Write(x.FuncionId);
                w.Write(x.ClienteId);
                w.Write(x.TotalPagar);
                w.Write(x.MetodoPago);
                w.Write(x.DetallePago);
                w.Write(x.Cambio);
                w.Write(
                    x.FechaCompra.Ticks);

                w.Write(
                    x.ListaDeAsientos.Count);

                foreach (
                    string asiento
                    in x.ListaDeAsientos)
                {
                    w.Write(asiento);
                }
            }
        }
    }
}
