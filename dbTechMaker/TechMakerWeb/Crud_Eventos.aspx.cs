using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dbTechMaker.Implementation;
using dbTechMaker.Model;

namespace TechMakerWeb
{
    public partial class Crud_Eventos : System.Web.UI.Page
    {
        private EventoImpl eventoImpl;
        private string type;
        private short id;
        private Evento N;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                load();
            }
        }

        private void load()
        {
            try
            {
                type = Request.QueryString["type"];
                if (type == "U")
                {
                    Update();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async void btnEnviar_Click(object sender, EventArgs e)
        {
            
            DateTime fechaFin;
            

            if (!DateTime.TryParse(txtFechaFin.Text, out fechaFin))
            {
                // Manejar el error de formato
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Formato de fecha de fin no válido.');", true);
                return;
            }

            // Validar que las fechas no sean anteriores a la fecha y hora actuales
            DateTime now = DateTime.Now;
            
            if (fechaFin < now)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('La Fecha Fin del Evento no puede ser anterior a la fecha y hora actuales.');", true);
                return;
            }
            type = Request.QueryString["type"];

            string nombreEvento = txtNombreEvento.Text;
            string descripcionEvento = txtDescripcionEvento.Text;
            string gestionEvento = txtGestionEvento.Text;
            DateTime inicioEvento = DateTime.Parse(txtFechaInicio.Text);
            DateTime finEvento = DateTime.Parse(txtFechaFin.Text);

            
            eventoImpl = new EventoImpl();

            try
            {
                if (type == "U")
                {
                    Session_Class.Session_Event = short.Parse(Request.QueryString["id"]);
                    id = short.Parse(Request.QueryString["id"]);
                    N = eventoImpl.Get(id);
                    N.Name = nombreEvento;
                    N.Description = descripcionEvento;
                    N.Gestion = gestionEvento;
                    N.Fecha_inicio = inicioEvento;
                    N.Fecha_fin = finEvento;
                    int v = eventoImpl.Update(N);
                    if (v > 0)
                    {
                        Response.Redirect("Listado_eventos.aspx", endResponse: false);
                    }
                }
                else
                {
                    Evento evento = new Evento(nombreEvento, descripcionEvento, gestionEvento, inicioEvento, finEvento);
                    int v = await eventoImpl.InsertAsync(evento);
                    if (v > 0)
                    {
                        Response.Redirect("Crud_Metrics.aspx");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void Update()
        {
            Get();
        }

        private void Create()
        {

        }

        private void Get()
        {
            N = null;
            id = short.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                eventoImpl = new EventoImpl();
                N = eventoImpl.Get(id);
                if (N != null && !IsPostBack)
                {
                    txtNombreEvento.Text = N.Name.ToString();
                    txtDescripcionEvento.Text = N.Description.ToString();
                    txtGestionEvento.Text = N.Gestion.ToString();
                    txtFechaInicio.Text = N.Fecha_inicio.ToString("yyyy-MM-dd");
                    txtFechaFin.Text = N.Fecha_fin.ToString("yyyy-MM-dd");
                }
            }
        }
    }
}