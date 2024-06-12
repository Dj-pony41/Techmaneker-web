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

namespace TechMakerWeb
{
    public partial class Listado_proyectos : System.Web.UI.Page
    {
        private Listado_proyectoImpl proyectImpl;
        private string type;
        private short id;
        private Listado_proyecto N;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                load();
                proyectImpl = new Listado_proyectoImpl();

                // Obtener el IDCareer de la sesión
                int careerId = Session_Class.Session_Career;

                // Crear un objeto Listado_proyecto con el IDCareer obtenido
                Listado_proyecto proyecto = new Listado_proyecto();
                proyecto.careerId = careerId;

                // Llamar al método Select proporcionando el objeto Listado_proyecto
                DataTable dt = proyectImpl.Select2(proyecto);
                GenerarCuerpoTablaDinamica(dt);
            }
        }

        private void load()
        {
            try
            {
                type = Request.QueryString["type"];
                if (type == "A")
                {
                    Aceptado();
                }
                else
                {
                    if (type == "R")
                    {
                        Rechazado();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GenerarCuerpoTablaDinamica(DataTable dt)
        {
            HtmlGenericControl tbody = new HtmlGenericControl("tbody");
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
                    else
                    {
                        if (col.ColumnName == "approvalStatus")
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
                        else
                        {
                            if (col.ColumnName == "registerDate")
                            {
                                td.InnerText = Convert.ToDateTime(row[col.ColumnName]).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                td.InnerText = row[col.ColumnName].ToString();
                            }
                        }
                    }
                    if (col.ColumnName == "qrCodeDir" || col.ColumnName == "FechaInicioEvento")
                    {

                    }
                    else
                    {
                        tr.Controls.Add(td);
                    }
                }
                string qr = (string)(row["qrCodeDir"]);
                HtmlGenericControl td2 = new HtmlGenericControl("td");

                if (qr == "S/Qr")
                {
                    td2.InnerHtml = "<p class=\"status cancelled\">Sin qr</p>";
                    tr.Controls.Add(td2);
                }
                else
                {
                    // Crear nuevo botón con imagen
                    HtmlGenericControl tdImagen = new HtmlGenericControl("td");
                    HtmlGenericControl buttonImagen = new HtmlGenericControl("button");
                    buttonImagen.Attributes["class"] = "btn-class-style btn-imagen";
                    buttonImagen.InnerText = "Mostrar Imagen";


                    string imageUrl = row["qrCodeDir"].ToString();
                    string idQr = "Qr_" + row["proyectName"].ToString();

                    HtmlGenericControl img = new HtmlGenericControl("img");
                    img.Attributes["class"] = $"imagen-oculta";
                    img.Attributes["src"] = imageUrl;
                    img.Attributes["alt"] = "Imagen del evento";
                    img.Attributes["id"] = idQr;

                    // Añadir el botón y la imagen al td
                    tdImagen.Controls.Add(buttonImagen);
                    tdImagen.Controls.Add(img);

                    tr.Controls.Add(tdImagen);
                }

                


                HtmlGenericControl tdMasInformacion = new HtmlGenericControl("td");
                HtmlAnchor linkMasInformacion = new HtmlAnchor();
                linkMasInformacion.InnerHtml = "Aprobar";
                linkMasInformacion.Attributes["class"] = "btnAprobar";
                linkMasInformacion.HRef = $"Listado_proyecto.aspx?id={row["ID"]}&type=A&event={row["eventName"]}&date={row["FechaInicioEvento"]}";
                tdMasInformacion.Controls.Add(linkMasInformacion);
                tr.Controls.Add(tdMasInformacion);

                HtmlGenericControl tdRechazar = new HtmlGenericControl("td");
                HtmlAnchor LinkRechazar = new HtmlAnchor();
                LinkRechazar.InnerHtml = "Rechazar";
                LinkRechazar.Attributes["class"] = "btnRechazar";
                LinkRechazar.HRef = $"Observation.aspx?id={row["ID"]}";
                tdRechazar.Controls.Add(LinkRechazar);
                tr.Controls.Add(tdRechazar);

                tbody.Controls.Add(tr);
                contador++;
            }

            tableBody.Controls.Add(tbody);
        }


        private async void Aceptado()
        {
            short id = short.Parse(Request.QueryString["id"]); // Obtener el id del proyecto de la consulta de la URL
            string type = "A"; // Tipo de actualización (Aceptado en este caso)
            string eventname = Request.QueryString["event"];
            string date = Request.QueryString["date"];

            if (id > 0)
            {
                try
                {
                    proyectImpl = new Listado_proyectoImpl();
                    N = proyectImpl.Get(id); // Obtener los detalles del proyecto
                    if (N != null)
                    {
                        // Llamar al método UpdateStatus con los detalles del proyecto y el tipo de actualización
                        int n = await proyectImpl.UpdateStatusAsynAsync(N, type, null, eventname, date); // Como no se proporciona ninguna observación para aceptar el proyecto, se pasa null
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }




        private void Rechazado()
        {
            id = short.Parse(Request.QueryString["id"]);
            string redirectTo = Request.QueryString["redirectTo"];
            if (id > 0)
            {
                try
                {
                    proyectImpl = new Listado_proyectoImpl();
                    N = proyectImpl.Get(id);
                    if (N != null)
                    {
                        int n = proyectImpl.UpdateStatus(N, type);
                        if (!string.IsNullOrEmpty(redirectTo))
                        {
                            Response.Redirect(redirectTo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}