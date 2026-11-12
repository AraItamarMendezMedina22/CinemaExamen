using System;
using System.Collections.Generic;
using System.Linq;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Peliculas;
using Cinema_Doomsday.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Menus
{
    public static class MenuPeliculas
    {
        public static void Mostrar(
            ContextoDatos datos,
            Usuario usuario)
        {
     

      ConsolaUI.Pausa();
      ListarPeliculas.Ejecutar(
             datos);
      bool volver = false;
      List<string> opciones =
                   new List<string>
                   {
                        "Agregar",
                        "Editar",
                        "Buscar",
                        "Actualizar duración",
                        "Activar / Inactivar",
                        "Listar"
                   };

      while (!volver)
            {
                ConsolaUI.Encabezado(
                    "GESTIÓN DE PELÍCULAS");

                int opcion =
                    ConsolaUI.LeerMenu(
                        opciones,
                        true);

                switch (opcion)
                {
                    case 1:
                        AgregarPelicula.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 2:
                        EditarPelicula.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 3:
                        BuscarPelicula.Ejecutar(
                            datos);
                        break;

                    case 4:
                        ActualizarPelicula.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 5:
                        EstadoPelicula.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 0:
                        volver = true;
                        continue;
                }

                ConsolaUI.Pausa();
            }
        }
    }
}
