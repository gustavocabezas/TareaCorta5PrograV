using Datos;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class CategoriasNeg
    {

        public async Task<DataTable> VerCategoria()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var httpClient = new BaseHttpClient())
                {
                    var response = await httpClient.GetAsync("api/Categorias");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var categoriaResponse = JsonConvert.DeserializeObject<List<Categorias>>(jsonResponse);

                        dt = ConvertirListaToDataTable(categoriaResponse);
                    }
                    else
                    {
                        Debug.WriteLine($"\nError in CategoriasNeg.VerCategoria(): {response.StatusCode} - {response.ReasonPhrase}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
                Debugger.Break(); Debugger.Break();
#endif 
            }

            return dt;
        }

        public async Task<bool> GuardarCategoria(string categoria)
        {
            DataTable dt = new DataTable();
            bool categoriaExistente = true;

            try
            {
                using (var httpClient = new BaseHttpClient())
                {
                    var response = await httpClient.GetAsync("api/Categorias/PorNombre/" + categoria);

                    if (!response.IsSuccessStatusCode)
                    {
                        string jsonRequest = JsonConvert.SerializeObject(new Categorias() { Nombre = categoria });
                        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                        var response2 = await httpClient.PostAsync("api/Categorias/", content);
                        if (response2.IsSuccessStatusCode)
                            categoriaExistente = false;
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message);
                Debugger.Break();
#endif
            }

            return categoriaExistente;
        }

        public async Task<bool> EditarCategoria(string nombreAnterior, string nuevoNombre)
        {
            bool ActualizacionExitosa = false;
            Categorias categoriaNuevoNombre = new Categorias();

            try
            {
                using (var httpClient = new BaseHttpClient())
                {
                    var responseNuevoNombre = await httpClient.GetAsync("api/Categorias/PorNombre/" + nuevoNombre);
                    if (responseNuevoNombre.IsSuccessStatusCode)
                        return false;
                    else
                        categoriaNuevoNombre.Nombre = nuevoNombre;

                    var responseNombreAnterior = await httpClient.GetAsync("api/Categorias/PorNombre/" + nombreAnterior);
                    if (responseNombreAnterior.IsSuccessStatusCode)
                    {
                        string jsonResponseNombreAnterior = await responseNombreAnterior.Content.ReadAsStringAsync();
                        var categoriaResponseNombreAnterior = JsonConvert.DeserializeObject<Categorias>(jsonResponseNombreAnterior);
                        categoriaNuevoNombre.idCategoria = categoriaResponseNombreAnterior.idCategoria;
                    }
                    else
                        return false;

                    string jsonRequest = JsonConvert.SerializeObject(categoriaNuevoNombre);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync($"api/Categorias/?nombre={nombreAnterior}", content);
                    if (response.IsSuccessStatusCode)
                        ActualizacionExitosa = true;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message);
                Debugger.Break();
#endif
            }

            return ActualizacionExitosa;
        }

        public async Task<bool> EliminarCategoria(string nombre)
        {
            bool exito = false;

            try
            {
                using (var httpClient = new BaseHttpClient())
                {
                    var response = await httpClient.DeleteAsync($"api/Categorias/?nombre={nombre}");
                    if (response.IsSuccessStatusCode)
                        exito = true;
                }

                return exito;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message);
                Debugger.Break();
#endif
                return exito;
            }
        }

        private DataTable ConvertirListaToDataTable(IList data)
        {

            var properties = TypeDescriptor.GetProperties(typeof(Categorias));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (Categorias item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties) row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

    }
}