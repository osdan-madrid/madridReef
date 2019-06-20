using madridReef.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace madridReef.Services
{
    public class TipoProductosHelper
    {
        FirebaseClient firebase = new FirebaseClient(AppSettings.FirebaseClient);

        public async Task<List<TipoProducto>> GetAllTipoProductos()
        {
            
            return (await firebase
              .Child("TipoProductos")
              .OnceAsync<TipoProducto>()).Select(item => new TipoProducto
              {
                  TipoProductoID = item.Object.TipoProductoID,
                  Nombre = item.Object.Nombre,
                  FechaRegistro = item.Object.FechaRegistro,
                  FechaModificacion = item.Object.FechaModificacion

              }).ToList();
        }

        public async Task AddTipoProducto(TipoProducto tipoProducto)
        {
            string ID = FirebaseKeyGenerator.Next();
            await firebase
              .Child("TipoProductos")
              .PostAsync(new TipoProducto() {
                  TipoProductoID = ID,
                  Nombre = tipoProducto.Nombre,
                  FechaRegistro = System.DateTime.Now,
                  FechaModificacion = null,
                });

        }

        public async Task<TipoProducto> GetTipoProducto(TipoProducto tipoProducto)
        {
            var all = await GetAllTipoProductos();
            await firebase
              .Child("TipoProductos")
              .OnceAsync<TipoProducto>();
            return all.Where(a => a.TipoProductoID == tipoProducto.TipoProductoID).FirstOrDefault();
        }

        public async Task UpdateTipoProducto(TipoProducto tipoProducto)
        {
            var toUpdate = (await firebase
              .Child("TipoProductos")
              .OnceAsync<TipoProducto>()).Where(a => a.Object.TipoProductoID == tipoProducto.TipoProductoID).FirstOrDefault();

            await firebase
              .Child("TipoProductos")
              .Child(toUpdate.Key)
              .PutAsync(new TipoProducto() {
                  Nombre = tipoProducto.Nombre,
                  FechaModificacion = System.DateTime.Now,
              });




        }

        public async Task DeleteProveedor(TipoProducto tipoProducto)
        {
            var toDelete = (await firebase
              .Child("TipoProductos")
              .OnceAsync<TipoProducto>()).Where(a => a.Object.TipoProductoID == tipoProducto.TipoProductoID).FirstOrDefault();
            await firebase.Child("TipoProductos").Child(toDelete.Key).DeleteAsync();

        }
    }

}
