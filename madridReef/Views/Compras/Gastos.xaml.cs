using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using madridReef.Models;
using madridReef.ViewModels.Compras;
using madridReef.Services;

namespace madridReef.Views.Compras
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Gastos 
    {
        CompraDetailViewModel viewModel;
        CatalogoGastosHelper firebaseHelper = new CatalogoGastosHelper();
        List<CatalogoGasto> allItems;
        //Compra compraGlobal = new Compra();

        public Gastos(ref CompraDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;


            TapGestureRecognizer tapEventSave = new TapGestureRecognizer();
            tapEventSave.Tapped += BtnAgregar_Clicked;
            myImgAdd.GestureRecognizers.Add(tapEventSave);

            TapGestureRecognizer tapEventClose = new TapGestureRecognizer();
            tapEventClose.Tapped += BtnCancelar_Clicked;
            myImgBack.GestureRecognizers.Add(tapEventClose);
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

            allItems = await firebaseHelper.GetAll();
            foreach (CatalogoGasto gasto in allItems)
            {
                picker.Items.Add(gasto.Nombre);
            }
            //picker.ItemsSource = allItems;


        }

        //async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        //{
        //    var item = args.SelectedItem as Gasto;
        //    if (item == null)
        //        return;


        //    //await Navigation.PushAsync(new GastosDetails(new ViewModels.Gastos.ItemDetailViewModel(item)));

        //    // Manually deselect item.
        //    //ItemsListView.SelectedItem = null;
        //}

        //private void TaskEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(TaskEntry.Text))
        //        TaskButton.IsEnabled = true;
        //    else if (string.IsNullOrEmpty(TaskEntry.Text))
        //        TaskButton.IsEnabled = false;
        //}

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            //await PopupNavigation.PopAsync();
            await PopupNavigation.PopAsync(true);
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = picker.Items[picker.SelectedIndex];
            
            CatalogoGasto gasto = allItems.Single(
                delegate (CatalogoGasto x)
                {
                    return selectedValue.ToString() == x.Nombre;      
                }
                );

            txtMonto.Text = gasto.Monto.ToString();
            txtID.Text = gasto.GastoId;
            //await Navigation.PushAsync(new GastosDetails(new ViewModels.Gastos.ItemDetailViewModel(item)));

            // Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }

        async private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            CatalogoGasto _nuevoGasto = new CatalogoGasto();
            _nuevoGasto.GastoId = txtID.Text;
            _nuevoGasto.Monto = Convert.ToDecimal(txtMonto.Text);
            _nuevoGasto.Nombre = picker.Items[picker.SelectedIndex].ToString();


            if (this.viewModel.compra == null)
                this.viewModel.compra = new Compra();

            if(this.viewModel.compra.Gastos ==null)
                this.viewModel.compra.Gastos = new List<CatalogoGasto>();

            

            this.viewModel.compra.Gastos.Add(_nuevoGasto);

            actualizarTotal();

            //resetearControles();
            PopupNavigation.PopAsync();

        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            resetearControles();
            PopupNavigation.PopAsync();

        }

        private void resetearControles()
        {
            txtID.Text = string.Empty;
            txtMonto.Text = string.Empty;

        }

        private void actualizarTotal()
        {
            if (this.viewModel.compra != null && this.viewModel.compra.Gastos != null)
            {


                foreach (CatalogoGasto gasto in this.viewModel.compra.Gastos)
                    this.viewModel.compra.PrecioTotalCompra += gasto.Monto;

            }
        }
    }
}