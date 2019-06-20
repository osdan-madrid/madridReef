using System;
using System.Collections.Generic;
using System.Text;

namespace madridReef.Models
{
    public class TipoProducto
    {
        /// <summary>
        /// Identificador único del tipo de producto
        /// </summary>
        public string TipoProductoID { get; set; }

        /// <summary>
        /// Nombre del tipo de Producto
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Fecha de modificación del registro
        /// </summary>
        public DateTime? FechaModificacion { get; set; }

    }
}
