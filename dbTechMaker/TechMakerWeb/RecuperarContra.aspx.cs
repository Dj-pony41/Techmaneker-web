using dbTechMaker.Implementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TechMakerWeb
{
    public partial class RecuperarContra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnMandar_OnClick(object sender, EventArgs e)
        {
            try
            {
                UsuarioImpl implUser = new UsuarioImpl();
                DataTable table = implUser.verificarMail(txtMail.Text);
                string passtemp = GenerarContraseña();
                if (table.Rows.Count > 0)
                {
                    DataTable table2 = implUser.passTemp(txtMail.Text, passtemp, txtUserName.Text);
                    Send(passtemp, txtMail.Text);
                    lblInfo.Text = "";
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    lblInfo.Text = "No esta registrado el Email";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        static string GenerarContraseña()
        {
            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"; // Caracteres permitidos en la contraseña
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(8);

            for (int i = 0; i < 8; i++)
            {
                int index = rnd.Next(caracteresPermitidos.Length);
                sb.Append(caracteresPermitidos[index]);
            }

            return sb.ToString();
        }
        public string Send(string password, string email)
        {
            string message = "";
            try
            {
                var person = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("colorinshopcontacto@gmail.com", "vapqpysxkwexunrc")
                };
                var emailS = new MailMessage("colorinshopcontacto@gmail.com", email);

                emailS.Subject = "Asunto: " + DateTime.Now.ToString();
                emailS.Body = "Su contraseña temporal es: " + password;
                person.Send(emailS);

                return message;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}