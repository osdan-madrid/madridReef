using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using madridReef.Services;
using madridReef.Models;

namespace madridReef.Views.TipoProductos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class newTipoProducto : ContentPage
    {
        TipoProductosHelper firebaseHelper = new TipoProductosHelper();
        public newTipoProducto()
        {
            InitializeComponent();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            TipoProducto _nuevo = new TipoProducto();
            _nuevo.Nombre = txtNombre.Text;
            _nuevo.FechaRegistro = Convert.ToDateTime(txtFechaRegistro.Text);
            _nuevo.FechaModificacion = Convert.ToDateTime(txtFechaModificacion.Text);
            await firebaseHelper.AddTipoProducto(_nuevo);
            txtNombre.Text = string.Empty;
            txtFechaModificacion.Text = string.Empty;
            txtFechaRegistro.Text = string.Empty;
            await DisplayAlert("Exitoso", "Tipo Producto Agregado Exitosamente", "OK");
            //var allPersons = await firebaseHelper.GetAllProveedores();
            //list.ItemsSource = allPersons;
        }
    }
}