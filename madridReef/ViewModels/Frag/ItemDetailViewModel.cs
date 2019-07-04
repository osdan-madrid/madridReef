
using System;

using madridReef.Models;


namespace madridReef.ViewModels.Frag
{
    public class FragDetailViewModel : BaseViewModel
    {
        public madridReef.Models.Frag frag { get; set; }
        public FragDetailViewModel(madridReef.Models.Frag item = null)
        {
            Title = "Nuevo Frag"; //item?.Comp;
            frag = item;
        }
    }
}

