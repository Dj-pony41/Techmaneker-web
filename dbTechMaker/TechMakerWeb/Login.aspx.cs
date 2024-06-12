using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dbTechMaker.Implementation;
using dbTechMaker.Model;


using System.Net.Mail;
using System.Net;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TechMakerWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
        protected void btnLogin(object sender, EventArgs e)
        {
            // Lógica para manejar el evento de clic del enlace
            try
            {
                UsuarioImpl implUser = new UsuarioImpl();
                DataTable table = implUser.Login(txtUsuario.Text, txtContrasena.Text);

                if (txtUsuario.Text != "" && txtContrasena.Text != "")
                {
                    if (table.Rows.Count > 0)
                    {
                        // Obtener el rol del usuario desde la tabla
                        string role = table.Rows[0]["role"].ToString();
                        Session_Class.Session_Role = table.Rows[0]["role"].ToString();
                        Session_Class.Session_Career = int.Parse(table.Rows[0]["idCareer"].ToString());
                        Session_Class.Session_ID = int.Parse(table.Rows[0]["id"].ToString());

                        // Redireccionar según el rol del usuario
                        switch (Session_Class.Session_Role)
                        {
                            case "Administrador":
                                Response.Redirect("Listado_eventos.aspx");
                                break;
                            case "Director":
                                Response.Redirect("Listado_proyecto.aspx");
                                break;

                            case "Usuario":
                                Response.Redirect("Listado_Proyectos_Propuestos.aspx");
                                break;

                            case "Evaluador":
                                Response.Redirect("Lector_qr.aspx?type=E");
                                break;
                            case "Invitado":
                                Response.Redirect("VistaE.aspx");
                                break;
                            default:
                                lblInfo.Text = "Rol de usuario no válido";
                                break;
                        }
                    }
                    else
                    {
                        lblInfo.Text = "Nombre de usuario y/o contraseña incorrectos";
                    }
                }
                else
                {
                    lblInfo.Text = "Complete los campos requeridos";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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