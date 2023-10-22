using Datos;
using System;
using System.Data;
using System.Linq;

namespace Negocios
{
    public class PlatillosNeg
    {



        public void AgregarUsuario(string nombre, decimal costo)
        {
            try
            {
                using (LaCriollitaEntities db = new LaCriollitaEntities())
                {
                    Platillos new_Platillo = new Platillos();
                    new_Platillo.Nombre = nombre;
                    new_Platillo.Costo = costo;

                    db.Platillos.Add(new_Platillo);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public DataTable VerPlatillos()
        {
            DataTable dt = new DataTable();

            try
            {
                using (LaCriollitaEntities db = new LaCriollitaEntities())
                {
                    // se hace el linq para la conexion a la base de datos que mostrara nombre costo y categoria 
                    var lst = from d in db.Platillos
                              orderby d.Categorias.Nombre
                              select new
                              {
                                  Nombre = d.Nombre,
                                  Categoria = d.Categorias.Nombre,
                                  Estado = d.Estados.Nombre,
                                  Costo = d.Costo
                              };

                    // crea las columnas con su nombre respectivo 

                    dt.Columns.Add("Nombre", typeof(string));     // Nombre del platillo
                    dt.Columns.Add("Categoria", typeof(string)); // Nombre de la categoría
                    dt.Columns.Add("Estado", typeof(string)); // Nombre de la categoría
                    dt.Columns.Add("Costo", typeof(decimal));       // Costo del platillo


                    // recorre la tabla para agregar los datos a las columnas 
                    foreach (var item in lst)
                    {
                        dt.Rows.Add(item.Nombre, item.Categoria, item.Estado, item.Costo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dt;

        }

        public DataTable VerPlatillosActivos()
        {
            DataTable dt = new DataTable();

            try
            {
                using (LaCriollitaEntities db = new LaCriollitaEntities())
                {
                    // se hace el linq para la conexion a la base de datos que mostrara nombre costo y categoria 
                    var lst = from d in db.Platillos
                              where d.idEstado == 1
                              orderby d.Categorias.Nombre
                              select new
                              {
                                  Nombre = d.Nombre,
                                  Categoria = d.Categorias.Nombre,
                                  Costo = d.Costo
                              };

                    // crea las columnas con su nombre respectivo 

                    dt.Columns.Add("Nombre", typeof(string));     // Nombre del platillo
                    dt.Columns.Add("Categoria", typeof(string)); // Nombre de la categoría
                    dt.Columns.Add("Costo", typeof(decimal));       // Costo del platillo


                    // recorre la tabla para agregar los datos a las columnas 
                    foreach (var item in lst)
                    {
                        dt.Rows.Add(item.Nombre, item.Categoria, item.Costo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dt;

        }


        public void ModificarEstadoPlatillo(string nombre, int nuevoEstado)
        {
            try
            {
                using (LaCriollitaEntities db = new LaCriollitaEntities())
                {
                    var platillo = db.Platillos.FirstOrDefault(p => p.Nombre == nombre);

                    if (platillo != null)
                    {
                        platillo.idEstado = nuevoEstado;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public int ObtenerIdPlatilloPorNombre(string nombre)
        {
            using (LaCriollitaEntities db = new LaCriollitaEntities())
            {
                var platillo = db.Platillos.FirstOrDefault(p => p.Nombre == nombre);

                if (platillo != null)
                {
                    return platillo.idPlatillo;
                }


            }

            // Si no se encuentra el platillo, puedes devolver un valor predeterminado, como -1 o 0, según tu elección.
            return -1;
        }


        public void EliminarPlatillo(string nombre)
        {
            try
            {
                using (LaCriollitaEntities db = new LaCriollitaEntities())
                {
                    var platillo = db.Platillos.FirstOrDefault(p => p.Nombre == nombre);

                    if (platillo != null)
                    {
                        db.Platillos.Remove(platillo);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        //private DataTable ConvertirListaToDataTable(IList data)
        //{

        //    var properties = TypeDescriptor.GetProperties(typeof(Platillos));

        //    DataTable table = new DataTable();


        //    foreach (PropertyDescriptor prop in properties)
        //    {
        //        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    }

        //    foreach (Platillos item in data)
        //    {
        //        DataRow row = table.NewRow();
        //        foreach (PropertyDescriptor prop in properties) row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
        //        table.Rows.Add(row);
        //    }
        //    return table;
        //}


    }
}
