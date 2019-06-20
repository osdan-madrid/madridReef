using System;

using madridReef.Models;

namespace madridReef.ViewModels.Gastos
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Gasto gasto { get; set; }
        public ItemDetailViewModel(Gasto item = null)
        {
            Title = item?.Nombre;
            gasto = item;
        }
    }
}
