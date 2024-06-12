using dbTechMaker.Implementation;
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
    public partial class Listado_Usuarios : System.Web.UI.Page
    {
        UsuarioImpl implUser;
        string type;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadU();
                implUser = new UsuarioImpl();
                DataTable dt = implUser.Select();
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

                    tr.Controls.Add(td);
                }

                HtmlGenericControl tdMasInformacion = new HtmlGenericControl("td");
                HtmlAnchor linkMasInformacion = new HtmlAnchor();
                linkMasInformacion.InnerHtml = "Editar";
                linkMasInformacion.Attributes["class"] = "btnAprobar";
                linkMasInformacion.HRef = $"Usuario.aspx?id={row["ID"]}&type=U";
                tdMasInformacion.Controls.Add(linkMasInformacion);
                tr.Controls.Add(tdMasInformacion);

                HtmlGenericControl tdRechazar = new HtmlGenericControl("td");
                HtmlAnchor LinkRechazar = new HtmlAnchor();
                LinkRechazar.InnerHtml = "Eliminar";
                LinkRechazar.Attributes["class"] = "btnRechazar";
                LinkRechazar.HRef = $"Listado_Usuarios.aspx?id={row["ID"]}&type=D";
                tdRechazar.Controls.Add(LinkRechazar);
                tr.Controls.Add(tdRechazar);

                tbody.Controls.Add(tr);
                contador++;
            }

            tableBody.Controls.Add(tbody);
        }

        void LoadU()
        {
            try
            {
                type = Request.QueryString["type"];

                if (type == "D")
                {
                    DialogResult resultado = MessageBox.Show("¿Estás seguro de continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        Delete();
                    }
                    else
                    {
                        Response.Redirect("Listado_Usuarios.aspx");
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

        void Delete()
        {
            id = int.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                try
                {
                    implUser = new UsuarioImpl();
                    int n = implUser.Delete(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}