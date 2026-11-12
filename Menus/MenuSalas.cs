using Cinema_Doomsday.Modelos;
using Cinema_Doomsday.Peliculas;
using Cinema_Doomsday.Salas;
using Cinema_Doomsday.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Doomsday.Menus
{
    public static class MenuSalas
    {
        public static void Mostrar(
            ContextoDatos datos,
            Usuario usuario)
        {
     

      ConsolaUI.Pausa();
      bool volver = false;
      ListarSalas.Ejecutar(
     datos);
      List<string> opciones =
          new List<string>
          {
                        "Agregar",
                        "Editar",
                        "Buscar",
                        "Listar",
                        "Actualizar capacidad",
                        "Activar / Inactivar"
          };
            while (!volver)
            {
                ConsolaUI.Encabezado(
                    "GESTIÓN DE SALAS");
;

                int opcion =
                    ConsolaUI.LeerMenu(
                        opciones,
                        true);

                switch (opcion)
                {
                    case 1:
                        AgregarSala.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 2:
                        EditarSala.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 3:
                        BuscarSala.Ejecutar(
                            datos);
                        break;

                    case 4:
                        ActualizarSala.Ejecutar(
                            datos,
                            usuario.NombreUsuario);
                        break;

                    case 5:
                        EstadoSala.Ejecutar(
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
