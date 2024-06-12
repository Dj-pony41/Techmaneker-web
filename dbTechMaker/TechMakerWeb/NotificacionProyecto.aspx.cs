using dbTechMaker.Implementation;
using dbTechMaker.Model;
using System;
using System.Data;
using System.Web.UI.HtmlControls;

namespace TechMakerWeb
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarNotificaciones(); // Llama a un método para cargar las notificaciones en la página
            }
        }

        private void CargarNotificaciones()
        {
            // Obtener el careerId de la sesión
            int careerId = Session_Class.Session_Career;

            // Aquí deberías obtener los datos de las notificaciones filtradas por el careerId de la sesión
            Listado_proyectoImpl proyectoImpl = new Listado_proyectoImpl();
            DataTable dt = proyectoImpl.ObtenerNotificaciones(careerId); // Método ficticio para obtener las notificaciones

            if (dt != null && dt.Rows.Count > 0)
            {
                GenerarNotificaciones(dt); // Genera las notificaciones en la página
            }
            else
            {
                // Manejo de caso donde no hay notificaciones disponibles
                // Por ejemplo, mostrar un mensaje indicando que no hay notificaciones
                HtmlGenericControl li = new HtmlGenericControl("li");
                li.InnerText = "No hay notificaciones disponibles.";
                notificationList.Controls.Add(li);
            }
        }




        private void GenerarNotificaciones(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                // Crear un elemento de lista para cada notificación
                HtmlGenericControl li = new HtmlGenericControl("li");
                li.Attributes["class"] = "notification-item";

                // Verificar si el proyecto fue aprobado o rechazado y asignar la clase correspondiente
                if (row["Estado"].ToString() == "Aceptado")
                {
                    li.Attributes["class"] += " approved";
                }
                else if (row["Estado"].ToString() == "Rechazado")
                {
                    li.Attributes["class"] += " rejected";
                }

                // Crear un div para mostrar la información de la notificación
                HtmlGenericControl divNotification = new HtmlGenericControl("div");
                divNotification.Attributes["class"] = "notification";

                // Agregar la fecha al div de notificación (a la izquierda)
                HtmlGenericControl divFecha = new HtmlGenericControl("div");
                divFecha.Attributes["class"] = "fecha";
                divFecha.InnerText = Convert.ToDateTime(row["Fecha"]).ToString("dd/MM/yyyy");
                divNotification.Controls.Add(divFecha);

                // Agregar información de la notificación al div
                string mensaje = $"El proyecto '{row["NombreProyecto"]}' ha sido {row["Estado"]}.";
                HtmlGenericControl divMensaje = new HtmlGenericControl("div");
                divMensaje.Attributes["class"] = "mensaje";
                divMensaje.InnerText = mensaje;
                divNotification.Controls.Add(divMensaje);

                // Si el estado es "Rechazado", agregar el botón de detalles
                if (row["Estado"].ToString() == "Rechazado")
                {
                    // Obtener la observación del proyecto
                    string observation = SelectObservation(Convert.ToInt32(row["id"]));

                    // Crear un div para mostrar la observación
                    HtmlGenericControl divObservation = new HtmlGenericControl("div");
                    divObservation.Attributes["class"] = "observacion";
                    divObservation.InnerText = $"{observation}";
                    divNotification.Controls.Add(divObservation);
                }





                // Agregar el div de notificación al elemento de lista
                li.Controls.Add(divNotification);

                // Agregar la notificación a la lista de notificaciones
                notificationList.Controls.Add(li);
            }
        }


        private string SelectObservation(int projectId)
        {
            // Aquí deberías escribir la lógica para seleccionar la observación del proyecto con el projectId proporcionado
            // Supongamos que ya tienes una instancia de Listado_proyectoImpl para obtener los datos
            Listado_proyectoImpl proyectoImpl = new Listado_proyectoImpl();
            DataTable observationData = proyectoImpl.SelectObservation(projectId); // Método ficticio para seleccionar la observación

            if (observationData != null && observationData.Rows.Count > 0)
            {
                // Obtener la observación de la primera fila de datos
                string observation = observationData.Rows[0]["Observation"].ToString();
                return observation;
            }
            else
            {
                // Manejar el caso donde no se encontró ninguna observación
                return "No se encontró ninguna observación para este proyecto.";
            }
        }


    }
}
