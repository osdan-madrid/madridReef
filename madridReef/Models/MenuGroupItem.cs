using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace madridReef.Models
{
    public class MenuGroupItem : ObservableCollection<MenuItem>, INotifyPropertyChanged
    {

        private bool _expanded;

        public string Title { get; set; }

        public string TitleWithItemCount
        {
            get { return string.Format("{0} ({1})", Title, MenuItemCount); }
        }

        public string ShortName { get; set; }

        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged("Expanded");
                    OnPropertyChanged("StateIcon");
                }
            }
        }

        public string StateIcon
        {
            get { return Expanded ? "expanded_blue.png" : "collapsed_blue.png"; }
        }

        public int MenuItemCount { get; set; }

        public MenuGroupItem(string title, string shortName, bool expanded = false)
        {
            Title = title;
            ShortName = shortName;
            Expanded = expanded;
        }

        public static ObservableCollection<MenuGroupItem> All { private set; get; }

        static MenuGroupItem()
        {
            ObservableCollection<MenuGroupItem> Groups = new ObservableCollection<MenuGroupItem>{
                new MenuGroupItem("Compras","C"){
                    new MenuItem { Nombre = "Nueva", Icon="CarritoCompra.png" , Description ="Registrar una nueva compra", Id=MenuItem_Type.Compras },
                },
                new MenuGroupItem("Catalogos","CAT"){
                    new MenuItem { Nombre= "Frags",  Icon="Sol.png", Id=MenuItem_Type.Frag, Description="Dar de alta un nuevo frag" },
                    new MenuItem { Nombre= "Gastos",  Icon="Money.png", Id=MenuItem_Type.Gastos },
                    new MenuItem { Nombre="Proveedores", Icon="Proveedor.png" , Id=MenuItem_Type.Proveedores},
                    new MenuItem { Nombre="Tipo Productos", Icon="Cajas.png",  Id=MenuItem_Type.TipoProducto },
                },
                new MenuGroupItem("Frags","F"){
                     new MenuItem { Nombre= "Nuevo",  Icon="CarritoCompra.png" },
                } };
            All = Groups;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}