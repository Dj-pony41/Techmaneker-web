using dbTechMaker.Model;
using System;
using System.Data;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using dbTechMaker.Interface;
using dbTechMaker.Implementation;
using System.Data.SqlClient;
using System.Web.DynamicData;

namespace TechMakerWeb
{
    public partial class Score2 : System.Web.UI.Page
    {
        private ScoreImpl scoreImpl;
        private NotaImpl notaImpl;
        Nota nota;
        private UsuarioImpl evaluatorImpl;
        private Usuario E;
        private int carreraId;
        private int idProyect;
        private int idSesion;
        private int idEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                E = null;
                evaluatorImpl = new UsuarioImpl();
                E = evaluatorImpl.existEvaluador(Session_Class.Session_ID);

                if (E == null)
                {
                    // Mostrar mensaje de que no es evaluador y redirigir
                    Response.Write("<script>alert('No es evaluador');</script>");
                    Response.Redirect("VistaNoEvaluador.aspx"); // Redirigir a una página de acceso denegado
                }
                else
                {
                    nota = null;
                    notaImpl = new NotaImpl();

                    int idp = Session_Class.Session_ID;
                    nota = notaImpl.Get2(idp, int.Parse(Request.QueryString["projectid"]));

                    if (nota != null)
                    {
                        Response.Write("Solo Puede calificar una vez");
                        Response.Redirect("VistaSoloUnaVez.aspx");
                    }
                    else
                    {
                        int eventId;
                        
                        if (int.TryParse(Request.QueryString["eventid"], out eventId))
                        {
                            
                            idProyect = int.Parse(Request.QueryString["projectid"]);

                            
                            if (int.TryParse(Request.QueryString["careerid"], out carreraId))
                            {
                                
                                if (carreraId != Session_Class.Session_Career)
                                {
                                    
                                    Response.Redirect("VistaNoEvaluador.aspx");
                                }
                                else
                                {
                                    LoadScores(eventId);
                                }

                                
                            }
                            else
                            {
                                // Manejar el caso donde el careerId no esté presente o no sea válido
                                throw new Exception("El careerId no es válido.");
                            }
                        }
                        else
                        {
                            // Manejar el caso donde el eventId no esté presente o no sea válido
                            throw new Exception("El eventId no es válido.");
                        }

                        if (Session_Class.Session_Role != "User" && Session_Class.Session_Role != "Administrador" && Session_Class.Session_Role != "Usuario" && Session_Class.Session_Role != "Director" && Session_Class.Session_Role != "Evaluador")
                        {
                            // Redirigir a la página de inicio de sesión o mostrar un mensaje de error
                            Response.Redirect("https://localhost:44377/Login.aspx");
                            // O bien, puedes mostrar un mensaje de error
                            // Response.Write("No tienes permiso para acceder a esta página.");
                            // Y luego, si deseas, puedes ocultar el contenido del QR en la página
                            // qrCodeImage.Visible = false; // Esto depende de cómo estés mostrando el QR en la página
                        }
                        else
                        {
                            // El usuario tiene permiso para ver el código QR, así que no hagas nada especial aquí
                        }
                    }
                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                int promedio;
                if (int.TryParse(resultadoTextBox.Text, out promedio))
                {

                    idProyect = int.Parse(Request.QueryString["projectid"]);
                    idSesion = Session_Class.Session_ID;
                    // Crear una instancia de la nota con los valores necesarios
                    nota = new Nota(idProyect, idSesion, 1, promedio);



                    // Crear una instancia de la implementación de Nota
                    notaImpl = new NotaImpl();
                    int n = notaImpl.Insert(nota);
                    idEvent = int.Parse(Request.QueryString["eventid"]);
                    Response.Redirect($"Listado_evaluador_home.aspx?eventid={idEvent}");

                }
                else
                {
                    throw new Exception("El valor del promedio no es un número válido.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void LoadScores(int eventId)
        {
            scoreImpl = new ScoreImpl();
            DataTable dt = scoreImpl.SelectByEventId(eventId); // Filtrar por eventId

            int contador = 1;
            int sumaCalificaciones = 0;

            foreach (DataRow row in dt.Rows)
            {
                HtmlGenericControl td = new HtmlGenericControl("td");
                HtmlGenericControl tr = new HtmlGenericControl("tr");

                HtmlGenericControl tdNro = new HtmlGenericControl("td");
                tdNro.InnerText = contador.ToString();
                tr.Controls.Add(tdNro);

                HtmlGenericControl tdDescripcion = new HtmlGenericControl("td");
                tdDescripcion.InnerText = row["Description"].ToString();
                tr.Controls.Add(tdDescripcion);


                HtmlGenericControl tdCalificacion = new HtmlGenericControl("td");
                HtmlGenericControl txtCalificacion = new HtmlGenericControl("input");
                txtCalificacion.Attributes["type"] = "number";
                txtCalificacion.Attributes["min"] = "0";
                txtCalificacion.Attributes["max"] = "100";
                txtCalificacion.Attributes["class"] = "Cajitas-calificar";
                txtCalificacion.Attributes["value"] = "50"; // Valor predeterminado
                txtCalificacion.Attributes["id"] = "calificacion_" + contador; // Identificador único para el campo de calificación
                txtCalificacion.Attributes["onchange"] = "recalcularPromedio()"; // Agrega un controlador de eventos onchange

                txtCalificacion.Attributes["onchange"] = "validarCalificacion(this)";
                txtCalificacion.Attributes["onkeydown"] = "return validarEntrada(event)";

                tdCalificacion.Controls.Add(txtCalificacion);
                tr.Controls.Add(tdCalificacion);

                // Agregar atributos adicionales al campo de calificación

                

                // Sumar la calificación a la suma total
                sumaCalificaciones += Convert.ToInt32(txtCalificacion.Attributes["value"]);

                // Columna para las flechas
                

                tableBody.Controls.Add(tr);

                contador++;
            }
        }
    }
}
