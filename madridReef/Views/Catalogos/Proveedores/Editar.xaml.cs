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
    public partial class Editar : ContentPage
    {
        ProveedorDetailViewModel viewModel;
        ProveedoresHelper firebaseHelper = new ProveedoresHelper();
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Editar(ProveedorDetailViewModel viewModel)
        {
            try
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
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al actualizar el Proveedor : BtnActualizar_Clicked");
                DisplayAlert("Error ", ex.Message, "OK");
            }
        }

        public Editar()
        {
            try
            {
                InitializeComponent();

                var proveedor = new Proveedor
                {
                    NombreCompleto = "nombrecompleto",
                    NombreEmpresa = "nombreEmpresa"
                };

                viewModel = new ProveedorDetailViewModel(proveedor);
                BindingContext = proveedor;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al actualizar el Proveedor : BtnActualizar_Clicked");
                DisplayAlert("Error ", ex.Message, "OK");
            }
        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Proveedor _nuevoProveedor = new Proveedor();
                _nuevoProveedor.NombreEmpresa = txtEmpresa.Text;
                _nuevoProveedor.NombreCompleto = txtNombre.Text;
                _nuevoProveedor.NoCelular = txtCelular.Text;
                _nuevoProveedor.FacebookURLProfile = txtFacebook.Text;
                _nuevoProveedor.ProveedorID = txtID.Text;
                _nuevoProveedor.FechaRegistro = Convert.ToDateTime(txtFechaRegistro.Text.ToString());
                await firebaseHelper.UpdateProveedor(_nuevoProveedor);
                ResetearControles();
                await DisplayAlert("Exitoso", "Proveedor Actualizado Exitosamente", "OK");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al actualizar el Proveedor : BtnActualizar_Clicked");
                await DisplayAlert("Error ", ex.Message, "OK");
            }
        }

        private async void ResetearControles()
        {
            try
            {
                txtEmpresa.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtCelular.Text = string.Empty;
                txtFacebook.Text = string.Empty;
                txtFechaModificacion.Text = string.Empty;
                txtFechaRegistro.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}