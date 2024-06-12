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
    public partial class WebForm4 : System.Web.UI.Page
    {
        ProyectoImpl implProyecto;
        Proyecto proyecto;
        byte id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillCombo();
                LoadProyecto();
            }
        }

        protected void btnRegistrarProyecto_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica si hay un evento seleccionado
                if (Evento.SelectedValue != null)
                {
                    int selectedEventId = Convert.ToInt32(Evento.SelectedValue); // Obtén el valor del evento seleccionado

                    proyecto = new Proyecto(txtNombreProyecto.Text, txtDescripcion.Text, Session_Class.Session_Career, selectedEventId, Session_Class.Session_ID); // Asegúrate de que el constructor de Proyecto acepte el ID del evento
                    implProyecto = new ProyectoImpl();
                    int n = implProyecto.Insert(proyecto);
                    Response.Redirect("Listado_Proyectos_Propuestos.aspx");

                }
                else
                {
                    // Manejar el caso donde no se ha seleccionado ningún evento
                    Response.Write("Por favor, selecciona un evento.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones más específico y amigable
                Response.Write($"Ocurrió un error al registrar el proyecto: {ex.Message}");
            }
        }


        protected void btnActualizarProyecto_Click(object sender, EventArgs e)
        {
            try
            {
                id = byte.Parse(Request.QueryString["id"]);

                if (id > 0)
                {
                    implProyecto = new ProyectoImpl();
                    Proyecto p = new Proyecto(id, txtNombreProyecto.Text, txtDescripcion.Text);
                    implProyecto.Update(p);
                    txtNombreProyecto.Text = "";
                    txtDescripcion.Text = "";

                    Response.Redirect("Listado_Proyectos_Propuestos.aspx");

                }
            }
            catch (Exception)
            {
                throw;
            }
        }




        void LoadProyecto()
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
                        Response.Redirect("Proyecto.aspx");
                    }
                    else
                    {
                        Response.Redirect("Proyecto.aspx");
                    }
                }
                else if (type == "U")
                {
                    btnActualizarProyecto.Visible = true;
                    btnRegistrarProyecto.Visible= false;
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
            id = byte.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                try
                {
                    implProyecto = new ProyectoImpl();
                    int n = implProyecto.Delete(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        void fillCombo()
        {
            if (!IsPostBack)
            {
                try
                {
                    implProyecto = new ProyectoImpl();
                    DataTable combo = implProyecto.ComboBoxEvento();
                    Evento.DataSource = combo;
                    Evento.DataTextField = "eventName";
                    Evento.DataValueField = "id";
                    Evento.DataBind();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        void Get()
        {
            proyecto = null;
            id = byte.Parse(Request.QueryString["id"]);
            try
            {
                if (id > 0)
                {
                    implProyecto = new ProyectoImpl();
                    proyecto = implProyecto.Get(id);
                    if (proyecto != null)
                    {
                        txtNombreProyecto.Text = proyecto.ProyectName.ToString();
                        txtDescripcion.Text = proyecto.Description.ToString();
                        Evento.SelectedValue = proyecto.Eventid.ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
