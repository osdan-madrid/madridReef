using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using madridReef.Services;
using madridReef.Models;

namespace madridReef.Views.Proveedores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class newProveedoresDetails : ContentPage
    {
        ProveedoresHelper firebaseHelper = new ProveedoresHelper();
        public newProveedoresDetails()
        {
            InitializeComponent();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            Proveedor _nuevoProveedor = new Proveedor();
            _nuevoProveedor.NombreEmpresa = txtEmpresa.Text;
            _nuevoProveedor.NombreCompleto = txtNombre.Text;
            _nuevoProveedor.NoCelular = txtCelular.Text;
            _nuevoProveedor.FacebookURLProfile = txtFacebook.Text;
            _nuevoProveedor.FechaRegistro = Convert.ToDateTime(txtFechaRegistro.Text);
            _nuevoProveedor.FechaModificacion = Convert.ToDateTime(txtFechaModificacion.Text);
            await firebaseHelper.AddProveedor(_nuevoProveedor);
            txtEmpresa.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtFacebook.Text = string.Empty;
            txtFechaModificacion.Text = string.Empty;
            txtFechaRegistro.Text = string.Empty;
            await DisplayAlert("Exitoso", "Proveedor Agregado Exitosamente", "OK");
            //var allPersons = await firebaseHelper.GetAllProveedores();
            //list.ItemsSource = allPersons;
        }
    }
}