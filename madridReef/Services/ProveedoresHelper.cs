using madridReef.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace madridReef.Services
{
    public class ProveedoresHelper
    {
        FirebaseClient firebase = new FirebaseClient(AppSettings.FirebaseClient);

        public async Task<List<Proveedor>> GetAllProveedores()
        {
            
            return (await firebase
              .Child("Proveedores")
              .OnceAsync<Proveedor>()).Select(item => new Proveedor
              {
                  ProveedorID = item.Key,
                  NombreEmpresa = item.Object.NombreEmpresa,
                  NombreCompleto = item.Object.NombreCompleto,
                  NoCelular = item.Object.NoCelular,
                  FacebookURLProfile = item.Object.FacebookURLProfile,
                  FechaRegistro = item.Object.FechaRegistro,
                  FechaModificacion = item.Object.FechaModificacion

              }).ToList();
        }

        public async Task AddProveedor(Proveedor nuevoProveedor)
        {
            await firebase
              .Child("Proveedores")
              .PostAsync(new Proveedor() {
                  NombreEmpresa = nuevoProveedor.NombreEmpresa,
                  NombreCompleto = nuevoProveedor.NombreCompleto,
                  NoCelular = nuevoProveedor.NoCelular,
                  FechaRegistro = System.DateTime.Now,
                  FechaModificacion = null,
                  FacebookURLProfile = nuevoProveedor.FacebookURLProfile,
                });

        }

        public async Task<Proveedor> GetProveedor(Proveedor proveedor)
        {
            var all = await GetAllProveedores();
            await firebase
              .Child("Proveedores")
              .OnceAsync<Proveedor>();
            return all.Where(a => a.ProveedorID == proveedor.ProveedorID).FirstOrDefault();
        }

        public async Task UpdateProveedor(Proveedor proveedor)
        {
            var toUpdate = (await firebase
              .Child("Proveedores")
              .OnceAsync<Proveedor>()).Where(a => a.Object.ProveedorID == proveedor.ProveedorID).FirstOrDefault();

            await firebase
              .Child("Proveedores")
              .Child(toUpdate.Key)
              .PutAsync(new Proveedor() {
                  NombreEmpresa = proveedor.NombreEmpresa,
                  NombreCompleto = proveedor.NombreCompleto,
                  NoCelular = proveedor.NoCelular,
                  FechaModificacion = System.DateTime.Now,
                  FechaRegistro = proveedor.FechaRegistro,
                  FacebookURLProfile = proveedor.FacebookURLProfile,
              });




        }

        public async Task DeleteProveedor(Proveedor proveedor)
        {
            var toDelete = (await firebase
              .Child("Proveedores")
              .OnceAsync<Proveedor>()).Where(a => a.Object.ProveedorID == proveedor.ProveedorID).FirstOrDefault();
            await firebase.Child("Proveedores").Child(toDelete.Key).DeleteAsync();

        }
    }

}
