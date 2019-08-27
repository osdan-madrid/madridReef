using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using madridReef.Services;
using madridReef.Models;
using madridReef.ViewModels;

namespace madridReef.Views.Catalogos.Proveedores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Consulta : ContentPage
    {
        ProveedoresHelper firebaseHelper = new ProveedoresHelper();
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Consulta()
        {
            try
            {
                InitializeComponent();
            } 
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al actualizar el Consulta : Consulta");
                DisplayAlert("Error ", ex.Message, "OK");
            }

}

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allProveedores = await firebaseHelper.GetAllProveedores();
            ItemsListView.ItemsSource = allProveedores;
        }


        async void AddProveedor_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Nuevo()));
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            try
            {
                var item = args.SelectedItem as Proveedor;
                if (item == null)
                    return;

                //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
                await Navigation.PushAsync(new Editar(new ViewModels.ProveedorDetailViewModel(item)));

                // Manually deselect item.
                ItemsListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al actualizar el OnItemSelected : OnItemSelected");
                await DisplayAlert("Error ", ex.Message, "OK");
            }
        }
    }
}