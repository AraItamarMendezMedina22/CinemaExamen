using System;
using System.IO;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.PersistenciaEscritura;
using Cinema_Doomsday.PersistenciaLectura;
using Cinema_Doomsday.Utilidades;

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

        static readonly string RutaTemporal =
            Path.Combine(
                Carpeta,
                "cinema_datos.tmp");

        const string Firma =
            "DOOMSDAY";

        const byte Version = 1;

        public static ContextoDatos Cargar()
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

            try
            {
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

                    if (fs.Position != fs.Length)
                    {
                        throw new InvalidDataException(
                            "El archivo contiene datos adicionales.");
                    }
                }

                return datos;
            }
              catch (
                Exception ex)
                when (
                    ex is IOException ||
                    ex is InvalidDataException ||
                    ex is EndOfStreamException ||
                    ex is ArgumentException ||
                    ex is OverflowException)
            {
                RespaldarCorrupto();

                throw new InvalidDataException(
                    "El archivo de datos está dañado o no es compatible.",
                    ex);
            }
        }

        public static void Guardar(
            ContextoDatos datos)
        {
            if (datos == null)
            {
                throw new ArgumentNullException(
                    "datos");
            }

            Directory.CreateDirectory(
                Carpeta);

            try
            {
                if (File.Exists(RutaTemporal))
                    File.Delete(RutaTemporal);

                using (
                    FileStream fs =
                        new FileStream(
                            RutaTemporal,
                            FileMode.CreateNew,
                            FileAccess.Write,
                            FileShare.None,
                            4096,
                            FileOptions.WriteThrough))
                using (
                    BinaryWriter writer =
                        new BinaryWriter(fs))
                {
                    writer.Write(Firma);
                    writer.Write(Version);

                    EscritorDatos.Escribir(
                        writer,
                        datos);

                    writer.Flush();
                    fs.Flush(true);
                }

                ReemplazarArchivo();
            }
            catch (
                Exception ex)
                when (
                    ex is IOException ||
                    ex is UnauthorizedAccessException ||
                    ex is InvalidDataException)
            {
                EliminarArchivoTemporal();

                throw new IOException(
                    "No se pudo guardar el archivo de datos.",
                    ex);
            }
        }

        static void ReemplazarArchivo()
        {
            if (!File.Exists(Ruta))
            {
                File.Move(
                    RutaTemporal,
                    Ruta);

                return;
            }

            string respaldo =
                Path.Combine(
                    Carpeta,
                    "cinema_datos_anterior_" +
                    DateTime.Now.ToString(
                        "yyyyMMdd_HHmmss") +
                    ".dat");

            File.Replace(
                RutaTemporal,
                Ruta,
                respaldo,
                true);
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
                    "La firma del archivo es inválida.");
            }

            if (version != Version)
            {
                throw new InvalidDataException(
                    "La versión del archivo no es compatible.");
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

        static void EliminarArchivoTemporal()
        {
            try
            {
                if (File.Exists(RutaTemporal))
                    File.Delete(RutaTemporal);
            }
            catch
            {
                // No ocultar la excepción original del guardado.
            }
        }
    }
}
