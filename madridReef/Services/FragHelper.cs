using madridReef.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace madridReef.Services
{
    
    public class FragHelper
    {
        FirebaseClient firebase = new FirebaseClient(AppSettings.FirebaseClient);
        public async Task<List<Frag>> GetAll()
        {

            return (await firebase
              .Child("Frags")
              .OnceAsync<Frag>()).Select(item => new Frag
              {
                  Id = item.Key,
                  CantidadPolipos = item.Object.CantidadPolipos,
                  ColoniaMadre = item.Object.ColoniaMadre,
                  FechaElaboracion = item.Object.FechaElaboracion,
                  FechaVenta = item.Object.FechaVenta,
                  Gastos = item.Object.Gastos,
                  ImagenURL = item.Object.ImagenURL,
                  PrecioSugeridoVenta = item.Object.PrecioSugeridoVenta,
                  //ValorPolipos = item.Object.ValorPolipos,
                  FechaRegistro = item.Object.FechaRegistro,
                  FechaModificacion = item.Object.FechaModificacion

              }).ToList();
        }

        public async Task Add(Frag _nuevo)
        {
            await firebase
              .Child("Frags")
              .PostAsync(new Frag()
              {
                  CantidadPolipos = _nuevo.CantidadPolipos,
                  ColoniaMadre = _nuevo.ColoniaMadre,
                  FechaElaboracion = _nuevo.FechaElaboracion,
                  FechaVenta = _nuevo.FechaVenta,
                  Gastos = _nuevo.Gastos,
                  ImagenURL = _nuevo.ImagenURL,
                  PrecioSugeridoVenta = _nuevo.PrecioSugeridoVenta,
                  PrecioElaboracion = _nuevo.PrecioElaboracion,
                  PrecioDeVenta = _nuevo.PrecioDeVenta,
                  Descripcion = _nuevo.Descripcion,
                  Pagado = _nuevo.Pagado,
                  Ganancia = _nuevo.Ganancia,
                  TotalOtrosGastos = _nuevo.TotalOtrosGastos,
                  NombreComprador = _nuevo.NombreComprador,
                  FechaRegistro = System.DateTime.Now
              });

        }

        public async Task<Frag> GetOne(Frag item)
        {
            var all = await GetAll();
            await firebase
              .Child("Frags")
              .OnceAsync<Frag>();
            return all.Where(a => a.Id == item.Id).FirstOrDefault();
        }

        public async Task<Frag> GetFiltered(Frag item)
        {
            var all = await GetAll();
            await firebase
              .Child("Frags")
              .OnceAsync<Frag>();
            return all.Where(a => a.Estatus == item.Estatus).FirstOrDefault();
        }

        public async Task Update(Frag item)
        {
            var toUpdate = (await firebase
              .Child("Frags")
              .OnceAsync<Frag>()).Where(a => a.Key == item.Id).FirstOrDefault();

            await firebase
              .Child("Frags")
              .Child(toUpdate.Key)
              .PutAsync(new Frag()
              {
                  CantidadPolipos = item.CantidadPolipos,
                  ColoniaMadre = item.ColoniaMadre,
                  FechaElaboracion = item.FechaElaboracion,
                  FechaVenta = item.FechaVenta,
                  Gastos = item.Gastos,
                  ImagenURL = item.ImagenURL,
                  PrecioSugeridoVenta = item.PrecioSugeridoVenta,
                  //ValorPolipos = item.ValorPolipos,
                  FechaRegistro = item.FechaRegistro,
                  FechaModificacion = System.DateTime.Now
              });




        }

        public async Task Delete(Frag item)
        {
            var toDelete = (await firebase
              .Child("Frags")
              .OnceAsync<Frag>()).Where(a => a.Key == item.Id).FirstOrDefault();
            await firebase.Child("Frags").Child(toDelete.Key).DeleteAsync();

        }
    }
}
