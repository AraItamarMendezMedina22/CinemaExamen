using Cinema_Doomsday.Funciones;
using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Peliculas;
using Cinema_Doomsday.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Menus
{
    public static class MenuFunciones
    {
        public static void Mostrar(
            ContextoDatos datos,
            Usuario usuario)
        {
    

      ConsolaUI.Pausa();
      bool volver = false;
      ListarFunciones.Ejecutar(
   datos);
      List<string> opciones =
          new List<string>
          {
                        "Agregar",
                        "Editar",
                        "Buscar",
                        "Listar",
                        "Actualizar precio",
                        "Activar / Inactivar"
          };
      while (!volver)
            {
                ConsolaUI.Encabezado(
                    "FUNCIONES / CARTELERA");


                int opcion =
                    ConsolaUI.LeerMenu(
                        opciones,
                        true);

                switch (opcion)
                {
                    case 1:
                        AgregarFuncion.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 2:
                        EditarFuncion.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 3:
                        BuscarFuncion.Ejecutar(datos);
                        break;

                    case 4:
                        ListarFunciones.Ejecutar(datos);
                        break;

                    case 5:
                        ActualizarFuncion.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 6:
                        EstadoFuncion.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 0:
                        volver = true;
                        break;
                }

                ConsolaUI.Pausa();
            }
        }
    }
}
