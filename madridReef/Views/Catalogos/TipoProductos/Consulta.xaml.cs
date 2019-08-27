using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using madridReef.Services;
using madridReef.Models;
using madridReef.ViewModels;

namespace madridReef.Views.Catalogos.TipoProductos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Consulta : ContentPage
    {
        TipoProductosHelper firebaseHelper = new TipoProductosHelper();
        public ObservableCollection<string> Items { get; set; }

        public Consulta()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var all = await firebaseHelper.GetAllTipoProductos();
            ItemsListView.ItemsSource = all;
        }

        async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Nuevo()));
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as TipoProducto;
            if (item == null)
                return;

            
            await Navigation.PushAsync(new Editar(new ViewModels.TipoProductos.TipoProductoDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }
}
