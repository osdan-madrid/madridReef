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
using madridReef.ViewModels.Frag;
using System.Reflection;

namespace madridReef.Views.Catalogos.Frags
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Nuevo : ContentPage
    {
        Frag frag = new Frag();
        FragDetailViewModel _viewModel = new FragDetailViewModel();
        FragHelper firebaseHelperFrag = new FragHelper();

        ProveedoresHelper firebaseHelper = new ProveedoresHelper();
        List<Proveedor> allProveedores;

        TipoProductosHelper firebaseHelperProductos = new TipoProductosHelper();
        List<TipoProducto> allTipoProductos;

        ComprasHelper firebaseHelperCompras = new ComprasHelper();
        
        

        List<Compra> allCompras;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Nuevo()
        {
            try
            { 
                InitializeComponent();
                GenerarHandlers();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta : BtnGuardarVenta_Clicked");
                DisplayAlert("Error ", ex.Message, "OK");
            }

        }
        public Nuevo(Frag frag = null)
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta : BtnGuardarVenta_Clicked");
                DisplayAlert("Error ", ex.Message, "OK");
            }

        }

      

        protected async override void OnAppearing()
        {
            try
            { 
                base.OnAppearing();

                frag.Gastos = new List<CatalogoGasto>();

                resetearControles();
                actualizarListaGastos();
                fillColoniasMadre();
                fillEstatus();


                Logger.Info("Prueba de logger");

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta : BtnGuardarVenta_Clicked");
                await DisplayAlert("Error ", ex.Message, "OK");
            }
        }

        #region Handlers


        /// <summary>
        /// Evento SelectedIndexChanged del Combo de Proveedores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var selectedValue = picker.Items[picker.SelectedIndex];

        //    Proveedor proveedor = allProveedores.Single(
        //        delegate (Proveedor x)
        //        {
        //            return selectedValue.ToString() == x.NombreEmpresa;
        //        }
        //        );


        //    txtIdProveedor.Text = proveedor.ProveedorID;
        //}

        ///// <summary>
        ///// Evento SelectedIndexChanged del Combo de Tipo de Compra
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void PickerTipoCompra_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var selectedValue = pickerTipoProducto.Items[pickerTipoProducto.SelectedIndex];

        //    TipoProducto tipoProducto = allTipoProductos.Single(
        //        delegate (TipoProducto x)
        //        {
        //            return selectedValue.ToString() == x.Nombre;
        //        }
        //        );


        //    txtIdTipoProducto.Text = tipoProducto.TipoProductoID;


        //}

        /// <summary>
        /// Método utilizado para mostrar el modal para asignar los gastos relacionados a la compra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            ////await Navigation.PushAsync(new AgregarGasto(ref compra));
            try
            {
                if (frag.Gastos == null)
                    frag.Gastos = new List<CatalogoGasto>();

                //var agregarGasto = new AgregarGasto();
                //agregarGasto.BindingContext =  compra;
                //await Navigation.PushAsync(agregarGasto);

                if (_viewModel.frag == null)
                {
                    _viewModel.frag = new Frag();
                    _viewModel.frag.Gastos = new List<CatalogoGasto>();

                }

                await PopupNavigation.PushAsync(new  Gastos(ref _viewModel), true);

                actualizarTotales();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta.");
                await DisplayAlert("Ocurrió el siguiente error: ", ex.Message, "OK");
            }


            //int nn = _viewModel.compra.Gastos.Count();
            //await DisplayAlert("Exitoso", nn.ToString(), "OK");
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
                Logger.Error(ex, "Error al registrar una venta : BtnGuardarVenta_Clicked");
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
                if (_viewModel != null && _viewModel.frag != null && _viewModel.frag.Gastos != null)
                {
                    actualizarTotales();

                    _viewModel.frag.Descripcion = txtDescripción.Text;
                    _viewModel.frag.CantidadPolipos = Convert.ToInt32(txtUnidades.Text);
                    _viewModel.frag.ColoniaMadre = new Compra();
                    _viewModel.frag.ColoniaMadre.Id = txtIdColoniaMadre.Text;
                    _viewModel.frag.ColoniaMadre.Descripcion = pickerColoniaMadre.SelectedItem.ToString();
                    _viewModel.frag.ColoniaMadre.PrecioEstimadoUnidad = Convert.ToDecimal(lblMontoPolipo.Text.Replace("$", "").Replace(" ", "")); 
                    _viewModel.frag.FechaElaboracion = Convert.ToDateTime(lblFechaElaboracion.Text);
                    _viewModel.frag.FechaVenta = lblFechaVenta.Text != string.Empty && lblFechaVenta.Text != "(null)" ? Convert.ToDateTime(lblFechaVenta.Text) : (DateTime?)null;
                    _viewModel.frag.ImagenURL = txtURL.Text;
                    _viewModel.frag.Pagado = txtPrecioVenta.Text != string.Empty && txtPrecioVenta.Text != "(null)" ? true : false;
                    _viewModel.frag.PrecioDeVenta = txtPrecioVenta.Text != string.Empty && txtPrecioVenta.Text != "(null)" ? Convert.ToDecimal(txtPrecioVenta.Text) : (decimal?) null;
                    _viewModel.frag.PrecioElaboracion = Convert.ToDecimal(lblMontoTotal.Text.Replace("$","").Replace(" ",""));
                    _viewModel.frag.PrecioSugeridoVenta = txtPrecioSugeridoVenta.Text != string.Empty ? Convert.ToDecimal(txtPrecioSugeridoVenta.Text) : (decimal?)null;
                    _viewModel.frag.Ganancia = _viewModel.frag.PrecioDeVenta != null ? _viewModel.frag.PrecioDeVenta - _viewModel.frag.PrecioElaboracion : (decimal?)null;
                    _viewModel.frag.NombreComprador = txtComprador.Text != string.Empty ? txtComprador.Text : null;
                    _viewModel.frag.Estatus = lblEstatus.Text;
                    

                    await firebaseHelperFrag.Add(_viewModel.frag);


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
        private void DatePickerVenta_DateSelected(object sender, DateChangedEventArgs e)
        {
            
            try
            {
                lblFechaVenta.Text = e.NewDate.ToShortDateString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error al registrar una venta : BtnGuardarVenta_Clicked");
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
            bool Respuesta = await DisplayAlert("Confirmar", "¿Está seguro de eliminar?", "Si", "No");
            if (Respuesta)
            {
                var item = (Xamarin.Forms.Button)sender;
                CatalogoGasto listitem = (from itm in _viewModel.frag.Gastos
                                          where itm.Nombre == item.CommandParameter.ToString()
                                          select itm).FirstOrDefault<CatalogoGasto>();

                _viewModel.frag.Gastos.Remove(listitem);

                actualizarListaGastos();
            }

        }

        /// <summary>
        /// Evento SelectedIndexChanged al seleccionar la colonia madre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickerColoniaMadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedValue = pickerColoniaMadre.Items[pickerColoniaMadre.SelectedIndex];

                Compra colonia = allCompras.Single(
                    delegate (Compra x)
                    {
                        return selectedValue.ToString() == x.Descripcion;
                    }
                    );


                txtIdColoniaMadre.Text = colonia.Id;
                lblMontoPolipo.Text = colonia.PrecioEstimadoUnidad.ToString();
            }
            catch (Exception ex) { throw ex; }

        }

        /// <summary>
        /// Evento DateSelected del seleccionador de fecha de elaboración
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePickerElaboracion_DateSelected(object sender, DateChangedEventArgs e)
        {
            lblFechaElaboracion.Text = e.NewDate.ToShortDateString();
        }

        /// <summary>
        /// Evento DateSelected del seleccionador de fecha de elaboración
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickerEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
    

                lblEstatus.Text = pickerColoniaMadre.Items[pickerEstatus.SelectedIndex].ToString();
            }
            catch (Exception ex) { throw ex; }


        }

        #endregion



        #region Métodos

        ///// <summary>
        ///// Llenará la lista de Proveedores
        ///// </summary>
        //async private void fillProveedores()
        //{
        //    allProveedores = await firebaseHelper.GetAllProveedores();
        //    foreach (Proveedor proveedor in allProveedores)
        //    {
        //        picker.Items.Add(proveedor.NombreEmpresa);
        //    }
        //}

        /// <summary>
        /// Llenará la lista de Colonias Madre
        /// </summary>
        async private void fillColoniasMadre()
        {
            try
            {
                pickerColoniaMadre.Items.Clear();

                allCompras = await firebaseHelperCompras.GetAllColoniasMadre();
                foreach (Compra compra in allCompras)
                {
                    pickerColoniaMadre.Items.Add(compra.Descripcion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void fillEstatus()
        {
            try
            {
                pickerEstatus.Items.Clear();

                Type type = typeof(Estatus_Frags);
                foreach (PropertyInfo propertyInfo in type.GetProperties())
                    pickerEstatus.Items.Add(propertyInfo.Name);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Llenará la lista de tipo de Productos.
        ///// </summary>
        //async private void fillTipoProductos()
        //{
        //    allTipoProductos = await firebaseHelperProductos.GetAllTipoProductos();
        //    foreach (TipoProducto tipoProducto in allTipoProductos)
        //    {
        //        pickerTipoProducto.Items.Add(tipoProducto.Nombre);
        //    }
        //}

        /// <summary>
        /// Actualizará el total de la compra (incluyendo todos los gastos relacionados)
        /// </summary>
        async private void actualizarTotales()
        {
            decimal montoTotal = lblMontoTotal.Text != string.Empty ? Convert.ToDecimal(lblMontoTotal.Text.Replace("$", "").Replace(" ", "")) : 0;
            decimal montoPolipo = lblMontoPolipo.Text != string.Empty ? Convert.ToDecimal(lblMontoPolipo.Text.Replace("$", "").Replace(" ", "")) : 0;

            if (txtUnidades.Text != string.Empty)
                montoTotal = montoPolipo / (Convert.ToInt32(txtUnidades.Text));


            if (_viewModel.frag != null && _viewModel.frag.Gastos != null)
            {
                foreach (CatalogoGasto gasto in _viewModel.frag.Gastos)
                    montoTotal += gasto.Monto;



                //if (txtUnidades.Text != string.Empty)
                //    montoPolipo = montoTotal / (Convert.ToInt32(txtUnidades.Text));

                actualizarListaGastos();
            }

            lblMontoTotal.Text = montoTotal.ToString("C2");
            //lblMontoPolipo.Text = montoPolipo.ToString("C2");
        }

        /// <summary>
        /// Actualizará la lista que muestra el detalle de los gastos relacionados a la compra.
        /// </summary>
        async private void actualizarListaGastos()
        {
            try
            {
                if (_viewModel != null && _viewModel.frag != null && _viewModel.frag.Gastos != null && _viewModel.frag.Gastos.Count > 0)
                {
                    lstGastos.ItemsSource = null;
                    lstGastos.ItemsSource = _viewModel.frag.Gastos;
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
                _viewModel = new FragDetailViewModel();
                txtComprador.Text = string.Empty;
                lblFechaElaboracion.Text = string.Empty;
                lblFechaVenta.Text = string.Empty;
                lblMontoPolipo.Text = string.Empty;
                lblMontoTotal.Text = string.Empty;
                txtIdColoniaMadre.Text = string.Empty;
                txtPrecioSugeridoVenta.Text = string.Empty;
                txtPrecioVenta.Text = string.Empty;
                txtUnidades.Text = string.Empty;
                txtURL.Text = string.Empty;

            }
            catch (Exception ex)
            { throw ex; }
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
            catch (Exception ex)
            { throw ex; }

        }

        /// <summary>
        /// Evento TextChanged, del control TxtUnidades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtUnidades_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (lblMontoPolipo.Text != string.Empty && txtUnidades.Text != string.Empty)
                {

                    lblMontoTotal.Text = Convert.ToString(Convert.ToInt32(txtUnidades.Text) * Convert.ToDecimal(lblMontoPolipo.Text));
                }
            }
            catch (Exception ex) { throw ex; }

        }





        #endregion


    }
}





