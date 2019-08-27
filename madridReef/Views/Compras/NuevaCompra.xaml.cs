using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using madridReef.Services;
using madridReef.Models;
using madridReef.ViewModels.Compras;
using Rg.Plugins.Popup.Services;


namespace madridReef.Views.Compras
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevaCompra : ContentPage
    {
        Compra compra = new Compra();
        CompraDetailViewModel _viewModel = new CompraDetailViewModel();
        ComprasHelper firebaseHelperCompras = new ComprasHelper();

        ProveedoresHelper firebaseHelper = new ProveedoresHelper();
        List<Proveedor> allProveedores;

        TipoProductosHelper firebaseHelperProductos = new TipoProductosHelper();
        List<TipoProducto> allTipoProductos;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public NuevaCompra()
        {
            try
            { 
                InitializeComponent();
                GenerarHandlers();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta.");
                DisplayAlert("Error ", ex.Message, "OK");
            }

        }
        public NuevaCompra(Compra compra = null)
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            try
            {
                txtDescripción.Focus();
                base.OnAppearing();

                compra.Gastos = new List<CatalogoGasto>();

                resetearControles();
                actualizarListaGastos();
                fillProveedores();
                fillTipoProductos();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta.");
                DisplayAlert("Error ", ex.Message, "OK");
            }
        }

        #region Handlers


        /// <summary>
        /// Evento SelectedIndexChanged del Combo de Proveedores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedValue = picker.Items[picker.SelectedIndex];

                Proveedor proveedor = allProveedores.Single(
                    delegate (Proveedor x)
                    {
                        return selectedValue.ToString() == x.NombreEmpresa;
                    }
                    );


                txtIdProveedor.Text = proveedor.ProveedorID;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta : Picker_SelectedIndexChanged");
                DisplayAlert("Error ", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Evento SelectedIndexChanged del Combo de Tipo de Compra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickerTipoCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedValue = pickerTipoProducto.Items[pickerTipoProducto.SelectedIndex];

                TipoProducto tipoProducto = allTipoProductos.Single(
                    delegate (TipoProducto x)
                    {
                        return selectedValue.ToString() == x.Nombre;
                    }
                    );


                txtIdTipoProducto.Text = tipoProducto.TipoProductoID;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta : PickerTipoCompra_SelectedIndexChanged");
                DisplayAlert("Error ", ex.Message, "OK");
            }

        }

        /// <summary>
        /// Método utilizado para mostrar el modal para asignar los gastos relacionados a la compra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (compra.Gastos == null)
                    compra.Gastos = new List<CatalogoGasto>();

                //var agregarGasto = new AgregarGasto();
                //agregarGasto.BindingContext =  compra;
                //await Navigation.PushAsync(agregarGasto);

                if (_viewModel.compra == null)
                {
                    _viewModel.compra = new Compra();
                    _viewModel.compra.Gastos = new List<CatalogoGasto>();

                }

                await PopupNavigation.PushAsync(new Gastos(ref _viewModel), true);

                actualizarTotales();


                //int nn = _viewModel.compra.Gastos.Count();
                //await DisplayAlert("Exitoso", nn.ToString(), "OK");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta : BtnAgregar_Clicked");
                await DisplayAlert("Error ", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Evento Click para actualizar los costos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnActualizarCostos_Clicked(object sender, EventArgs e)
        {
            try
            {
                actualizarTotales();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta : BtnActualizarCostos_Clicked");
                DisplayAlert("Error ", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Evento Click del botón para guardar la venta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void BtnGuardarVenta_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (_viewModel != null && _viewModel.compra != null && _viewModel.compra.Gastos != null)
                {
                    actualizarTotales();

                    _viewModel.compra.Descripcion = txtDescripción.Text;
                    _viewModel.compra.CantidadUnidades = Convert.ToInt32(txtUnidades.Text);
                    _viewModel.compra.proveedor = new Proveedor();
                    _viewModel.compra.proveedor.ProveedorID = txtIdProveedor.Text;
                    _viewModel.compra.proveedor.NombreEmpresa = picker.SelectedItem.ToString();
                    _viewModel.compra.tipoProducto = new TipoProducto();
                    _viewModel.compra.tipoProducto.TipoProductoID = txtIdTipoProducto.Text;
                    _viewModel.compra.tipoProducto.Nombre = pickerTipoProducto.SelectedItem.ToString();
                    _viewModel.compra.PrecioTotalCompra = Convert.ToDecimal(lblMontoTotal.Text.Replace("$", "").Replace(" ", ""));
                    _viewModel.compra.PrecioEstimadoUnidad = Convert.ToDecimal(lblMontoPolipo.Text.Replace("$", "").Replace(" ", ""));
                    _viewModel.compra.FechaCompra = Convert.ToDateTime(lblFecha.Text);
                    _viewModel.compra.ImagenURL = txtURL.Text;

                    Logger.Info(string.Concat("Se registró correctamente la compra de: ", txtDescripción.Text));

                    await firebaseHelperCompras.Add(_viewModel.compra);

                    resetearControles();
                    await DisplayAlert("Exitoso", "Compra Registrada Exitosamente", "OK");
                }
                else
                    await DisplayAlert("Error", "Es necesario agregar costos", "OK");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta : BtnGuardarVenta_Clicked");
                await DisplayAlert("Error ", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Evento Click del DatePicker para seleccionar la fecha de compra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                lblFecha.Text = e.NewDate.ToShortDateString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta : DatePicker_DateSelected");
                DisplayAlert("Error ", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Evento Click para borrar de la lista de gasto, uno de ellos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void DeleteClicked(Object sender, EventArgs args)
        {
            try
            {
                bool Respuesta = await DisplayAlert("Confirmar", "¿Está seguro de eliminar?", "Si", "No");
                if (Respuesta)
                {
                    var item = (Xamarin.Forms.Button)sender;
                    CatalogoGasto listitem = (from itm in _viewModel.compra.Gastos
                                              where itm.Nombre == item.CommandParameter.ToString()
                                              select itm).FirstOrDefault<CatalogoGasto>();

                    _viewModel.compra.Gastos.Remove(listitem);

                    actualizarListaGastos();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta : DeleteClicked");
                await DisplayAlert("Error ", ex.Message, "OK");
            }

        }


        #endregion



        #region Métodos

        /// <summary>
        /// Llenará la lista de Proveedores
        /// </summary>
        async private void fillProveedores()
        {
            if (picker.Items.Count < 1)
            {
                allProveedores = await firebaseHelper.GetAllProveedores();
                foreach (Proveedor proveedor in allProveedores)
                {
                    picker.Items.Add(proveedor.NombreEmpresa);
                }
            }
        }

        /// <summary>
        /// Llenará la lista de tipo de Productos.
        /// </summary>
        async private void fillTipoProductos()
        {
            try
            {
                if (pickerTipoProducto.Items.Count < 1)
                {

                    allTipoProductos = await firebaseHelperProductos.GetAllTipoProductos();
                    foreach (TipoProducto tipoProducto in allTipoProductos)
                    {
                        pickerTipoProducto.Items.Add(tipoProducto.Nombre);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualizará el total de la compra (incluyendo todos los gastos relacionados)
        /// </summary>
        async private void actualizarTotales()
        {
            try
            { 
                decimal montoTotal = 0;
                decimal montoPolipo = 0;

                if (_viewModel.compra != null && _viewModel.compra.Gastos != null)
                {
                    foreach (CatalogoGasto gasto in _viewModel.compra.Gastos)
                        montoTotal += gasto.Monto;



                    if (txtUnidades.Text != string.Empty)
                    {
                        montoPolipo = montoTotal / (Convert.ToInt32(txtUnidades.Text));
                    }

                    actualizarListaGastos();
                }

                lblMontoTotal.Text = montoTotal.ToString("C2");
                lblMontoPolipo.Text = montoPolipo.ToString("C2");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualizará la lista que muestra el detalle de los gastos relacionados a la compra.
        /// </summary>
        async private void actualizarListaGastos()
        {
            try
            {
                if (_viewModel != null && _viewModel.compra != null && _viewModel.compra.Gastos != null && _viewModel.compra.Gastos.Count > 0)
                {
                    lstGastos.ItemsSource = null;
                    lstGastos.ItemsSource = _viewModel.compra.Gastos;
                    lstGastos.IsVisible = true;
                }
                else
                    lstGastos.IsVisible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método utilizado para resetear los controles a su estado inicial.
        /// </summary>
        private void resetearControles()
        {
            try
            { 
                txtDescripción.Text = string.Empty;
                txtIdProveedor.Text = string.Empty;
                txtIdTipoProducto.Text = string.Empty;
                txtUnidades.Text = string.Empty;
                txtURL.Text = string.Empty;
                lblMontoTotal.Text = string.Empty;
                lblMontoPolipo.Text = string.Empty;

                _viewModel = new CompraDetailViewModel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método utilizado para generar los handlers de las imágenes que funcionan como botón.
        /// </summary>
        public void GenerarHandlers()
        {
            try
            {
                TapGestureRecognizer tapEventSave = new TapGestureRecognizer();
                tapEventSave.Tapped += BtnGuardarVenta_Clicked;
                myImgSave.GestureRecognizers.Add(tapEventSave);

                TapGestureRecognizer tapEventAdd = new TapGestureRecognizer();
                tapEventAdd.Tapped += BtnAgregar_Clicked;
                myImgAdd.GestureRecognizers.Add(tapEventAdd);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


        #endregion











    }
}