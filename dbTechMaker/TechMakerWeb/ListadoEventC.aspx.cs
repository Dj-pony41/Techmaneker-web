using dbTechMaker.Implementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechMakerWeb
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        EventoImpl eventoImpl;
        protected void Page_Load(object sender, EventArgs e)
        {
            Evento();
        }
        public void Evento()
        {
            try
            {
                eventoImpl = new EventoImpl();
                DataTable dt = eventoImpl.Eventos();
                DataTable table = new DataTable("Evento");
                table.Columns.Add("Evento", typeof(string));
                table.Columns.Add(" ", typeof(string));
                foreach (DataRow dr in dt.Rows)
                {
                    table.Rows.Add(dr[1].ToString());
                }
                gridData.DataSource = table;
                gridData.DataBind();
                for (int i = 0; i < gridData.Rows.Count; i++)
                {
                    string id = dt.Rows[i][0].ToString();
                    string detalleNota = " <a class='btn btn-secondary' href='Calificacion.aspx?id=" + id + "'> Ver Notas </a> ";
                    
                    gridData.Rows[i].Cells[1].Text = detalleNota;
                    gridData.Rows[i].Attributes["data-id"] = id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}