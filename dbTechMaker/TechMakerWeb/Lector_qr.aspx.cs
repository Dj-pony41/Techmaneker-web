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
    public partial class Lector_qr : System.Web.UI.Page
    {
        private string type;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                type = Request.QueryString["type"];
                ActualizarDatosEvaluador(type);
            }

            
        }
        private void ActualizarDatosEvaluador(string type)
        {
            if (type == "E")
            {
                title_qr.InnerText = $"Escanee el qr del evento";
            }
            else if (type == "P")
            {
                title_qr.InnerText = $"Escanee el qr del proyecto";
            }
            
            
        }
    }
}