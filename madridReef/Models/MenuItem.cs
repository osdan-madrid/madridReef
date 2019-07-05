using System;
using System.Collections.Generic;
using System.Text;

namespace madridReef.Models
{
    public enum MenuItem_Type
    {
        Browse,
        Compras,
        Gastos,
        Proveedores,
        TipoProducto,
        Frag
    }

    public class MenuItem
    {
        /// <summary>
        /// Identificador del elemento del menú.
        /// </summary>
        public MenuItem_Type Id { get; set; }

        /// <summary>
        /// Nombre de la opción del menú
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Icono que se mostrará
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// FA Icon File
        /// </summary>
        public string FaIconFile = "fa-solid-900.ttf#Font Awesome 5 Free Solid";

        public string Description { get; set; }
    }
}
