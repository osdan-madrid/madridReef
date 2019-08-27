using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using madridReef.Models;
using madridReef.ViewModels.Frag;
using madridReef.Services;

namespace madridReef.Views.Catalogos.Frags
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Gastos 
    {
        FragDetailViewModel viewModel;
        CatalogoGastosHelper firebaseHelper = new CatalogoGastosHelper();
        List<CatalogoGasto> allItems;

        public Gastos(ref FragDetailViewModel viewModel)
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
        


        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
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

            txtMonto.Text = gasto.Monto == 0? string.Empty : gasto.Monto.ToString();
            txtID.Text = gasto.GastoId;

        }

        async private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            CatalogoGasto _nuevoGasto = new CatalogoGasto();
            _nuevoGasto.GastoId = txtID.Text;
            _nuevoGasto.Monto = Convert.ToDecimal(txtMonto.Text);
            _nuevoGasto.Nombre = picker.Items[picker.SelectedIndex].ToString();


            if (this.viewModel.frag == null)
                this.viewModel.frag = new Frag();

            if(this.viewModel.frag.Gastos ==null)
                this.viewModel.frag.Gastos = new List<CatalogoGasto>();

            

            this.viewModel.frag.Gastos.Add(_nuevoGasto);

            actualizarTotal();

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
            decimal montoTotal = 0;
            if (this.viewModel.frag != null && this.viewModel.frag.Gastos != null)
            {


                foreach (CatalogoGasto gasto in this.viewModel.frag.Gastos)
                    montoTotal += gasto.Monto;


                this.viewModel.frag.TotalOtrosGastos = montoTotal;
            }
        }
    }
}