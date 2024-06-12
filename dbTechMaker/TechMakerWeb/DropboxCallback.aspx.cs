using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechMakerWeb
{
    public partial class DropboxCallback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string authorizationCode = Request.QueryString["code"];

            if (!string.IsNullOrEmpty(authorizationCode))
            {
                // Aquí puedes manejar el código de autorización recibido de Dropbox
                // Por ejemplo, puedes guardar el código y redirigir al usuario a otra página

                Response.Write($"Authorization code received: {authorizationCode}");
            }
            else
            {
                Response.Write("Authorization code not provided");
            }
        }
    }
}