using System;
using System.Collections.Generic;

namespace madridReef.Models
{
    public  class Compra
    {
        /// <summary>
        /// Identificador único de la compra
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Descripción de la compra
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Tipo de producto al que se refiere la compra
        /// </summary>
        public TipoProducto tipoProducto { get; set; }

        /// <summary>
        /// Proveedor al que se le compró
        /// </summary>
        public Proveedor proveedor { get; set; }

        /// <summary>
        /// Fecha en que se realizó la compra
        /// </summary>
        public DateTime FechaCompra { get; set; }

        /// <summary>
        /// Precio total de la compra
        /// </summary>
        public decimal PrecioTotalCompra { get; set; }

        /// <summary>
        /// Lista de gastos relacionados a la compra
        /// </summary>
        public List<Gasto> Gastos { get; set; }

        /// <summary>
        /// Cantidad de pólipos ó unidades.
        /// </summary>
        public int CantidadUnidades { get; set; }

        /// <summary>
        /// URL de la imagen almacenada en la nube
        /// </summary>
        public string Imagen { get; set; }

        /// <summary>
        /// Fecha de Registro
        /// </summary>
        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Fecha de Modificación
        /// </summary>
        public DateTime FechaModificacion { get; set; }
    }
}
