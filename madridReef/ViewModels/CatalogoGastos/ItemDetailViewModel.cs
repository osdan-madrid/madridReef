using System;

using madridReef.Models;

namespace madridReef.ViewModels.CatalogoGastos
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public CatalogoGasto gasto { get; set; }
        public ItemDetailViewModel(CatalogoGasto item = null)
        {
            Title = item?.Nombre;
            gasto = item;
        }
    }
}
