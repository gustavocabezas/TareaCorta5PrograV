using Negocios;
using System;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace TareaCorta5PrograV.Paginas
{
    public partial class AdmDeCategorias : System.Web.UI.Page
    {
        CategoriasNeg ob_Negocios = new CategoriasNeg();
        private string _idCategoria = null;



        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDataGrid();
        }



        private async void cargarDataGrid()
        {
            try
            {
                Datos.DataSource = null;
                Datos.DataBind();
                Datos.DataSource = await ob_Negocios.VerCategoria();
                Datos.DataBind();
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message);
                Debugger.Break();
#endif 
            }
        }



        protected void btnCancelar_Click(object sender, EventArgs e) { hiddenSelectedName.Value = ""; }
         
        protected async void BtnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreCategoria = TextBoxCategoriaCrear.Text;
                if (string.IsNullOrEmpty(nombreCategoria) || string.IsNullOrWhiteSpace(nombreCategoria))
                {
                    string mensaje = "El nombre no puede ser vacío";
                    string script = "<script type=\"text/javascript\">alert('" + mensaje + "');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }
                else
                {
                    bool categoriaExiste = await ob_Negocios.GuardarCategoria(nombreCategoria);
                    if (categoriaExiste)
                    {
                        string mensaje = "La categoría ya existe.";
                        string script = "<script type=\"text/javascript\">alert('" + mensaje + "');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    }


                    hiddenSelectedName.Value = "";
                    cargarDataGrid();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message);
                Debugger.Break();
#endif 
            }
        }



        protected async void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreCategoria = TextBoxEditarCategoria.Text;
                if (string.IsNullOrEmpty(nombreCategoria) || string.IsNullOrWhiteSpace(nombreCategoria))
                {
                    string mensaje = "El nombre no puede ser vacío";
                    string script = "<script type=\"text/javascript\">alert('" + mensaje + "');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }
                else
                {
                    bool exito = await ob_Negocios.EditarCategoria(hiddenSelectedName.Value, nombreCategoria);
                    if (!exito)
                    {
                        string mensaje = "No se pudo editar la categoría. Puede ser porque ya existe una categoría con ese nombre.";
                        string script = "<script type=\"text/javascript\">alert('" + mensaje + "');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    }
                    else
                    {
                        string mensaje = "Se edito la categoría.";
                        string script = "<script type=\"text/javascript\">alert('" + mensaje + "');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    }
                }

                hiddenSelectedName.Value = "";
                cargarDataGrid();
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message);
                Debugger.Break();
#endif
            }
        }



        protected async void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                bool exito = await ob_Negocios.EliminarCategoria(hiddenSelectedName.Value);
                if (!exito)
                {
                    string mensaje = "No se pudo eliminar la categoría.";
                    string script = "<script type=\"text/javascript\">alert('" + mensaje + "');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }
                else
                {
                    string mensaje = "Se elimino la categoría.";
                    string script = "<script type=\"text/javascript\">alert('" + mensaje + "');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }



                cargarDataGrid();
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message);
                Debugger.Break();
#endif 
            }
            finally
            {
                hiddenSelectedName.Value = "";
            }
        }



        protected void UsersGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button detailsButton = (Button)e.Row.FindControl("DetailsButton");
                string id = Datos.DataKeys[e.Row.RowIndex].Value.ToString();



                detailsButton.PostBackUrl = $"UserDetails.aspx?id={id}";
            }
        }
         
    }
}