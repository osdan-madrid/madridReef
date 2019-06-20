using madridReef.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace madridReef.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Gastos, Title="Gastos" , Icon="\uf51e", FaIconFile = "fa-solid-900.ttf#Font Awesome 5 Free Solid" },
                new HomeMenuItem {Id = MenuItemType.Proveedores, Title="Proveedores" , Icon="\uf275", FaIconFile = "fa-solid-900.ttf#Font Awesome 5 Free Solid" },
                new HomeMenuItem {Id = MenuItemType.TipoProducto, Title="Tipo Productos" , Icon="\uf4ce", FaIconFile = "fa-solid-900.ttf#Font Awesome 5 Free Solid" },
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}