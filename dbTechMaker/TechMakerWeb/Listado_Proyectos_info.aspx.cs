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

namespace TechMakerWeb
{
    public partial class Listado_Proyectos_info : System.Web.UI.Page
    {
        private ProyectoImpl proyectImpl;
        private string type;
        private short id;
        private Listado_proyecto N;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = short.Parse(Request.QueryString["id"]);
            proyectImpl = new ProyectoImpl();
            DataTable dt = proyectImpl.Evento_info(id);
            GenerarCuerpoTablaDinamica(dt);
        }


        private void GenerarCuerpoTablaDinamica(DataTable dt)
        {
            // Agrupar los datos por carrera
            var groupedData = dt.AsEnumerable()
                                .GroupBy(row => row["Carrera"].ToString())
                                .OrderBy(group => group.Key);

            foreach (var group in groupedData)
            {
                // Crear una tabla para cada carrera
                HtmlGenericControl table = new HtmlGenericControl("table");
                table.Attributes["class"] = "careerTable";

                // Crear el encabezado de la tabla
                HtmlGenericControl thead = new HtmlGenericControl("thead");
                HtmlGenericControl trHead = new HtmlGenericControl("tr");

                foreach (DataColumn col in dt.Columns)
                {
                    HtmlGenericControl th = new HtmlGenericControl("th");
                    th.InnerText = col.ColumnName;
                    trHead.Controls.Add(th);
                }
                thead.Controls.Add(trHead);
                table.Controls.Add(thead);

                // Crear el cuerpo de la tabla
                HtmlGenericControl tbody = new HtmlGenericControl("tbody");
                int contador = 1;

                foreach (DataRow row in group)
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
                            if (col.ColumnName == "registerDate")
                            {
                                td.InnerText = Convert.ToDateTime(row[col.ColumnName]).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                td.InnerText = row[col.ColumnName].ToString();
                            }
                            if (col.ColumnName == "Nota del proyecto")
                            {
                                int nota = (int)(row[col.ColumnName]);

                                if (nota > 100)
                                {
                                    td.InnerHtml = "<p class=\"status pending\">Sin nota</p>";
                                }
                            }
                        }

                        tr.Controls.Add(td);
                    }

                    tbody.Controls.Add(tr);
                    contador++;
                }

                table.Controls.Add(tbody);

                // Agregar el nombre de la carrera como encabezado de la sección
                HtmlGenericControl h2 = new HtmlGenericControl("h2");
                h2.InnerText = group.Key;

                // Añadir la tabla y el encabezado de la carrera al cuerpo principal
                tableBody.Controls.Add(h2);
                tableBody.Controls.Add(table);
            }
        }

    }
}