using System;
using System.Collections.Generic;
using System.Text;

using madridReef.Models;

namespace madridReef.ViewModels.TipoProductos
{
    public class TipoProductoDetailViewModel : BaseViewModel
    {
        public TipoProducto _tipoProducto { get; set; }
        public TipoProductoDetailViewModel(TipoProducto tipoProducto = null)
        {
            Title = tipoProducto?.Nombre;
            _tipoProducto = tipoProducto;
        }
    }
}
