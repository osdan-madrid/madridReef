using madridReef.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace madridReef.Services
{
    public class CatalogoGastosHelper
    {
        FirebaseClient firebase = new FirebaseClient(AppSettings.FirebaseClient);

        public async Task<List<CatalogoGasto>> GetAll()
        {

            return (await firebase
              .Child("Gastos")
              .OnceAsync<CatalogoGasto>()).Select(item => new CatalogoGasto
              {
                  GastoId = item.Key, 
                  Descripcion = item.Object.Descripcion,
                  Nombre = item.Object.Nombre,
                  Monto = item.Object.Monto,
                  FechaRegistro = item.Object.FechaRegistro,
                  FechaModificacion = item.Object.FechaModificacion
                
              }).ToList();
        }

        public async Task Add(CatalogoGasto _nuevo)
        {
            await firebase
              .Child("Gastos")
              .PostAsync(new CatalogoGasto()
              {
                 Descripcion = _nuevo.Descripcion,
                 Nombre = _nuevo.Nombre,
                 FechaRegistro = System.DateTime.Now,
                 Monto = _nuevo.Monto
              });

        }

        public async Task<CatalogoGasto> GetOne(CatalogoGasto item)
        {
            var all = await GetAll();
            await firebase
              .Child("Gastos")
              .OnceAsync<CatalogoGasto>();
            return all.Where(a => a.GastoId == item.GastoId).FirstOrDefault();
        }

        public async Task Update(CatalogoGasto item)
        {
            var toUpdate = (await firebase
              .Child("Gastos")
              .OnceAsync<CatalogoGasto>()).Where(a => a.Key == item.GastoId).FirstOrDefault();

            await firebase
              .Child("Gastos")
              .Child(toUpdate.Key)
              .PutAsync(new CatalogoGasto()
              {
                  Nombre = item.Nombre,
                  Descripcion = item.Descripcion,
                  Monto = item.Monto,
                  FechaRegistro = item.FechaRegistro,
                  FechaModificacion = System.DateTime.Now
              });




        }

        public async Task Delete(CatalogoGasto item)
        {
            var toDelete = (await firebase
              .Child("Gastos")
              .OnceAsync<CatalogoGasto>()).Where(a => a.Key == item.GastoId).FirstOrDefault();
            await firebase.Child("Gastos").Child(toDelete.Key).DeleteAsync();

        }
    }

}
