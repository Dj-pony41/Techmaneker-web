using dbTechMaker.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbTechMaker.Model;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace dbTechMaker.Implementation
{
    public class ProyectoImpl : BaseImpl, IProyecto
    {
        private string query;



        public int Delete(Proyecto t)
        {
            query = @"DELETE FROM Proyect WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);

            command.Parameters.AddWithValue("@id", t.Id);
            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int generarqrprueba()
        {
            int result = 1;
            string nombreArchivoQR = $"Techmaker.png";
            string directorio = @"D:\imagenqrEventos\";
            string rutaImagenQR = Path.Combine(directorio, nombreArchivoQR);
            string url = $"https://localhost:44377/Listado_evaluador_home.aspx?evento=Techmaker&eventid=1";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20); // Ajusta el tamaño según sea necesario

            // Guardar la imagen del código QR en el sistema de archivos
            Bitmap imagen = new Bitmap(qrCodeImage);

            imagen.Save(rutaImagenQR, ImageFormat.Png);

            return result;
        }

        public int Delete(int t)
        {
            query = @"DELETE FROM Proyect WHERE id= @id";
            SqlCommand command = CreateBasicCommand(query);

            command.Parameters.AddWithValue("@id", t);
            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Proyecto Get(int id)
        {
            Proyecto p = null;
            query = @"SELECT id, proyectName, description, approvalStatus, qrCode, careerid, 
                    eventid, idUser, status, registerDate, ISNULL(UpdateDate, CURRENT_TIMESTAMP), Userid FROM Proyect WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);

            try
            {
                DataTable table = ExecuteDataTableCommand(command);
                if (table.Rows.Count > 0)
                {
                    p = new Proyecto
                    {
                        Id = int.Parse(table.Rows[0][0].ToString()),
                        ProyectName = table.Rows[0]["proyectName"].ToString(),
                        Description = table.Rows[0]["description"].ToString(),
                        ApprovalStatus = table.Rows[0]["approvalStatus"].ToString(),
                        QrCode = table.Rows[0]["qrCode"].ToString(),
                        Careerid = Convert.ToInt32(table.Rows[0]["careerid"]),
                        Eventid = Convert.ToInt32(table.Rows[0]["eventid"]),
                        IdUser = Convert.ToInt32(table.Rows[0]["idUser"]),
                        Userid = Convert.ToInt32(table.Rows[0]["Userid"])
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return p;
        }

        public int Insert(Proyecto t)
        {
            try
            {
                string nombreArchivoQR = $"{t.ProyectName}.png"; // Nombre del archivo basado en el nombre del proyecto

                string directorio = @"D:\imagenqr\";
                string rutaImagenQR = Path.Combine(directorio, nombreArchivoQR);

                // Insertar el proyecto en la base de datos junto con la ruta de la imagen del código QR
                string query = @"INSERT INTO Proyect (proyectName, description, careerid, eventid, idUser, Userid) 
            VALUES (@proyectName, @description, @careerid, @eventid, @idUser, @Userid)";

                SqlCommand command = CreateBasicCommand(query);
                command.Parameters.AddWithValue("@proyectName", t.ProyectName);
                command.Parameters.AddWithValue("@description", t.Description);
                command.Parameters.AddWithValue("@careerid", t.Careerid);
                command.Parameters.AddWithValue("@eventid", t.Eventid);
                command.Parameters.AddWithValue("@idUser", t.IdUser);
                command.Parameters.AddWithValue("@Userid", Session_Class.Session_ID);

                int ejecucion_correcta = ExecuteBasicCommand(command);


                string query2 = @"SELECT TOP 1 id
                                  FROM Proyect
                                  ORDER BY registerDate DESC";

                SqlCommand comand2 = CreateBasicCommand(query2);



                DataTable td = ExecuteDataTableCommand(comand2);
                string url;
                // Verifica si el usuario está logueado
                
                    // Si está logueado, genera la URL específica para el proyecto
                string proyectoEncoded = Uri.EscapeDataString(t.ProyectName); // Encode el nombre del proyecto
                url = $"https://localhost:44377/ScoreWeb.aspx?proyecto={proyectoEncoded}&eventid={t.Eventid}&sessionID={Session_Class.Session_ID}&projectid={td.Rows[0][0]}"; //ojo correjir
                

                // Generar el código QR específico para este proyecto
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20); // Ajusta el tamaño según tus necesidades

                // Guardar la imagen del código QR en el sistema de archivos
                Bitmap imagen = new Bitmap(qrCodeImage);

               

                // Guarda la imagen en la ruta especificada
                //imagen.Save(rutaImagenQR, ImageFormat.Png);



                return ejecucion_correcta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        


        private string GuardarImagenQR(Bitmap imagen, string nombreArchivo)
        {
            // Define la ruta donde se guardará la imagen QR, puedes modificar esta ruta según tus necesidades
            string directorio = @"D:\imagenqr\";
            string rutaCompleta = Path.Combine(directorio, nombreArchivo);

            // Guarda la imagen en la ruta especificada
            imagen.Save(rutaCompleta, ImageFormat.Png);

            // Retorna la ruta completa de la imagen QR
            return rutaCompleta;
        }


        public DataTable Select()
        {

            query = @"SELECT id, proyectName, description, approvalStatus, qrCode, careerid, eventid, idUser, 
                        status, registerDate, UpdateDate, Userid FROM Proyect ORDER BY proyectName";
            SqlCommand command = CreateBasicCommand(query);

            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Proyecto t)
        {
            try
            {
                // URL que quieres que el código QR represente, incluyendo el eventid
                string proyectoEncoded = Uri.EscapeDataString(t.ProyectName); // Encode el nombre del proyecto
                string url = $"https://localhost:44377/ScoreWeb.aspx?proyecto={proyectoEncoded}&eventid={t.Eventid}&id={t.Id}";

                // Generar el código QR específico para este proyecto
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20); // Ajusta el tamaño según tus necesidades

                // Guardar la imagen del código QR en el sistema de archivos
                string nombreArchivoQR = $"{t.ProyectName}.png"; // Nombre del archivo basado en el nombre del proyecto
                string rutaImagenQR = GuardarImagenQR(qrCodeImage, nombreArchivoQR);

                // Actualizar el proyecto en la base de datos junto con la nueva ruta de la imagen del código QR
                string query = @"UPDATE Proyect
                        SET proyectName = @proyectName, 
                            description = @description,
                            qrCode = @qrCode WHERE id = @id";

                SqlCommand command = CreateBasicCommand(query);
                command.Parameters.AddWithValue("@proyectName", t.ProyectName);
                command.Parameters.AddWithValue("@description", t.Description);
                command.Parameters.AddWithValue("@qrCode", "NULL"); // Utiliza la nueva ruta de la imagen guardada en el sistema de archivos
                command.Parameters.AddWithValue("@careerid", t.Careerid);
               
             
                command.Parameters.AddWithValue("@id", t.Id);

                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ComboBoxEvento()
        {
            query = @" SELECT id,eventName
                       FROM Event
                       WHERE Status=1";
            SqlCommand command = CreateBasicCommand(query);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable Evento_info(int id)
        {
            query = @"
        SELECT 
            P.id, 
            P.proyectName AS 'Nombre del Proyecto', 
            C.Name AS 'Carrera', 
            P.description AS 'Descripcion', 
            E.eventName AS 'Nombre del Evento', 
            P.registerDate AS 'Fecha de registro',
            ISNULL(AVG(S.note), 200) AS 'Nota del proyecto'
        FROM 
            Proyect P
        INNER JOIN 
            [Event] E ON E.id = P.eventId
        INNER JOIN 
            Career C ON C.id = P.careerId
        LEFT JOIN 
            Score S ON S.idProyect = P.id
        WHERE 
            E.id = @id AND
            P.approvalStatus = 'Aceptado' AND 
            E.status = 1
        GROUP BY 
            P.id, P.proyectName, C.Name, P.description, E.eventName, P.registerDate
        ORDER BY 
            C.Name,  -- Primero ordenar por carrera
            CASE 
                WHEN ISNULL(AVG(S.note), 200) = 200 THEN 1 
                ELSE 0 
            END,
            ISNULL(AVG(S.note), 200) DESC,  -- Luego ordenar por calificación dentro de cada carrera
            P.proyectName";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable Proyectos_sugeridos(int id)
        {
            query = @"SELECT id, proyectName AS 'Nombre del Proyecto', description AS 'description', ISNULL(Observation,'Sin observaciones para mostrar') AS 'Observation', approvalStatus AS 'approvalStatus'
                      FROM Proyect
                      WHERE idUser = @id AND status = 1
                      ORDER BY 
                      CASE 
                          WHEN approvalStatus = 'Pendiente' THEN 1 
                          WHEN approvalStatus = 'Aceptado' THEN 2 
                          WHEN approvalStatus = 'Rechazado' THEN 3 
                          ELSE 4 
                      END";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Proyectos_del_evento(int id)
        {
            query = @"SELECT P.id AS id, P.proyectName AS NombreProyecto, P.description AS Descripcion,
                     (SELECT COUNT(id) FROM Score WHERE idEvaluator = @idEvaluator AND idProyect = P.id) AS Estado, P.eventId AS Evento, P.careerId AS Carrera
                      FROM Proyect P
                      WHERE eventId = @eventId AND approvalStatus = 'Aceptado' AND careerId = @careerId
                      ORDER BY 4";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@eventId", id);
            command.Parameters.AddWithValue("@idEvaluator", Session_Class.Session_ID);
            command.Parameters.AddWithValue("@careerId", Session_Class.Session_Career);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Proyectos_restantes(int id)
        {
            query = @"SELECT (SELECT COUNT(id)
		                      FROM Proyect
		                      WHERE eventId = @eventId AND approvalStatus = 'Aceptado' AND careerId = @careerId) - COUNT(P.id) AS 'Proyectos_restantes',(SELECT name
																								                                FROM [User]
																								                                WHERE id = @idEvaluator) AS 'Nombre'
                      FROM Proyect P
                      INNER JOIN Score S ON S.idProyect = P.id
                      WHERE eventId = @eventId AND approvalStatus = 'Aceptado' AND S.idEvaluator = @idEvaluator";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@eventId", id);
            command.Parameters.AddWithValue("@idEvaluator", Session_Class.Session_ID);
            command.Parameters.AddWithValue("@careerId", Session_Class.Session_Career);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }


}
