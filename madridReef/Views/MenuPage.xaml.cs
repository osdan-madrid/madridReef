using madridReef.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        // Nuevo
        private ObservableCollection<MenuGroupItem> _allGroups;
        private ObservableCollection<MenuGroupItem> _expandedGroups;
        //


        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            //menuItems = new List<HomeMenuItem>
            //{
            //    new HomeMenuItem {Id = MenuItemType.Compras, Title="Compras" , Icon="\uf07a", FaIconFile = "fa-solid-900.ttf#Font Awesome 5 Free Solid" },
            //    new HomeMenuItem {Id = MenuItemType.Gastos, Title="Gastos" , Icon="\uf51e", FaIconFile = "fa-solid-900.ttf#Font Awesome 5 Free Solid" },
            //    new HomeMenuItem {Id = MenuItemType.Proveedores, Title="Proveedores" , Icon="\uf275", FaIconFile = "fa-solid-900.ttf#Font Awesome 5 Free Solid" },
            //    new HomeMenuItem {Id = MenuItemType.TipoProducto, Title="Tipo Productos" , Icon="\uf4ce", FaIconFile = "fa-solid-900.ttf#Font Awesome 5 Free Solid" },
            //    new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
            //    new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            //};

            //ListViewMenu.ItemsSource = menuItems;

            //ListViewMenu.SelectedItem = menuItems[0];
            //ListViewMenu.ItemSelected += async (sender, e) =>
            //{
            //    if (e.SelectedItem == null)
            //        return;

            //    var id = (int)((HomeMenuItem)e.SelectedItem).Id;
            //    await RootPage.NavigateFromMenu(id);
            //};


            //Nuevo
            _allGroups = MenuGroupItem.All;
            UpdateListContent();

            GroupedView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((madridReef.Models.MenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };

            //
        }

        private void HeaderTapped(object sender, EventArgs args)
        {
            int selectedIndex = _expandedGroups.IndexOf(
                ((MenuGroupItem)((Button)sender).CommandParameter));
            _allGroups[selectedIndex].Expanded = !_allGroups[selectedIndex].Expanded;
            UpdateListContent();
        }

        private void UpdateListContent()
        {
            _expandedGroups = new ObservableCollection<MenuGroupItem>();
            foreach (MenuGroupItem group in _allGroups)
            {
                //Create new FoodGroups so we do not alter original list
                MenuGroupItem newGroup = new MenuGroupItem(group.Title, group.ShortName, group.Expanded);
                //Add the count of food items for Lits Header Titles to use
                newGroup.MenuItemCount = group.Count;
                if (group.Expanded)
                {
                    foreach (madridReef.Models.MenuItem elemento in group)
                    {
                        newGroup.Add(elemento);
                    }
                }
                _expandedGroups.Add(newGroup);
            }
            GroupedView.ItemsSource = _expandedGroups;
        }
    }
}