using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using madridReef.Services;
using madridReef.Models;
using madridReef.ViewModels.TipoProductos;

namespace madridReef.Views.TipoProductos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TipoProductoDetails : ContentPage
    {
        TipoProductoDetailViewModel viewModel;
        TipoProductosHelper firebaseHelper = new TipoProductosHelper();
        public TipoProductoDetails( TipoProductoDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;

            txtNombre.Text = viewModel._tipoProducto.Nombre;
            txtFechaRegistro.Text = viewModel._tipoProducto.FechaRegistro.ToString();
            txtFechaModificacion.Text = viewModel._tipoProducto.FechaModificacion.ToString();
            txtID.Text = viewModel._tipoProducto.TipoProductoID;
        }

        public TipoProductoDetails()
        {
            InitializeComponent();

            var tipo = new TipoProducto
            {
                //Text = "Item 1",
                //Description = "This is an item description."
                Nombre = "nombrecompleto"
                
            };

            viewModel = new TipoProductoDetailViewModel(tipo);
            BindingContext = tipo;
        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            TipoProducto _actualiza = new TipoProducto();
            _actualiza.Nombre = txtNombre.Text;
            _actualiza.TipoProductoID = txtID.Text;
            _actualiza.FechaRegistro = Convert.ToDateTime(txtFechaRegistro.Text.ToString());
            await firebaseHelper.UpdateTipoProducto(_actualiza);

            txtNombre.Text = string.Empty;
            txtFechaModificacion.Text = string.Empty;
            txtFechaRegistro.Text = string.Empty;
            await DisplayAlert("Exitoso", "Tipo Producto Actualizado Exitosamente", "OK");
            //var allPersons = await firebaseHelper.GetAllProveedores();
            //list.ItemsSource = allPersons;
        }
    }
}