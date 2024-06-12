using System;
using dbTechMaker.Implementation;
using dbTechMaker.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TechMakerWeb
{
    public partial class Crud_Metrics : System.Web.UI.Page
    {
        private MetricasImpl metricaImpl;
        private string type;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // Inicializa la lista de preguntas si es la primera vez que se carga la página
                Preguntas = new List<string>();
            }

            // Siempre recrear los controles dinámicos en cada postback
            Listar_preguntas();
        }

        private List<string> Preguntas
        {
            get
            {
                if (ViewState["Preguntas"] == null)
                {
                    ViewState["Preguntas"] = new List<string>();
                }
                return (List<string>)ViewState["Preguntas"];
            }
            set
            {
                ViewState["Preguntas"] = value;
            }
        }

        protected void btnAgregarPregunta_Click(object sender, EventArgs e)
        {
            // Obtener el texto de la caja de texto
            string pregunta = txt_Pregunta.Text.Trim();

            // Verificar que no esté vacío
            if (!string.IsNullOrEmpty(pregunta))
            {
                // Agregar la pregunta a la lista
                List<string> preguntas = Preguntas;
                preguntas.Add(pregunta);
                Preguntas = preguntas;

                // Limpiar la caja de texto
                txt_Pregunta.Text = string.Empty;

                // Volver a listar las preguntas
                Listar_preguntas();
            }
        }

        protected void btnEliminarPregunta_Click(object sender, EventArgs e)
        {
            // Obtener el botón que desencadenó el evento
            Button btnEliminar = (Button)sender;

            // Obtener el índice de la pregunta en la lista desde el CommandArgument del botón
            int preguntaIndex = int.Parse(btnEliminar.CommandArgument);

            // Verificar si el índice es válido
            if (preguntaIndex >= 0 && preguntaIndex < Preguntas.Count)
            {
                // Eliminar la pregunta de la lista
                List<string> preguntas = Preguntas;
                preguntas.RemoveAt(preguntaIndex);
                Preguntas = preguntas;

                // Volver a listar las preguntas actualizadas
                Listar_preguntas();
            }
        }

        protected void Listar_preguntas()
        {
            // Limpiar el contenido actual del PlaceHolder
            phPreguntas.Controls.Clear();

            // Recorrer la lista de preguntas y generar los controles de servidor de ASP.NET para cada una
            for (int i = 0; i < Preguntas.Count; i++)
            {
                // Crear un nuevo elemento <li> para la pregunta
                HtmlGenericControl li = new HtmlGenericControl("li");
                li.ID = "elemento" + i; // Asignar un ID único

                // Crear un nuevo párrafo para el texto de la pregunta
                HtmlGenericControl p = new HtmlGenericControl("p");
                p.Attributes["class"] = "text";
                p.InnerText = Preguntas[i]; // Establecer el texto de la pregunta

                // Crear un nuevo botón de servidor de ASP.NET para eliminar la pregunta
                Button btnEliminar = new Button();
                btnEliminar.ID = "btnEliminar" + i; // Establecer el ID del botón
                btnEliminar.CommandArgument = i.ToString(); // Establecer el índice como argumento de comando
                btnEliminar.CssClass = "btn-svg-icon"; // Asignar una clase CSS si es necesario
                btnEliminar.Text = ""; // Texto del botón
                btnEliminar.Click += btnEliminarPregunta_Click; // Asociar el evento click

                // Agregar el botón al elemento <li>
                li.Controls.Add(p); // Agregar el párrafo al elemento <li>
                li.Controls.Add(btnEliminar); // Agregar el botón al elemento <li>

                // Agregar el elemento <li> al PlaceHolder
                phPreguntas.Controls.Add(li);
            }
        }

        protected void btnConcatenarPreguntas_Click(object sender, EventArgs e)
        {
            metricaImpl = new MetricasImpl();
            DataTable dt = metricaImpl.Select_id_evento();
            Session_Class.Session_Metrics = int.Parse(dt.Rows[0][0].ToString());
            // Crear un string para almacenar las preguntas concatenadas
            string Preguntas_criterio = "";

            // Recorrer la lista de preguntas
            try
            {
                if (type == "U")
                {
                    
                }
                else
                {
                    foreach (string pregunta in Preguntas)
                    {
                        Preguntas_criterio = pregunta;
                        metricaImpl = new MetricasImpl();
                        Metricas metrica = new Metricas(Preguntas_criterio, Session_Class.Session_Career, Session_Class.Session_Metrics, Session_Class.Session_ID);
                        int v = metricaImpl.Insert(metrica);

                        // Concatenar cada pregunta al string


                    }
                }
                
                Response.Redirect("Listado_eventos.aspx");
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }
    }
}
