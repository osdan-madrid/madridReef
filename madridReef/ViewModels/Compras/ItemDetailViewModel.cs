
using System;

using madridReef.Models;

namespace madridReef.ViewModels.Compras
{
    public class CompraDetailViewModel : BaseViewModel
    {
        public Compra compra { get; set; }
        public CompraDetailViewModel(Compra item = null)
        {
            Title = "Nueva Compra"; //item?.Comp;
            compra = item;
        }
    }
}
