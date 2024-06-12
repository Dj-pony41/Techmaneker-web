using dbTechMaker.Implementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TechMakerWeb
{
    public partial class Listado_evaluador_home : System.Web.UI.Page
    {
        private ProyectoImpl proyectoImpl;
        private short id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id = short.Parse(Request.QueryString["eventid"]);
                proyectoImpl = new ProyectoImpl();
                DataTable dt = proyectoImpl.Proyectos_del_evento(id); //ojo
                GenerarFlashCards(dt);

                proyectoImpl = new ProyectoImpl();
                DataTable dt2 = proyectoImpl.Proyectos_restantes(id); //ojo
                ActualizarDatosEvaluador(dt2);

                
                
            }
        }

        private void GenerarFlashCards(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                // Crear el contenedor de la categoría
                HtmlGenericControl divCategory = new HtmlGenericControl("div");
                divCategory.Attributes["class"] = "category";

                // Crear el contenedor de la parte izquierda
                HtmlGenericControl divLeft = new HtmlGenericControl("div");
                divLeft.Attributes["class"] = "left";

                // Imagen del proyecto
                HtmlGenericControl img = new HtmlGenericControl("img");
                img.Attributes["src"] = "Css/Images/images.png";
                img.Attributes["alt"] = "sun";
                divLeft.Controls.Add(img);

                // Contenido del proyecto
                HtmlGenericControl divContent = new HtmlGenericControl("div");
                divContent.Attributes["class"] = "content";
                HtmlGenericControl h1 = new HtmlGenericControl("h1");
                h1.Attributes["class"] = "Proyect";
                h1.InnerText = row["NombreProyecto"].ToString();
                HtmlGenericControl pDesc = new HtmlGenericControl("p");
                pDesc.Attributes["class"] = "description";
                pDesc.InnerText = row["Descripcion"].ToString();

                divContent.Controls.Add(h1);
                divContent.Controls.Add(pDesc);
                divLeft.Controls.Add(divContent);

                // Añadir la parte izquierda a la categoría
                divCategory.Controls.Add(divLeft);

                // Crear el contenedor de las opciones
                HtmlGenericControl divOptions = new HtmlGenericControl("div");
                divOptions.Attributes["class"] = "options";

                HtmlGenericControl pEstado = new HtmlGenericControl("p");
                int estado = (int)row["Estado"];
                if (estado == 1)
                {
                    pEstado.Attributes["class"] = "Calificado letra";
                    pEstado.InnerText = "Calificado";
                }
                else if (estado == 0)
                {
                    

                    HtmlAnchor buttonCalificar = new HtmlAnchor();
                    buttonCalificar.Attributes["class"] = "btn";
                    buttonCalificar.InnerText = "Calificar";
                    // Supongamos que tienes una página llamada "CalificarProyecto.aspx" y pasas un ID de proyecto en la query string
                    buttonCalificar.HRef = $"https://localhost:44377/ScoreWeb.aspx?proyecto={row["NombreProyecto"]}&eventid={row["Evento"]}&projectid={row["id"]}&careerid={row["Carrera"]}";
                    divOptions.Controls.Add(buttonCalificar);
                }
                divOptions.Controls.Add(pEstado);

                // Crear el botón calificar como un enlace
                

                // Añadir las opciones a la categoría
                divCategory.Controls.Add(divOptions);

                // Añadir la categoría al contenedor principal de categorías
                categorias.Controls.Add(divCategory);
            }
        }

        private void ActualizarDatosEvaluador(DataTable dt)
        {
            string nombreEvaluador = dt.Rows[0]["Nombre"].ToString();
            int proyectosPorCalificar = int.Parse(dt.Rows[0]["Proyectos_restantes"].ToString());
            Evaluador.InnerText = $"Hola {nombreEvaluador}";
            totalProyectos.InnerText = proyectosPorCalificar.ToString();
        }

    }
}