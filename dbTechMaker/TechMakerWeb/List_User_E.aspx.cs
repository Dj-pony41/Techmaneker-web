using System;
using System.Collections.Generic;
using dbTechMaker.Implementation;
using dbTechMaker.Model;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;

namespace TechMakerWeb
{
    public partial class List_User_E : System.Web.UI.Page
    {
        private UsuarioImpl usuarioImpl;
        private EvaluatorImpl evaluatorImpl;
        private string type;
        private short id;
        private Usuario U;
        private Evaluador E;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                load();
                usuarioImpl = new UsuarioImpl();
                DataTable dt = usuarioImpl.UsuariosE();

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
                    else if (col.ColumnName == "Estado")
                    {
                        string estado = (string)(row[col.ColumnName]);

                        if (estado == "Usuario")
                        {
                            td.InnerHtml = "<p class=\"status pending\">Docente</p>";
                        }
                        else if (estado == "Evaluador")
                        {
                            td.InnerHtml = "<p class=\"status delivered\">Evaludador</p>";
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
                    else if (col.ColumnName == "boton")
                    {
                        string boton = (string)(row[col.ColumnName]);
                        HtmlGenericControl tdButton = new HtmlGenericControl("td");
                        HtmlAnchor buttonEstado = new HtmlAnchor();

                        if (boton == "Usuario")
                        {
                            buttonEstado.InnerHtml = "Volver Evaluador";
                            buttonEstado.HRef = $"List_User_E.aspx?id={row["ID"]}&type=V";
                            buttonEstado.Attributes["class"] = "btnAprobar";
                        }
                        else if (boton == "Evaluador")
                        {
                            buttonEstado.InnerHtml = "Quitar Evaluador";
                            buttonEstado.HRef = $"List_User_E.aspx?id={row["ID"]}&type=Q";
                            buttonEstado.Attributes["class"] = "btnRechazar";
                        }
                        tdButton.Controls.Add(buttonEstado);
                        tr.Controls.Add(tdButton);
                    }
                    else if (col.ColumnName == "startDate" || col.ColumnName == "endDate")
                    {
                        td.InnerText = Convert.ToDateTime(row[col.ColumnName]).ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    {
                        td.InnerText = row[col.ColumnName].ToString();
                    }
                   

                    tr.Controls.Add(td);
                }



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
               
                if (type == "V")
                {
                    E = null;
                    id = short.Parse(Request.QueryString["id"]);
                    evaluatorImpl = new EvaluatorImpl();
                    E = evaluatorImpl.existEvaluador(id);
                    if (E != null)
                    {
                        Volver_Evaluador2();
                    }
                    else
                    {
                        Volver_Evaluador();
                    }

                }
                else if (type == "Q")
                {
                    Quitar_Evaluador();
                }



                }
            catch (Exception)
            {

                throw;
            }
        }

        private void Volver_Evaluador2()
        {
            id = short.Parse(Request.QueryString["id"]);
            type = "VQ";
            if (id > 0)
            {
                try
                {
                    usuarioImpl = new UsuarioImpl();
                    U = usuarioImpl.Get(id);
                    if (U != null)
                    {
                        int n = usuarioImpl.Evaluador(U, type);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void Volver_Evaluador()
        {
            id = short.Parse(Request.QueryString["id"]);
            type = Request.QueryString["type"];
            if (id > 0)
            {
                try
                {
                    usuarioImpl = new UsuarioImpl();
                    U = usuarioImpl.Get(id);
                    if (U != null)
                    {
                        int n = usuarioImpl.Evaluador(U, type);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void Quitar_Evaluador()
        {
            id = short.Parse(Request.QueryString["id"]);
            type = Request.QueryString["type"];
            if (id > 0)
            {
                try
                {
                    usuarioImpl = new UsuarioImpl();
                    U = usuarioImpl.Get(id);
                    if (U != null)
                    {
                        int n = usuarioImpl.Evaluador(U, type);
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