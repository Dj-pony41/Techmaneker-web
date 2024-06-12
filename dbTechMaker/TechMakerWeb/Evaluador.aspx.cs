using dbTechMaker.Implementation;
using dbTechMaker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace TechMakerWeb
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        UsuarioImpl implUser;
        Usuario U;
        string type;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillCombo();
               
                LoadU();

                // Deshabilitar el DropDownList
                ddlRole.Enabled = false;

                // Seleccionar el valor "Usuario"
                ddlRole.SelectedValue = "Usuario";
            }
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

            // Validate fields
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text) ||
                string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtLastName.Text) ||
                string.IsNullOrEmpty(txtSecondLastName.Text) || string.IsNullOrEmpty(txtMail.Text))
            {
                // Display error message
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Por favor, completa todos los campos.');", true);
                return;
            }

            // Validate email format
            if (!IsValidEmail(txtMail.Text))
            {
                // Display error message
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Por favor, introduce un correo electrónico válido.');", true);
                return;
            }


            UsuarioImpl implUser = new UsuarioImpl();
            try
            {
                byte selectedId = Convert.ToByte(Carrera.SelectedValue);
                U = new Usuario(txtUserName.Text, txtPassword.Text, txtName.Text, txtLastName.Text, txtSecondLastName.Text, txtMail.Text, ddlRole.SelectedValue, selectedId);
                try
                {
                    int n = implUser.Insert(U);

                    // Almacena la URL original del QR en una variable de sesión
                    string proyectoEncoded = Uri.EscapeDataString(txtUserName.Text);
                    string originalQRUrl = $"https://localhost:44377/Login.aspx";

                    // Redirige a la URL original del QR si el usuario está registrado
                    Response.Redirect(originalQRUrl);
                }
                catch (Exception ex)
                {
                    // Si hay un error al insertar el usuario, podrías manejarlo aquí
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                // Si hay un error al procesar los datos del formulario, podrías manejarlo aquí
                throw ex;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                id = byte.Parse(Request.QueryString["id"]);

                if (id > 0)
                {
                    byte selectedId = Convert.ToByte(Carrera.SelectedValue);

                    implUser = new UsuarioImpl();
                    Usuario u = new Usuario(id, txtName.Text, txtLastName.Text, txtSecondLastName.Text, txtMail.Text, ddlRole.SelectedValue, selectedId);
                    implUser.Update(u);
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    txtName.Text = "";
                    txtLastName.Text = "";
                    txtSecondLastName.Text = "";
                    txtMail.Text = "";

                    Response.Redirect("Usuario.aspx");
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
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
                        
                        Response.Redirect("Usuario.aspx");
                    }
                    else
                    {
                        Response.Redirect("Usuario.aspx");
                    }
                }
                else if (type == "U")
                {
                    Get();
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
        void Get()
        {
            U = null;
            id = byte.Parse(Request.QueryString["id"]);
            try
            {
                if (id > 0)
                {
                    implUser = new UsuarioImpl();
                    U = implUser.Get(id);
                    if (U != null)
                    {
                        txtUserName.Text = U.UserName.ToString();
                        txtName.Text = U.Name.ToString();
                        txtLastName.Text = U.LastName.ToString();
                        txtSecondLastName.Text = U.SecondLastName.ToString();
                        txtMail.Text = U.Mail.ToString();
                        ddlRole.SelectedItem.Text = U.Role.ToString();
                        Carrera.SelectedValue = U.CarreraID.ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
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
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}