using dbTechMaker.Implementation;
using dbTechMaker.Model;
using System;
using System.Web.UI;

namespace TechMakerWeb
{
    public partial class Observation : Page
    {
        Listado_proyectoImpl implProyecto;
        Listado_proyecto proyecto;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    ViewState["id"] = Request.QueryString["id"];
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Redirigir a la otra página, por ejemplo, Listado_proyecto.aspx
            Response.Redirect("Listado_proyecto.aspx");
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["id"] != null)
            {
                int projectId = int.Parse(ViewState["id"].ToString());
                string observation = txtObservation.Text;

                // Instanciar Listado_proyecto y Listado_proyectoImpl
                proyecto = new Listado_proyecto { id = projectId, Observation = observation };
                implProyecto = new Listado_proyectoImpl();

                // Llamar al método UpdateStatus para rechazar el proyecto y agregar la observación
                implProyecto.UpdateStatus(proyecto, "R", observation);

                // Redirigir de vuelta a la página de listado (o a cualquier otra página de tu elección)
                Response.Redirect("Listado_proyecto.aspx");
            }
        }
    }
}
