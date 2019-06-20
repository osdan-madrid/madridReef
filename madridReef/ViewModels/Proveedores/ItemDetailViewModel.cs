using System;

using madridReef.Models;

namespace madridReef.ViewModels
{
    public class ProveedorDetailViewModel : BaseViewModel
    {
        public Proveedor Proveedor { get; set; }
        public ProveedorDetailViewModel(Proveedor proveedor = null)
        {
            Title = proveedor?.NombreEmpresa;
            Proveedor = proveedor;
        }
    }
}
