using System;
using System.Collections.Generic;
using System.IO;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.PersistenciaEscritura;
using Cinema_Doomsday.PersistenciaLectura;
using Cinema_Doomsday.Utilidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Archivos
{
    public static class ArchivoDatos
    {
        static readonly string Carpeta =
            Path.Combine(
                AppDomain.CurrentDomain
                    .BaseDirectory,
                "Data");

        static readonly string Ruta =
            Path.Combine(
                Carpeta,
                "cinema_datos.dat");

        const string Firma =
            "DOOMSDAY";

        const byte Version = 1;

        public static ContextoDatos Cargar()
        {
            try
            {
                Directory.CreateDirectory(
                    Carpeta);

                if (!File.Exists(Ruta))
                {
                    ContextoDatos nuevo =
                        new ContextoDatos();

                    Guardar(nuevo);

                    return nuevo;
                }

                ContextoDatos datos =
                    new ContextoDatos();

                using (
                    FileStream fs =
                        new FileStream(
                            Ruta,
                            FileMode.Open,
                            FileAccess.Read,
                            FileShare.Read))
                using (
                    BinaryReader reader =
                        new BinaryReader(fs))
                {
                    ValidarCabecera(
                        reader);

                    LectorDatos.Leer(
                        reader,
                        datos);
                }

                return datos;
            }
            catch (Exception ex)
            {
                ConsolaUI.Error(
                    "No se pudo leer el archivo: " +
                    ex.Message);

                RespaldarCorrupto();

                return new ContextoDatos();
            }
        }

        public static void Guardar(
            ContextoDatos datos)
        {
            try
            {
                Directory.CreateDirectory(
                    Carpeta);

                using (
                    FileStream fs =
                        new FileStream(
                            Ruta,
                            FileMode.Create,
                            FileAccess.Write,
                            FileShare.None))
                using (
                    BinaryWriter writer =
                        new BinaryWriter(fs))
                {
                    writer.Write(Firma);
                    writer.Write(Version);

                    EscritorDatos.Escribir(
                        writer,
                        datos);
                }
            }
            catch (IOException ex)
            {
                throw new IOException(
                    "No se pudo guardar el archivo de datos. " +
                    ex.Message);
            }
        }

        static void ValidarCabecera(
            BinaryReader reader)
        {
            string firma =
                reader.ReadString();

            byte version =
                reader.ReadByte();

            if (firma != Firma)
            {
                throw new InvalidDataException(
                    "Firma del archivo inválida.");
            }

            if (version != Version)
            {
                throw new InvalidDataException(
                    "Versión de archivo incompatible.");
            }
        }

        static void RespaldarCorrupto()
        {
            try
            {
                if (!File.Exists(Ruta))
                    return;

                string respaldo =
                    Path.Combine(
                        Carpeta,
                        "cinema_datos_corruptos_" +
                        DateTime.Now.ToString(
                            "yyyyMMdd_HHmmss") +
                        ".dat");

                File.Move(
                    Ruta,
                    respaldo);

                ConsolaUI.Info(
                    "El archivo dañado fue respaldado.");
            }
            catch
            {
                ConsolaUI.Error(
                    "No se pudo respaldar el archivo dañado.");
            }
        }
    }
}
