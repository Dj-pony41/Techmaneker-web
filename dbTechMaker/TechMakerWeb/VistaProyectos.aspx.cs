using dbTechMaker.Implementation;
using dbTechMaker.Model;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TechMakerWeb
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        private Listado_proyectoImpl proyectoImpl;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializa la instancia de Listado_ProyectoImpl
            proyectoImpl = new Listado_proyectoImpl();

            if (!IsPostBack)
            {
                DataTable dt = proyectoImpl.ComboBoxEvento();

                Evento.DataSource = dt;
                Evento.DataTextField = "eventName";
                Evento.DataValueField = "id";
                Evento.DataBind();
            }
        }



        protected void Evento_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedEventId = Convert.ToInt32(Evento.SelectedValue);
            Listado_proyecto proyecto = new Listado_proyecto
            {
                eventId = selectedEventId
            };

            DataTable dt = proyectoImpl.Select3(proyecto);
            tableBody.Controls.Clear();

            int contador = 1;

            foreach (DataRow row in dt.Rows)
            {
                HtmlGenericControl tr = new HtmlGenericControl("tr");

                foreach (DataColumn col in dt.Columns)
                {
                    HtmlGenericControl td = new HtmlGenericControl("td");

                    if (col.ColumnName == "id")
                    {
                        td.InnerText = contador.ToString();
                    }
                    else if (col.ColumnName == "approvalStatus")
                    {
                        string estado = row[col.ColumnName].ToString();

                        if (estado == "Rechazado")
                        {
                            td.InnerHtml = "<p class=\"status cancelled\">Rechazado</p>";
                        }
                        else if (estado == "Aceptado")
                        {
                            td.InnerHtml = "<p class=\"status delivered\">Aceptado</p>";
                        }
                        else if (estado == "Pendiente")
                        {
                            td.InnerHtml = "<p class=\"status pending\">Pendiente</p>";
                        }
                    }
                    else if (col.ColumnName == "registerDate")
                    {
                        td.InnerText = Convert.ToDateTime(row[col.ColumnName]).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        td.InnerText = row[col.ColumnName].ToString();
                    }

                    tr.Controls.Add(td);
                }

                tableBody.Controls.Add(tr);
                contador++;
            }
        }
    }
}
