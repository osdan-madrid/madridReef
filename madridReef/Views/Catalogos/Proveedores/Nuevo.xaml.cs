using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using madridReef.Services;
using madridReef.Models;

namespace madridReef.Views.Catalogos.Proveedores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Nuevo : ContentPage
    {
        ProveedoresHelper firebaseHelper = new ProveedoresHelper();
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Nuevo()
        {
             InitializeComponent();
            ResetearControles();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            try
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

                ResetearControles();
                txtEmpresa.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar el Proveedor : BtnAdd_Clicked");
                await DisplayAlert("Error ", ex.Message, "OK");
            }
        }

        private async void ResetearControles()
        {
            try
            {
                txtCelular.Text = string.Empty;
                txtEmpresa.Text = string.Empty;
                txtFacebook.Text = string.Empty;
                txtFechaModificacion.Text = string.Empty;
                txtFechaRegistro.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtNombre.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}