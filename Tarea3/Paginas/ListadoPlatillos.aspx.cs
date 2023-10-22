using Negocios;
using System;
using System.Web.UI;

namespace TareaCorta5PrograV.Paginas
{
    public partial class ListadoPlatillos : System.Web.UI.Page
    {

        PlatillosNeg ob_Negocios = new PlatillosNeg();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                cargarDataGrid();
            }

            if (IsPostBack)
            {
                string platilloAEliminar = Session["PlatilloAEliminar"] as string;

                if (!string.IsNullOrEmpty(platilloAEliminar))
                {
                    // Si la variable de sesión contiene un valor, entonces verifica si se hizo un postback para evitar que se borre 
                    if (!string.IsNullOrEmpty(Request["__EVENTTARGET"]) && Request["__EVENTTARGET"].Equals("btnEliminar"))
                    {
                        ob_Negocios.EliminarPlatillo(platilloAEliminar);
                        cargarDataGrid();
                        Datos.SelectedIndex = -1;
                    }

                    Session.Remove("PlatilloAEliminar");
                }
            }

        }

        private void cargarDataGrid()
        {
            try
            {
                Datos.DataSource = null;
                Datos.DataBind();

                Datos.DataSource = ob_Negocios.VerPlatillos();
                Datos.DataBind();
            }
            catch (Exception ex)
            {


            }
        }

        protected void TxtAdmCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdmDeCategorias.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

            if (Datos.SelectedRow != null)
            {
                string nombre = Datos.SelectedRow.Cells[0].Text;

                Session["PlatilloAEliminar"] = nombre;

                string script = "if (confirm('¿Desea  eliminar  el  platillo seleccionado?')) { __doPostBack('btnEliminar', ''); }";
                ScriptManager.RegisterStartupScript(this, GetType(), "confirmacion", script, true);
            }
            else
            {
                string script = "alert('Por favor seleccione el platillo');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
            }
        }

        protected void btnInactivar_Click1(object sender, EventArgs e)
        {
            if (Datos.SelectedRow != null)
            {
                string nombre = Datos.SelectedRow.Cells[0].Text;


                ob_Negocios.ModificarEstadoPlatillo(nombre, 2);

                cargarDataGrid();

                Datos.SelectedIndex = -1;
            }
            else
            {
                string mensaje = "Por favor seleccione el platillo";
                string script = "<script type=\"text/javascript\">alert('" + mensaje + "');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

            }

        }
        protected void btnActivar_Click(object sender, EventArgs e)
        {
            if (Datos.SelectedRow != null)
            {
                string nombre = Datos.SelectedRow.Cells[0].Text;

                ob_Negocios.ModificarEstadoPlatillo(nombre, 1);

                cargarDataGrid();

                Datos.SelectedIndex = -1;
            }
            else
            {
                string mensaje = "Por favor seleccione el platillo";
                string script = "<script type=\"text/javascript\">alert('" + mensaje + "');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

            }

        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrearPlatillo.aspx");

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {


            if (Datos.SelectedRow != null)
            {
                string nombre = Datos.SelectedRow.Cells[0].Text;

                Session["EditarPlatillo"] = nombre;


                Response.Redirect("EditarPlatillo.aspx");
            }
            else
            {

                string script = "alert('Por favor seleccione el platillo');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
            }


        }

        protected void Datos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}