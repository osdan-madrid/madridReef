﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using madridReef.Services;
using madridReef.Models;
using madridReef.ViewModels;

namespace madridReef.Views.Proveedores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProveedoresList : ContentPage
    {
        ProveedoresHelper firebaseHelper = new ProveedoresHelper();
        public ProveedoresList()
        {
            InitializeComponent();

   
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allProveedores = await firebaseHelper.GetAllProveedores();
            ItemsListView.ItemsSource = allProveedores;
        }


        async void AddProveedor_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new newProveedoresDetails()));
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Proveedor;
            if (item == null)
                return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
            await Navigation.PushAsync(new ProveedoresDetails(new ViewModels.ProveedorDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }
}