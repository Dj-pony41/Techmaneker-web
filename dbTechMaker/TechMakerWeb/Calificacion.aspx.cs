using dbTechMaker.Implementation;
using dbTechMaker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechMakerWeb
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        NotaImpl notaImpl;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            capturaId();
            Calificaciones();
        }
        void capturaId()
        {
            if (!IsPostBack)
            {
                id = int.Parse(Request.QueryString["id"]);
            }
        }
        public void Calificaciones()
        {
            try
            {
                notaImpl = new NotaImpl();
                DataTable dt = notaImpl.Calificaciones(id);
                DataTable table = new DataTable("Calificacion");
                table.Columns.Add("Proyecto", typeof(string));
                table.Columns.Add("Promedio", typeof(string));
                foreach (DataRow dr in dt.Rows)
                {
                    table.Rows.Add(dr[0].ToString(), dr[1].ToString());
                }
                gridData.DataSource = table;
                gridData.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}