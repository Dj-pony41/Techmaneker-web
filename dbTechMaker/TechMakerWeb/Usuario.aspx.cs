using dbTechMaker.Implementation;
using dbTechMaker.Model;
using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TechMakerWeb
{
    public partial class WebForm1 : Page
    {
        UsuarioImpl implUser;
        Usuario U;
        string usernameO;
        string password;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillCombo();
                LoadU();
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string secondLastName = txtSecondLastName.Text.Trim();
            string mail = txtMail.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(secondLastName) || string.IsNullOrEmpty(mail) || !IsValidEmail(mail))
            {
                // Display error message (you can use a Label control to display the error message)
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Todos los campos son obligatorios y el correo debe ser válido.');", true);
                return;
            }

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(secondLastName))
            {
                string initials = $"{name[0]}{lastName[0]}{secondLastName[0]}".ToLower();
                string randomNumbers = new Random().Next(100000, 999999).ToString();
                usernameO = $"{initials}{randomNumbers}";
            }

            password = new Random().Next(100000, 999999).ToString();

            try
            {
                byte selectedId = Convert.ToByte(Carrera.SelectedValue);
                U = new Usuario(usernameO, password, name, lastName, secondLastName, mail, ddlRole.SelectedValue, selectedId);

                try
                {
                    implUser = new UsuarioImpl();
                    int n = implUser.Insert(U);
                    Send(usernameO, password, mail);
                    Response.Redirect("Listado_Usuarios.aspx");
                }
                catch (Exception ex)
                {
                    // Handle exception (you may want to log the error and show a user-friendly message)
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                if (id > 0)
                {
                    byte selectedId = Convert.ToByte(Carrera.SelectedValue);
                    Usuario u = new Usuario(id, txtName.Text.Trim(), txtLastName.Text.Trim(), txtSecondLastName.Text.Trim(), txtMail.Text.Trim(), ddlRole.SelectedValue, selectedId);
                    implUser = new UsuarioImpl();
                    implUser.Update(u);

                    Response.Redirect("Listado_Usuarios.aspx");
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }

        void LoadU()
        {
            try
            {
                string type = Request.QueryString["type"];
                if (type == "D")
                {
                    DialogResult resultado = MessageBox.Show("¿Estás seguro de continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        Delete();
                        Response.Redirect("Usuario.aspx");
                    }
                    else
                    {
                        Response.Redirect("Usuario.aspx");
                    }
                }
                else if (type == "U")
                {
                    btnRegistrar.Visible = false;
                    btnUpdate.Visible = true;
                    Get();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }

        void Delete()
        {
            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                if (id > 0)
                {
                    implUser = new UsuarioImpl();
                    implUser.Delete(id);
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }

        void Get()
        {
            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                if (id > 0)
                {
                    implUser = new UsuarioImpl();
                    U = implUser.Get(id);
                    if (U != null)
                    {
                        txtName.Text = U.Name;
                        txtLastName.Text = U.LastName;
                        txtSecondLastName.Text = U.SecondLastName;
                        txtMail.Text = U.Mail;
                        ddlRole.SelectedValue = U.Role;
                        Carrera.SelectedValue = U.CarreraID.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }

        void fillCombo()
        {
            if (!IsPostBack)
            {
                try
                {
                    implUser = new UsuarioImpl();
                    DataTable combo = implUser.ComboBoxCarrera();
                    Carrera.DataSource = combo;
                    Carrera.DataTextField = "Name";
                    Carrera.DataValueField = "id";
                    Carrera.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle exception
                    throw ex;
                }
            }
        }

        public string Send(string user, string password, string email)
        {
            try
            {
                var person = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("colorinshopcontacto@gmail.com", "vapqpysxkwexunrc")
                };

                var emailS = new MailMessage("colorinshopcontacto@gmail.com", email)
                {
                    Subject = "Asunto: " + DateTime.Now.ToString(),
                    Body = "Usuario creado exitosamente\nUsuario: " + user + "\nContraseña: " + password
                };

                person.Send(emailS);
                return "Correo enviado exitosamente.";
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
