using System;
using dbTechMaker.Implementation;
using dbTechMaker.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TechMakerWeb
{
    public partial class Listado_eventos : System.Web.UI.Page
    {
        private EventoImpl eventoImpl;
        private MetricasImpl metricasImpl;
        private string type;
        private short id;
        private Evento N;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                load();
                eventoImpl = new EventoImpl();
                DataTable dt = eventoImpl.Select();
                
                GenerarCuerpoTablaDinamica(dt);
            }
        }

        private void GenerarCuerpoTablaDinamica(DataTable dt)
        {
            HtmlGenericControl tbody = new HtmlGenericControl("tbody");
            int contador = 1;

            foreach (DataRow row in dt.Rows)
            {
                HtmlGenericControl tr = new HtmlGenericControl("tr");

                int evento_id = 0;
                foreach (DataColumn col in dt.Columns)
                {
                    HtmlGenericControl td = new HtmlGenericControl("td");

                    if (col.ColumnName == "id")
                    {
                        evento_id = Convert.ToInt32(row[col.ColumnName]); // Obtener el evento_id
                        td.InnerText = contador.ToString();
                    }
                    else if (col.ColumnName == "approvalStatus")
                    {
                        string estado = (string)(row[col.ColumnName]);

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
                    else if (col.ColumnName == "description")
                    {
                        // Crear el botón "Leer descripción"
                        HtmlGenericControl button = new HtmlGenericControl("button");
                        button.Attributes["class"] = "btn-class-style btn-leer";
                        button.InnerText = "Leer descripción";

                        // Crear el párrafo oculto con la descripción
                        HtmlGenericControl p = new HtmlGenericControl("p");
                        p.Attributes["class"] = "descripcion";
                        p.InnerText = row[col.ColumnName].ToString();

                        // Añadir el botón y el párrafo al td
                        td.Controls.Add(button);
                        td.Controls.Add(p);
                    }
                    else if (col.ColumnName == "startDate" || col.ColumnName == "endDate")
                    {
                        td.InnerText = Convert.ToDateTime(row[col.ColumnName]).ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    {
                        td.InnerText = row[col.ColumnName].ToString();
                    }
                    if (col.ColumnName == "qrCodeDir")
                    {

                    }
                    else
                    {
                        tr.Controls.Add(td);
                    }
                }

                // Crear la columna de métricas
                HtmlGenericControl tdMetricas = new HtmlGenericControl("td");
                HtmlGenericControl buttonMetricas = new HtmlGenericControl("button");
                buttonMetricas.Attributes["class"] = "btn-class-style btn-leer-metricas";
                buttonMetricas.InnerText = "Ver Metricas";

                // Añadir el botón al td
                tdMetricas.Controls.Add(buttonMetricas);

                // Obtener las métricas para el evento actual
                metricasImpl = new MetricasImpl();
                DataTable dtm = metricasImpl.Select_all_metrics_event(evento_id);

                // Añadir cada métrica como un párrafo
                foreach (DataRow metricRow in dtm.Rows)
                {
                    HtmlGenericControl pMetrica = new HtmlGenericControl("p");
                    pMetrica.Attributes["class"] = "metrica-texto";
                    pMetrica.InnerText = metricRow[0].ToString(); // Asumiendo que la métrica está en la primera columna
                    tdMetricas.Controls.Add(pMetrica);
                }

                tr.Controls.Add(tdMetricas);

                // Crear nuevo botón con imagen
                HtmlGenericControl tdImagen = new HtmlGenericControl("td");
                HtmlGenericControl buttonImagen = new HtmlGenericControl("button");
                buttonImagen.Attributes["class"] = "btn-class-style btn-imagen";
                buttonImagen.InnerText = "Mostrar Imagen";


                string imageUrl = row["qrCodeDir"].ToString();
                string idQr = row["eventName"].ToString() + " (" + Convert.ToDateTime(row["startDate"]).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(row["endDate"]).ToString("dd/MM/yyyy") + ")";

                HtmlGenericControl img = new HtmlGenericControl("img");
                img.Attributes["class"] = $"imagen-oculta";
                img.Attributes["src"] = imageUrl;
                img.Attributes["alt"] = "Imagen del evento";
                img.Attributes["id"] = idQr;

                // Añadir el botón y la imagen al td
                tdImagen.Controls.Add(buttonImagen);
                tdImagen.Controls.Add(img);

                tr.Controls.Add(tdImagen);

                HtmlGenericControl tdMasInformacion = new HtmlGenericControl("td");
                HtmlAnchor linkMasInformacion = new HtmlAnchor();
                linkMasInformacion.InnerHtml = "Editar";
                linkMasInformacion.Attributes["class"] = "btnAprobar";
                linkMasInformacion.HRef = $"Crud_Eventos.aspx?id={row["ID"]}&type=U";
                tdMasInformacion.Controls.Add(linkMasInformacion);
                tr.Controls.Add(tdMasInformacion);

                HtmlGenericControl tdRechazar = new HtmlGenericControl("td");
                HtmlAnchor LinkRechazar = new HtmlAnchor();
                LinkRechazar.InnerHtml = "Eliminar";
                LinkRechazar.Attributes["class"] = "btnRechazar";
                LinkRechazar.HRef = $"Listado_eventos.aspx?id={row["ID"]}&type=C";
                tdRechazar.Controls.Add(LinkRechazar);
                tr.Controls.Add(tdRechazar);

                tbody.Controls.Add(tr);
                contador++;
            }

            tableBody.Controls.Add(tbody);
        }

      

        private void eliminar()
        {
            id = short.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                try
                {
                    eventoImpl = new EventoImpl();
                    N = eventoImpl.Get(id);
                    if (N != null)
                    {
                        int n = eventoImpl.Delete(N);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void load()
        {
            try
            {
                type = Request.QueryString["type"];

                if (type == "C")
                {
                    DialogResult resultado = MessageBox.Show("¿Estás seguro de continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        eliminar();
                    }
                    else
                    {
                        Response.Redirect("Listado_eventos.aspx");
                    }
                }
                else if (type == "U")
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}