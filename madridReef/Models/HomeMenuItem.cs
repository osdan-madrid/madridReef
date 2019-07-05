using System;
using System.Collections.Generic;
using System.Text;

namespace madridReef.Models
{
    public enum MenuItemType
    {
        Browse,
        Compras,
        Gastos,
        Proveedores,
        TipoProducto,
        Frag
    }
    public class HomeMenuItem
    {
        /// <summary>
        /// Identificador del elemento del menú.
        /// </summary>
        public MenuItemType Id { get; set; }

        /// <summary>
        /// Propiedad que almacenará el título del elemento del menú.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Propiedad que contendrá la ruta del ícono a utilizar.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Propiedad para almacenar el nombre del archivo que contendrá el icon
        /// </summary>
        public string FaIconFile { get; set; }
    }
}
