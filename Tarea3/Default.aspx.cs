using Negocios;
using System;
using System.Web.UI;

namespace TareaCorta5PrograV
{
    public partial class _Default : Page
    {
        PlatillosNeg ob_Negocios = new PlatillosNeg();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDataGrid();
            }

        }

        private void cargarDataGrid()
        {
            try
            {
                Dato.DataSource = null;
                Dato.DataBind();

                Dato.DataSource = ob_Negocios.VerPlatillosActivos();
                Dato.DataBind();
            }
            catch (Exception ex)
            {


            }
        }
    }
}