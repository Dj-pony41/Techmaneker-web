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
        private ProyectoImpl proyectImpl;
        private string type;
        private short id;
        private Proyecto N;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                load();
                proyectImpl = new ProyectoImpl();
                DataTable dt = proyectImpl.Select();
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
                                td.InnerText = Convert.ToDateTime(row[col.ColumnName]).ToString("dd/mm/yyyy");
                            }
                            else
                            {
                                td.InnerText = row[col.ColumnName].ToString();
                            }                  
                        }
                    }

                    tr.Controls.Add(td);
                }
                HtmlGenericControl tdMasInformacion = new HtmlGenericControl("td");
                HtmlAnchor linkMasInformacion = new HtmlAnchor();
                linkMasInformacion.InnerHtml = "Aprobar";
                linkMasInformacion.Attributes["class"] = "btnAprobar";
                linkMasInformacion.HRef = $"Listado_proyectos.aspx?id={row["ID"]}&type=A";
                tdMasInformacion.Controls.Add(linkMasInformacion);
                tr.Controls.Add(tdMasInformacion);

                HtmlGenericControl tdRechazar = new HtmlGenericControl("td");
                HtmlAnchor LinkRechazar = new HtmlAnchor();
                LinkRechazar.InnerHtml = "Rechazar";
                LinkRechazar.Attributes["class"] = "btnRechazar";
                LinkRechazar.HRef = $"Listado_proyectos.aspx?id={row["ID"]}&type=R";
                tdRechazar.Controls.Add(LinkRechazar);
                tr.Controls.Add(tdRechazar);


                tbody.Controls.Add(tr);

                contador++;
            }

            tableBody.Controls.Add(tbody);
        }

        private void Aceptado()
        {
            id = short.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                try
                {
                    proyectImpl = new ProyectoImpl();
                    N = proyectImpl.Get(id);
                    if (N != null)
                    {
                        int n = proyectImpl.UpdateStatus(N, type);
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
            if (id > 0)
            {
                try
                {
                    proyectImpl = new ProyectoImpl();
                    N = proyectImpl.Get(id);
                    if (N != null)
                    {
                        int n = proyectImpl.UpdateStatus(N, type);
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