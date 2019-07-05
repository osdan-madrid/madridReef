using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using madridReef.Models;

namespace madridReef.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    //case (int)MenuItemType.About:
                    //    MenuPages.Add(id, new NavigationPage(new AboutPage()));
                    //    break;
                    case (int)MenuItemType.Proveedores:
                        MenuPages.Add(id, new NavigationPage(new Proveedores.ProveedoresList()));
                        break;
                    case (int)MenuItemType.TipoProducto:
                        MenuPages.Add(id, new NavigationPage(new TipoProductos.TipoProductosList()));
                        break;
                    case (int)MenuItemType.Gastos:
                        MenuPages.Add(id, new NavigationPage(new CatalogoGastos.GastosList()));
                        break;
                    case (int)MenuItemType.Compras:
                        MenuPages.Add(id, new NavigationPage(new Compras.NuevaCompra()));
                        break;
                    case (int)MenuItemType.Frag:
                        MenuPages.Add(id, new NavigationPage(new Frags.NuevoFrag()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}