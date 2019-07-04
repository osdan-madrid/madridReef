﻿using System;
using System.Collections.Generic;

namespace madridReef.Models
{
    public class Frag
    {
        /// <summary>
        /// Identificador único
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// Colonia Madre
        /// </summary>
        public Compra ColoniaMadre { get; set; }

        /// <summary>
        /// Valor del frag, incluyendo todos los gastos relacionados
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Precio sugerido de venta
        /// </summary>
        public decimal PrecioSugeridoVenta { get; set; }

        /// <summary>
        /// Lista de gastos relacionados a la elaboracion
        /// </summary>
        public List<CatalogoGasto> Gastos { get; set; }

        /// <summary>
        /// Cantidad de pólipos
        /// </summary>
        public int CantidadPolipos { get; set; }

        /// <summary>
        /// URL de la imagen almacenada en la nube
        /// </summary>
        public string ImagenURL { get; set; }

        /// <summary>
        /// Fecha de Registro
        /// </summary>
        public DateTime FechaElaboracion { get; set; }

        /// <summary>
        /// Fecha de Registro
        /// </summary>
        public DateTime FechaVenta { get; set; }

        /// <summary>
        /// Fecha de Registro
        /// </summary>
        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Fecha de Modificación
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// ¿Ya fué pagado?
        /// </summary>
        public bool Pagado { get; set; }
    }
}
