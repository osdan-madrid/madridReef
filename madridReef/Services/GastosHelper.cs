using madridReef.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace madridReef.Services
{
    public class GastosHelper
    {
        FirebaseClient firebase = new FirebaseClient(AppSettings.FirebaseClient);

        public async Task<List<Gasto>> GetAll()
        {

            return (await firebase
              .Child("Gastos")
              .OnceAsync<Gasto>()).Select(item => new Gasto
              {
                  GastoId = item.Key, //item.Object.GastoId,
                  Descripcion = item.Object.Descripcion,
                  Nombre = item.Object.Nombre,
                  Monto = item.Object.Monto,
                  FechaRegistro = item.Object.FechaRegistro,
                  FechaModificacion = item.Object.FechaModificacion
                
              }).ToList();
        }

        public async Task Add(Gasto _nuevo)
        {
            //string ID = FirebaseKeyGenerator.Next();
            await firebase
              .Child("Gastos")
              .PostAsync(new Gasto()
              {
                 //GastoId = ID,
                 Descripcion = _nuevo.Descripcion,
                 Nombre = _nuevo.Nombre,
                 FechaRegistro = System.DateTime.Now,
                 Monto = _nuevo.Monto
              });

        }

        public async Task<Gasto> GetOne(Gasto item)
        {
            var all = await GetAll();
            await firebase
              .Child("Gasto")
              .OnceAsync<Gasto>();
            return all.Where(a => a.GastoId == item.GastoId).FirstOrDefault();
        }

        public async Task Update(Gasto item)
        {
            var toUpdate = (await firebase
              .Child("Gastos")
              .OnceAsync<Gasto>()).Where(a => a.Key == item.GastoId).FirstOrDefault();

            await firebase
              .Child("Gastos")
              .Child(toUpdate.Key)
              .PutAsync(new Gasto()
              {
                  Nombre = item.Nombre,
                  Descripcion = item.Descripcion,
                  Monto = item.Monto,
                  FechaRegistro = item.FechaRegistro,
                  FechaModificacion = System.DateTime.Now
              });




        }

        public async Task Delete(Gasto item)
        {
            var toDelete = (await firebase
              .Child("Gastos")
              .OnceAsync<Gasto>()).Where(a => a.Key == item.GastoId).FirstOrDefault();
            await firebase.Child("Gastos").Child(toDelete.Key).DeleteAsync();

        }
    }

}
