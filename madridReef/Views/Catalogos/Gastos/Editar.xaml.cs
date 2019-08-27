using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using madridReef.Services;
using madridReef.Models;
using madridReef.ViewModels.CatalogoGastos;



namespace madridReef.Views.Catalogos.Gastos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Editar : ContentPage
    {
        ItemDetailViewModel viewModel;
        CatalogoGastosHelper firebaseHelper = new CatalogoGastosHelper();
        public Editar(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
            txtMonto.Text = viewModel.gasto.Monto.ToString();
            txtNombre.Text = viewModel.gasto.Nombre;
            txtDescripcion.Text = viewModel.gasto.Descripcion;
            txtFechaRegistro.Text = viewModel.gasto.FechaRegistro.ToString();
            txtFechaModificacion.Text = viewModel.gasto.FechaModificacion.ToString();
            txtID.Text = viewModel.gasto.GastoId;
        }

        public Editar()
        {
            InitializeComponent();
            ResetearControles();
            var item = new CatalogoGasto
            {
                Nombre = "Nombre",
                Descripcion = "Descripcion"
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = item;
        }

        /// <summary>
        /// Evento Click del botón BtnActualizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                CatalogoGasto _item = new CatalogoGasto();
                _item.Descripcion = txtDescripcion.Text;
                _item.Nombre = txtNombre.Text;
                _item.Monto = Convert.ToDecimal(txtMonto.Text);
                _item.GastoId = txtID.Text;
                _item.FechaRegistro = Convert.ToDateTime(txtFechaRegistro.Text.ToString());
                await firebaseHelper.Update(_item);
                ResetearControles();
                await DisplayAlert("Exitoso", "Gasto Actualizado Exitosamente", "OK");
                await Navigation.PushModalAsync(new NavigationPage(new Editar()));
            }
            catch (Exception ex)
            {
                
                
                // handle your exception here! 
            }
        }

        /// <summary>
        /// Método utilizado para resetear los controles a su estado original.
        /// </summary>
        private void ResetearControles()
        {
            txtDescripcion.Text = string.Empty;
            txtFechaModificacion.Text = string.Empty;
            txtFechaRegistro.Text = string.Empty;
            txtID.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtMonto.Text = string.Empty;
        }

        /// <summary>
        /// Evento Click del botón Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            bool respuesta = await DisplayAlert("¿Eliminar?", "¿Está seguro de borrar el registro?", "Aceptar", "Cancelar");
           if(respuesta)
            {
                CatalogoGasto _item = new CatalogoGasto();
                _item.GastoId = txtID.Text;
                await firebaseHelper.Delete(_item);
                ResetearControles();
                await DisplayAlert("Exitoso", "Gasto Eliminado Exitosamente", "OK");
                await Navigation.PushModalAsync(new NavigationPage(new Editar()));
            }


        }
    }
}

