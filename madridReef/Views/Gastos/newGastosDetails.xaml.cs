﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using madridReef.Services;
using madridReef.Models;

namespace madridReef.Views.Gastos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class newGastosDetails : ContentPage
    {
        GastosHelper firebaseHelper = new GastosHelper();

     
        public newGastosDetails()
        {
            InitializeComponent();
            ResetearControles();



        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            Gasto _nuevo = new Gasto();
            _nuevo.Descripcion = txtDescripcion.Text;
            _nuevo.Nombre = txtNombre.Text;
            _nuevo.Monto = Convert.ToDecimal( txtMonto.Text);
            //_nuevo.FechaRegistro = Convert.ToDateTime(txtFechaRegistro.Text);
            
            await firebaseHelper.Add(_nuevo);
            ResetearControles();
            await DisplayAlert("Exitoso", "Gasto Agregado Exitosamente", "OK");
            
        }

        private void ResetearControles()
        {
            txtDescripcion.Text = string.Empty;
            txtFechaModificacion.Text = string.Empty;
            txtFechaRegistro.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtMonto.Text = string.Empty;
        }

    }
}
