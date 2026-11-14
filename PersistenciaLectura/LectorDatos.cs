using System;
using System.Collections.Generic;
using System.IO;
using Cinema_Doomsday.Modelos;


namespace Cinema_Doomsday.PersistenciaLectura
{
    public static class LectorDatos
    {
        public static void Leer(
            BinaryReader reader,
            ContextoDatos datos)
        {
            Usuarios(reader, datos);
            Peliculas(reader, datos);
            Salas(reader, datos);
            Funciones(reader, datos);
            Asientos(reader, datos);
            Clientes(reader, datos);
            Reservas(reader, datos);
        }

        static void Usuarios(
            BinaryReader r,
            ContextoDatos d)
        {
            int cantidad = ValidadorLectura.LeerCantidad(r, "usuarios");

            for (int i = 0; i < cantidad; i++)
            {
                string id =
                    r.ReadString();

                bool activo =
                    r.ReadBoolean();

                Usuario x =
                    new Usuario(
                        id,
                        r.ReadString(),
                        r.ReadString(),
                        r.ReadString());

                if (!activo)
                    x.Desactivar();

                d.Usuarios.Add(x);
            }
        }

        static void Peliculas(
            BinaryReader r,
            ContextoDatos d)
        {
            int cantidad = ValidadorLectura.LeerCantidad(r, "peliculas");

            for (int i = 0; i < cantidad; i++)
            

            {
                string id =
                    r.ReadString();

                bool activo =
                    r.ReadBoolean();

                Pelicula x =
                    new Pelicula(
                        id,
                        r.ReadString(),
                        r.ReadInt32(),
                        r.ReadString(),
                        r.ReadString());

                if (!activo)
                    x.Desactivar();

                d.Peliculas.Add(x);
            }
        }

        static void Salas(
            BinaryReader r,
            ContextoDatos d)
        {
            int cantidad = ValidadorLectura.LeerCantidad(r, "salas");

            for (int i = 0; i < cantidad; i++)
            
            {
                string id =
                    r.ReadString();

                bool activo =
                    r.ReadBoolean();

                Sala x =
                    new Sala(
                        id,
                        r.ReadInt32(),
                        r.ReadInt32(),
                        r.ReadString());

                if (!activo)
                    x.Desactivar();

                d.Salas.Add(x);
            }
        }

        static void Funciones(
            BinaryReader r,
            ContextoDatos d)
        {
            int cantidad = ValidadorLectura.LeerCantidad(r, "funciones");
            for (int i = 0; i < cantidad; i++)
            
            {
                string id =
                    r.ReadString();

                bool activo =
                    r.ReadBoolean();

                Funcion x =
                    new Funcion(
                        id,
                        r.ReadString(),
                        r.ReadString(),
                        new DateTime(
                            r.ReadInt64()),
                        r.ReadDecimal());

                if (!activo)
                    x.Desactivar();

                d.Funciones.Add(x);
            }
        }

        static void Asientos(
            BinaryReader r,
            ContextoDatos d)
        {
            int cantidad = ValidadorLectura.LeerCantidad(r, "asientos");

            for (int i = 0; i < cantidad; i++)
            
            {
                Asiento x =
                    new Asiento(
                        r.ReadString(),
                        r.ReadString(),
                        r.ReadString(),
                        r.ReadInt32());

                x.RestaurarEstado(
                    r.ReadBoolean());

                d.Asientos.Add(x);
            }
        }

        static void Clientes(
           BinaryReader r,
           ContextoDatos d)
        {
            int cantidad =
                ValidadorLectura.LeerCantidad(r,"clientes");

            for (int i = 0; i < cantidad; i++)
            {
                string id = r.ReadString();
                bool activo = r.ReadBoolean();

                Cliente x = new Cliente(
                    id,
                    r.ReadString(),
                    r.ReadString(),
                    r.ReadInt32(),
                    r.ReadString(),
                    r.ReadString(),
                    new DateTime(r.ReadInt64()));

                if (!activo)
                    x.Desactivar();

                d.Clientes.Add(x);
            }
        }

        static void Reservas(
            BinaryReader r,
            ContextoDatos d)
        {
            int cantidad = ValidadorLectura.LeerCantidad(r, "reservas");

            for (int i = 0; i < cantidad; i++)
            
             {
                string id =
                    r.ReadString();

                bool activo =
                    r.ReadBoolean();

                string funcion =
                    r.ReadString();

                string cliente =
                    r.ReadString();

                decimal total =
                    r.ReadDecimal();

                string metodo =
                    r.ReadString();

                string detalle =
                    r.ReadString();

                decimal cambio =
                    r.ReadDecimal();

                DateTime fecha =
                    new DateTime(
                        r.ReadInt64());

                int cantidadAsientos =
                    r.ReadInt32();

                List<string> asientos =
                    new List<string>();

                for (
                    int j = 0;
                    j < cantidadAsientos;
                    j++)

                {
                    asientos.Add(
                        r.ReadString());
                }

                Reserva x =
                    new Reserva(
                        id,
                        funcion,
                        cliente,
                        asientos,
                        total,
                        metodo,
                        detalle,
                        cambio);

                x.RestaurarFecha(
                    fecha);

                if (!activo)
                    x.Desactivar();

                d.Reservas.Add(x);
            }
        }

        
    }
}
