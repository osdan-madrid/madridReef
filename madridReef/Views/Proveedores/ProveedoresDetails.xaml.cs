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

namespace madridReef.Views.Proveedores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProveedoresDetails : ContentPage
    {
        ProveedorDetailViewModel viewModel;
        ProveedoresHelper firebaseHelper = new ProveedoresHelper();
        public ProveedoresDetails(ProveedorDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;

            txtNombre.Text = viewModel.Proveedor.NombreCompleto;
            txtEmpresa.Text = viewModel.Proveedor.NombreEmpresa;
            txtCelular.Text = viewModel.Proveedor.NoCelular;
            txtFacebook.Text = viewModel.Proveedor.FacebookURLProfile;
            txtFechaRegistro.Text = viewModel.Proveedor.FechaRegistro.ToString();
            txtFechaModificacion.Text = viewModel.Proveedor.FechaModificacion.ToString();
            txtID.Text = viewModel.Proveedor.ProveedorID;
        }

        public ProveedoresDetails()
        {
            InitializeComponent();

            var proveedor = new Proveedor
            {
                //Text = "Item 1",
                //Description = "This is an item description."
                NombreCompleto = "nombrecompleto",
                NombreEmpresa = "nombreEmpresa"
            };

            viewModel = new ProveedorDetailViewModel(proveedor);
            BindingContext = proveedor;
        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            Proveedor _nuevoProveedor = new Proveedor();
            _nuevoProveedor.NombreEmpresa = txtEmpresa.Text;
            _nuevoProveedor.NombreCompleto = txtNombre.Text;
            _nuevoProveedor.NoCelular = txtCelular.Text;
            _nuevoProveedor.FacebookURLProfile = txtFacebook.Text;
            _nuevoProveedor.ProveedorID = txtID.Text;
            _nuevoProveedor.FechaRegistro = Convert.ToDateTime( txtFechaRegistro.Text.ToString());
            await firebaseHelper.UpdateProveedor(_nuevoProveedor);
            txtEmpresa.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtFacebook.Text = string.Empty;
            txtFechaModificacion.Text = string.Empty;
            txtFechaRegistro.Text = string.Empty;
            await DisplayAlert("Exitoso", "Proveedor Actualizado Exitosamente", "OK");
            //var allPersons = await firebaseHelper.GetAllProveedores();
            //list.ItemsSource = allPersons;
        }
    }
}