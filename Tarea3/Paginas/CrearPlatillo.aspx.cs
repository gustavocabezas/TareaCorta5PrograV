using Datos;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace TareaCorta5PrograV.Paginas
{
    public partial class CrearPlatillo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategoriasEnListBox();
            }
        }
        private void CargarCategoriasEnListBox()
        {
            using (LaCriollitaEntities categoria = new LaCriollitaEntities())
            {
                var categorias = categoria.Categorias.Select(c => c.Nombre).ToList();

                ddlCategorias.DataSource = categorias;
                ddlCategorias.DataBind();
            }
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombrePlatillo = txtNombrePlatillo.Text;
            string categoriaPlatillo = ddlCategorias.SelectedValue;
            int estadoPlatillo = 1; //activo = 1 e inactivo = 2
            decimal costoPlatillo;
            int categoriaID = 0;


            if (string.IsNullOrWhiteSpace(txtNombrePlatillo.Text))
            {
                lblMensajeNombre.Text = "El nombre del platillo es requerido.";
                return;
            }
            else if (!decimal.TryParse(txtCostoPlatillo.Text, out costoPlatillo) || costoPlatillo < 0 || !EsNumeroConMaximoDosDecimales(costoPlatillo))
            {
                lblMensajeCosto.Text = "El costo debe ser un valor numérico con máximo 2 decimales.";
                return;
            }
            using (LaCriollitaEntities context = new LaCriollitaEntities())
            {

                string nombrePlatilloMinusculas = nombrePlatillo.ToLower();
                bool nombrePlatilloExiste = context.Platillos.Any(p => p.Nombre.ToLower() == nombrePlatilloMinusculas);

                if (nombrePlatilloExiste)
                {
                    lblMensajeNombre.Text = "El nombre del platillo ya existe. Elija otro nombre";
                    return;
                }
            }
            using (LaCriollitaEntities categoria = new LaCriollitaEntities())
            {
                var Categorias = (from c in categoria.Categorias
                                  where c.Nombre == categoriaPlatillo
                                  select new
                                  {
                                      id = c.idCategoria

                                  }).FirstOrDefault();
                categoriaID = Categorias.id;

            }


            Platillos nuevoPlatillo = new Platillos
            {
                Nombre = nombrePlatillo,
                idCategoria = categoriaID,
                idEstado = estadoPlatillo,
                Costo = costoPlatillo
            };

            using (LaCriollitaEntities platillo = new LaCriollitaEntities())
            {
                platillo.Platillos.Add(nuevoPlatillo);
                platillo.SaveChanges();



                Response.Redirect("ListadoPlatillos.aspx");
            }
        }
        private bool EsNumeroConMaximoDosDecimales(decimal numero)
        {
            decimal parteDecimal = numero - Math.Truncate(numero);



            string numeroComoString = parteDecimal.ToString();
            int cantidadDeDigitos = numeroComoString.Length;



            if (cantidadDeDigitos != 0 && cantidadDeDigitos > 4)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoPlatillos.aspx");
        }
    }
}