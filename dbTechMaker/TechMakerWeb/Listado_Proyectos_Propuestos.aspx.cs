using dbTechMaker.Implementation;
using dbTechMaker.Model;
using System;
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
    public partial class Listado_Proyectos_Propuestos : System.Web.UI.Page
    {
        private ProyectoImpl proyectImpl;
        private string type;
        private short id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                load();
                proyectImpl = new ProyectoImpl();
                DataTable dt = proyectImpl.Proyectos_sugeridos(Session_Class.Session_ID);
                //DataTable dt = proyectImpl.Proyectos_sugeridos(2016); //Ojo confines de prueba nada mas
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
                    else if (col.ColumnName == "Observation")
                    {
                        string observacion = (string)(row[col.ColumnName]);
                        if (observacion == "Sin observaciones para mostrar")
                        {
                            td.InnerText = row[col.ColumnName].ToString();
                        }
                        else
                        {
                            // Crear el botón "Leer descripción"
                            HtmlGenericControl button = new HtmlGenericControl("button");
                            button.Attributes["class"] = "btn-class-style btn-leer-metricas";
                            button.InnerText = "Leer Observaciones";

                            // Crear el párrafo oculto con la descripción
                            HtmlGenericControl p = new HtmlGenericControl("p");
                            p.Attributes["class"] = "metrica-texto";
                            p.InnerText = row[col.ColumnName].ToString();

                            // Añadir el botón y el párrafo al td
                            td.Controls.Add(button);
                            td.Controls.Add(p);
                        }
                        
                    }
                    else if (col.ColumnName == "startDate")
                    {
                        td.InnerText = Convert.ToDateTime(row[col.ColumnName]).ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    {
                        td.InnerText = row[col.ColumnName].ToString();
                    }

                    tr.Controls.Add(td);
                }

                HtmlGenericControl tdMasInformacion = new HtmlGenericControl("td");
                HtmlAnchor linkMasInformacion = new HtmlAnchor();
                linkMasInformacion.InnerHtml = "Editar";
                linkMasInformacion.Attributes["class"] = "btnAprobar";
                linkMasInformacion.HRef = $"Proyecto.aspx?id={row["ID"]}&type=U";
                tdMasInformacion.Controls.Add(linkMasInformacion);
                tr.Controls.Add(tdMasInformacion);

                HtmlGenericControl tdRechazar = new HtmlGenericControl("td");
                HtmlAnchor LinkRechazar = new HtmlAnchor();
                LinkRechazar.InnerHtml = "Eliminar";
                LinkRechazar.Attributes["class"] = "btnRechazar";
                LinkRechazar.HRef = $"Listado_Proyectos_Propuestos.aspx?id={row["ID"]}&type=E";
                tdRechazar.Controls.Add(LinkRechazar);
                tr.Controls.Add(tdRechazar);

                tbody.Controls.Add(tr);
                contador++;
            }

            tableBody.Controls.Add(tbody);
        }
        private void load()
        {
            try
            {
                type = Request.QueryString["type"];
                if (type == "E")
                {
                    eliminar();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void eliminar()
        {
            id = short.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Estás seguro de continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    id = byte.Parse(Request.QueryString["id"]);
                    if (id > 0)
                    {
                        try
                        {
                            proyectImpl = new ProyectoImpl();
                            int n = proyectImpl.Delete(id);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }

            }
        }
    }
}