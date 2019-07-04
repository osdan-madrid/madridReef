
using madridReef.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace madridReef.Services
{
    public class ComprasHelper
    {
        FirebaseClient firebase = new FirebaseClient(AppSettings.FirebaseClient);

        /// <summary>
        /// Obtendra la lista de todos los elementos que existen en la DB
        /// </summary>
        /// <returns></returns>
        public async Task<List<Compra>> GetAll()
        {

            return (await firebase
              .Child("Compras")
              .OnceAsync<Compra>()).Select(item => new Compra
              {
                  Id = item.Key, 
                  Descripcion = item.Object.Descripcion,
                  CantidadUnidades = item.Object.CantidadUnidades,
                  FechaCompra = item.Object.FechaCompra,
                  Gastos = item.Object.Gastos,
                  ImagenURL = item.Object.ImagenURL,
                  PrecioTotalCompra = item.Object.PrecioTotalCompra,
                  proveedor = item.Object.proveedor,
                  tipoProducto = item.Object.tipoProducto,
                  FechaRegistro = item.Object.FechaRegistro,
                  FechaModificacion = item.Object.FechaModificacion

              }).ToList();
        }
        /// <summary>
        /// Obtendra la lista de todos los elementos que existen en la DB
        /// </summary>
        /// <returns></returns>
        public async Task<List<Compra>> GetAllColoniaMadres()
        {

            var all = await GetAll();
            await firebase
              .Child("Compras")
              .OnceAsync<Compra>();
            return all.Where(a => a.tipoProducto.Nombre == "Colonia Madre").ToList();
        }

        /// <summary>
        /// Agregará un nuevo registro.
        /// </summary>
        /// <param name="item">Entidad de negocio con la información a agregar.</param>
        /// <returns></returns>
        public async Task Add(Compra item)
        {
            await firebase
              .Child("Compras")
              .PostAsync(new Compra()
              {

                  Descripcion = item.Descripcion,
                  CantidadUnidades = item.CantidadUnidades,
                  FechaCompra = item.FechaCompra,
                  Gastos = item.Gastos,
                  ImagenURL = item.ImagenURL,
                  PrecioTotalCompra = item.PrecioTotalCompra,
                  proveedor = item.proveedor,
                  tipoProducto = item.tipoProducto,
                  FechaRegistro = System.DateTime.Now

              });

        }

        /// <summary>
        /// Obtener el detalle del registro
        /// </summary>
        /// <param name="item">Entidad de negocio con la información a solicitar.</param>
        /// <returns></returns>
        public async Task<Compra> GetOne(Compra item)
        {
            var all = await GetAll();
            await firebase
              .Child("Compras")
              .OnceAsync<Compra>();
            return all.Where(a => a.Id == item.Id).FirstOrDefault();
        }

        /// <summary>
        /// Actualizará el registro con la información proporcionada.
        /// </summary>
        /// <param name="item">Entidad de negocio con la información a actualizar.</param>
        /// <returns></returns>
        public async Task Update(Compra item)
        {
            var toUpdate = (await firebase
              .Child("Compras")
              .OnceAsync<Compra>()).Where(a => a.Key == item.Id).FirstOrDefault();

            await firebase
              .Child("Compras")
              .Child(toUpdate.Key)
              .PutAsync(new Compra()
              {
                  Descripcion = item.Descripcion,
                  CantidadUnidades = item.CantidadUnidades,
                  FechaCompra = item.FechaCompra,
                  Gastos = item.Gastos,
                  ImagenURL = item.ImagenURL,
                  PrecioTotalCompra = item.PrecioTotalCompra,
                  proveedor = item.proveedor,
                  tipoProducto = item.tipoProducto,
                  FechaRegistro = System.DateTime.Now
              });




        }

        /// <summary>
        /// Eliminará el registro
        /// </summary>
        /// <param name="item">Entidad de negocio.</param>
        /// <returns></returns>
        public async Task Delete(Compra item)
        {
            var toDelete = (await firebase
              .Child("Compras")
              .OnceAsync<Compra>()).Where(a => a.Key == item.Id).FirstOrDefault();
            await firebase.Child("Compras").Child(toDelete.Key).DeleteAsync();

        }
    }

}
