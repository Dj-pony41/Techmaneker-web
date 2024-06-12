using dbTechMaker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechMakerWeb
{
    public partial class DocMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            // Limpiar las variables de sesión
            Session_Class.Session_Role = null;
            Session_Class.Session_Career = 0;
            Session_Class.Session_ID = 0;

            // Abandonar la sesión
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

            // Redirigir a la página de login
            Response.Redirect("Login.aspx");
        }
    }
}