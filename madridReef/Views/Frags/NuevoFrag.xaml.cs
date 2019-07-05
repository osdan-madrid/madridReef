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

namespace madridReef.Views.Frags
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoFrag : ContentPage
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

        public NuevoFrag()
        {
            InitializeComponent();
            GenerarHandlers();


        }
        public NuevoFrag(Frag frag = null)
        {
            InitializeComponent();

        }

      

        protected async override void OnAppearing()
        {

            base.OnAppearing();

            frag.Gastos = new List<CatalogoGasto>();

            resetearControles();
            actualizarListaGastos();
            //fillProveedores();
            //fillTipoProductos();
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

            await PopupNavigation.PushAsync(new Gastos(ref _viewModel), true);

            actualizarTotales();


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
            actualizarTotales();
        }

        /// <summary>
        /// Evento Click del botón para guardar la venta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void BtnGuardarVenta_Clicked(object sender, EventArgs e)
        {
            if (_viewModel != null && _viewModel.frag != null && _viewModel.frag.Gastos != null)
            {
                //actualizarTotales();

                //_viewModel.frag.Descripcion = txtDescripción.Text;
                //_viewModel.frag.CantidadUnidades = Convert.ToInt32(txtUnidades.Text);
                //_viewModel.frag.proveedor = new Proveedor();
                //_viewModel.frag.proveedor.ProveedorID = txtIdProveedor.Text;
                //_viewModel.frag.proveedor.NombreEmpresa = picker.SelectedItem.ToString();
                //_viewModel.frag.tipoProducto = new TipoProducto();
                //_viewModel.frag.tipoProducto.TipoProductoID = txtIdTipoProducto.Text;
                //_viewModel.frag.tipoProducto.Nombre = pickerTipoProducto.SelectedItem.ToString();
                //_viewModel.frag.PrecioTotalCompra = Convert.ToDecimal(lblMontoTotal.Text);
                //_viewModel.frag.PrecioEstimadoUnidad = Convert.ToDecimal(lblMontoPolipo.Text);
                //_viewModel.frag.FechaCompra = Convert.ToDateTime(lblFecha.Text);
                //_viewModel.frag.ImagenURL = txtURL.Text;
                //_viewModel.frag.PrecioEstimadoUnidad = Convert.ToDecimal(lblMontoPolipo.Text);

                //await firebaseHelperCompras.Add(_viewModel.compra);

                //resetearControles();
                await DisplayAlert("Exitoso", "Compra Registrada Exitosamente", "OK");
            }
            else
                await DisplayAlert("Error", "Es necesario agregar costos", "OK");
        }

        /// <summary>
        /// Evento Click del DatePicker para seleccionar la fecha de compra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            lblFechaVenta.Text = e.NewDate.ToShortDateString();
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
        async private void fillColoniaMadre()
        {

            allCompras = await firebaseHelperCompras.GetAllColoniaMadres();
            foreach (Compra compra in allCompras)
            {
                pickerColoniaMadre.Items.Add(compra.Descripcion);
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
            decimal montoTotal = 0;
            decimal montoPolipo = 0;
            if (_viewModel.frag != null && _viewModel.frag.Gastos != null)
            {
                foreach (CatalogoGasto gasto in _viewModel.frag.Gastos)
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
                await DisplayAlert("Error", "Ocurrió el siguiente error: " + ex.Message, "OK");
            }
        }

        /// <summary>
        /// Método utilizado para resetear los controles a su estado inicial.
        /// </summary>
        private void resetearControles()
        {
            txtDescripción.Text = string.Empty;
            //txtIdProveedor.Text = string.Empty;
            //txtIdTipoProducto.Text = string.Empty;
            txtUnidades.Text = string.Empty;
            txtURL.Text = string.Empty;
            _viewModel = new FragDetailViewModel();
        }

        /// <summary>
        /// Método utilizado para generar los handlers de las imágenes que funcionan como botón.
        /// </summary>
        public void GenerarHandlers()
        {
            TapGestureRecognizer tapEventSave = new TapGestureRecognizer();
            tapEventSave.Tapped += BtnGuardarVenta_Clicked;
            myImgSave.GestureRecognizers.Add(tapEventSave);

            TapGestureRecognizer tapEventAdd = new TapGestureRecognizer();
            tapEventAdd.Tapped += BtnAgregar_Clicked;
            myImgAdd.GestureRecognizers.Add(tapEventAdd);



        }















        #endregion

        private void PickerColoniaMadre_SelectedIndexChanged(object sender, EventArgs e)
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

        private void DatePickerElaboracion_DateSelected(object sender, DateChangedEventArgs e)
        {
            lblFechaElaboracion.Text = e.NewDate.ToShortDateString();
        }

        private void TxtUnidades_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lblMontoPolipo.Text != string.Empty && lblMontoPolipo.Text != string.Empty)
            {

                lblMontoPolipo.Text =  Convert.ToString( Convert.ToInt32( txtUnidades.Text) * Convert.ToDecimal( lblMontoPolipo.Text));
            }

        }
    }
}





