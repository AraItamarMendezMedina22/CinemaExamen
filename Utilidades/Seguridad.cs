using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Cinema_Doomsday.Utilidades
{
    public static class Seguridad
    {
        public static string CrearHash(
            string texto)
        {
            using (
                SHA256 sha =
                    SHA256.Create())
            {
                byte[] datos =
                    Encoding.UTF8.GetBytes(
                        texto);

                byte[] hash =
                    sha.ComputeHash(
                        datos);

                StringBuilder resultado =
                    new StringBuilder();

                foreach (
                    byte valor
                    in hash)
                {
                    resultado.Append(
                        valor.ToString("x2"));
                }

                return resultado.ToString();
            }
        }
    }
}
