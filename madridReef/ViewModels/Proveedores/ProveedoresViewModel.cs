using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using madridReef.Models;
using madridReef.Views;


namespace madridReef.ViewModels.Proveedores
{

    public class ProveedoresViewModel:BaseViewModel
    {
        public ObservableCollection<Proveedor> Proveedores { get; set; }
        public Command LoadProveedoresCommand { get; set; }

        public ProveedoresViewModel()
        {
            Title = "Browse";
            Proveedores = new ObservableCollection<Proveedor>();
            LoadProveedoresCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Proveedor>(this, "AddProveedor", async (obj, proveedor) =>
            {
                var newProveedor = proveedor as Proveedor;
                Proveedores.Add(newProveedor);
                await DataStore.AddItemAsync(newProveedor);
            });
        }


        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Proveedores.Clear();
                var proveedores = await DataStore.GetItemsAsync(true);
                foreach (var proveedor in proveedores)
                {
                    Proveedores.Add(proveedor);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}
