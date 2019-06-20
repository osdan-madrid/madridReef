using System;

namespace madridReef.Models
{
    public class Gasto
    {
        /// <summary>
        /// Identificador único del gasto
        /// </summary>
        public string GastoId { get; set; }

        /// <summary>
        /// Nombre del gasto
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción del gasto
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Monto del gasto
        /// </summary>
        public decimal Monto { get; set; }

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
