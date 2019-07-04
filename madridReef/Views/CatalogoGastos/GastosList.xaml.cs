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

namespace madridReef.Views.CatalogoGastos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GastosList : ContentPage
    {
        CatalogoGastosHelper firebaseHelper = new CatalogoGastosHelper();
        public GastosList()
        {
            InitializeComponent();


        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allItems = await firebaseHelper.GetAll();
            ItemsListView.ItemsSource = allItems;
            
        }


        async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new newGastosDetails()));
            //await Navigation.PushAsync(new NavigationPage(new newGastosDetails())); 
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as CatalogoGasto;
            if (item == null)
                return;


            await Navigation.PushAsync(new GastosDetails (new ViewModels.CatalogoGastos.ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }
}