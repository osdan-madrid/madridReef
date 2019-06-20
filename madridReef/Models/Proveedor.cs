using System;
using System.Collections.Generic;
using System.Text;

namespace madridReef.Models
{
    public class Proveedor
    {
        /// <summary>
        /// Identificador único del proveedor
        /// </summary>
        public string ProveedorID { get; set; }

        /// <summary>
        /// Nombre del proveedor
        /// </summary>
        public string NombreEmpresa { get; set; }

        /// <summary>
        /// Nombre del contacto
        /// </summary>
        public string NombreCompleto { get; set; }

        /// <summary>
        /// Número de Celular del Proveedor
        /// </summary>
        public string NoCelular { get; set; }

        /// <summary>
        /// URL del perfil de Facebook
        /// </summary>
        public string FacebookURLProfile { get; set; }

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
