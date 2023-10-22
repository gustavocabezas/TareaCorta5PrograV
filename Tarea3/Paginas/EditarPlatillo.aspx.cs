using Datos;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace TareaCorta5PrograV.Paginas
{
    public partial class EditarPlatillo : System.Web.UI.Page
    {
        private string nombreAnterior;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategoriasEnListBox();
                CargarEstadosEnListBox();

                if (Session["EditarPlatillo"] != null)
                {
                    var platillo = Session["EditarPlatillo"];
                    using (LaCriollitaEntities cargarDatos = new LaCriollitaEntities())
                    {
                        var platillos = (from p in cargarDatos.Platillos
                                         where p.Nombre == platillo.ToString()
                                         select new
                                         {
                                             Nombre = p.Nombre,
                                             Costo = p.Costo.ToString(),
                                             Estado = p.Estados.Nombre,
                                             Categoria = p.Categorias.Nombre
                                         }).FirstOrDefault();

                        txtNombrePlatillo.Text = platillos.Nombre.ToString();
                        txtCostoPlatillo.Text = platillos.Costo.ToString();
                        ddlEstados.SelectedValue = platillos.Estado.ToString();
                        ddlCategorias.SelectedValue = platillos.Categoria.ToString();

                        nombreAnterior = platillos.Nombre.ToString();

                        Session["NombreAnterior"] = nombreAnterior;
                    }
                }
            }
            else
            {
                nombreAnterior = Session["NombreAnterior"] as string;
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

        private void CargarEstadosEnListBox()
        {
            using (LaCriollitaEntities estado = new LaCriollitaEntities())
            {
                var estados = estado.Estados.Select(e => e.Nombre).ToList();
                ddlEstados.DataSource = estados;
                ddlEstados.DataBind();
            }

        }



        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        protected void btnAceptar_Click1(object sender, EventArgs e)
        {
            string nuevoNombre = txtNombrePlatillo.Text;
            string nuevoEstado = ddlEstados.SelectedValue;
            string nuevaCategoria = ddlCategorias.SelectedValue;
            decimal nuevoCosto;

            int nuevoEstadoId = 0;
            int nuevaCategoriaId = 0;

            //ssssssss
            if (string.IsNullOrWhiteSpace(txtNombrePlatillo.Text))
            {
                lblMensajeNombre.Text = "El nombre del platillo es requerido.";
                return;
            }
            else if (!decimal.TryParse(txtCostoPlatillo.Text, out nuevoCosto) || nuevoCosto < 0 || !EsNumeroConMaximoDosDecimales(nuevoCosto))
            {
                lblMensajeCosto.Text = "El costo debe ser un valor numérico con máximo 2 decimales.";
                return;
            }

            using (LaCriollitaEntities context = new LaCriollitaEntities())
            {
                string nombrePlatilloMinusculas = nombreAnterior.ToLower();
                bool nombrePlatilloExiste = context.Platillos.Any(p => p.Nombre.ToLower() == nombrePlatilloMinusculas);

                if (nombrePlatilloExiste)
                {

                    var Categorias = (from c in context.Categorias
                                      where c.Nombre == nuevaCategoria
                                      select new
                                      {
                                          id = c.idCategoria

                                      }).FirstOrDefault();
                    nuevaCategoriaId = Categorias.id;


                    var Estados = (from est in context.Estados
                                   where est.Nombre == nuevoEstado
                                   select new
                                   {
                                       id = est.idEstado

                                   }).FirstOrDefault();
                    nuevoEstadoId = Estados.id;
                }

            }

            using (LaCriollitaEntities context = new LaCriollitaEntities())
            {
                string nombrePlatilloMinusculas = nombreAnterior.ToLower();
                var platilloExistente = context.Platillos.FirstOrDefault(p => p.Nombre.ToLower() == nombrePlatilloMinusculas);
                if (platilloExistente != null)
                {
                    if (platilloExistente != null)
                    {
                        platilloExistente.Nombre = nuevoNombre;
                        platilloExistente.idCategoria = nuevaCategoriaId;
                        platilloExistente.idEstado = nuevoEstadoId;
                        platilloExistente.Costo = nuevoCosto;

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}


