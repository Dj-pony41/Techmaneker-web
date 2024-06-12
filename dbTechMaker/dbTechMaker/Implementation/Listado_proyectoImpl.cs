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
using System.Drawing.Imaging;
using System.Drawing;
using dbTechMaker.Utilities;

namespace dbTechMaker.Implementation
{
    public class Listado_proyectoImpl : BaseImpl, IListado_proyecto
    {
        public int Delete(Listado_proyecto t)
        {
            throw new NotImplementedException();
        }

        public Listado_proyecto Get(int id)
        {
            Listado_proyecto t = null;
            query = @"SELECT id, proyectName AS 'Nombre del Proyecto', description AS 'Descripcion del Proyecto', approvalStatus AS 'Estado de Aprovacion', qrCodeDir AS 'Codigo qr', careerId AS 'Codigo de carrera', eventId AS 'Codigo de evento', idUser AS 'Usuario creador'	   
                      FROM Proyect 
                      WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(command);
                if (table.Rows.Count > 0)
                {
                    t = new Listado_proyecto();
                    t.id = short.Parse(table.Rows[0]["id"].ToString());
                    t.proyectName = table.Rows[0]["Nombre del Proyecto"].ToString();
                    t.description = table.Rows[0]["Descripcion del Proyecto"].ToString();
                    t.approval = table.Rows[0]["Estado de Aprovacion"].ToString();
                    t.qrCode = table.Rows[0]["Codigo qr"].ToString();
                    t.careerId = short.Parse(table.Rows[0]["Codigo de carrera"].ToString());
                    t.eventId = short.Parse(table.Rows[0]["Codigo de evento"].ToString());
                    t.idUser = short.Parse(table.Rows[0]["Usuario creador"].ToString());

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return t;
        }

        public int Insert(Listado_proyecto t)
        {
            throw new NotImplementedException();
        }



        public int Update(Listado_proyecto t)
        {
            throw new NotImplementedException();
        }

        public DataTable ObtenerNotificaciones(int careerId)
        {
            string query = @"
        SELECT id, 
               proyectName AS 'NombreProyecto', 
               approvalStatus AS 'Estado', 
               registerDate AS 'Fecha'
        FROM Proyect
        WHERE approvalStatus IN ('Aceptado', 'Rechazado')
        AND CareerId = @careerId
        ORDER BY registerDate DESC";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@careerId", careerId);

            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public DataTable SelectObservation(int id)
        {
            string query = @"SELECT Observation
                    FROM Proyect
                    WHERE id = @id";

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






        public int UpdateStatus(Listado_proyecto t, string status, string observation)
        {
            string nombreArchivoQR = $"{t.proyectName}.png"; // Nombre del archivo basado en el nombre del proyecto
            string directorio = @"C:\imagenqrAprobados\";
            string rutaImagenQR = Path.Combine(directorio, nombreArchivoQR);

            string query = ""; // Inicializar la consulta SQL
            SqlCommand command = null; // Inicializar el SqlCommand

            if (status == "A")
            {
                query = @"UPDATE Proyect 
                  SET approvalStatus = 'Aceptado', 
                      UpdateDate = CURRENT_TIMESTAMP, 
                      UserId = @UserId
                  WHERE id = @id";

                // Crear el SqlCommand
                command = CreateBasicCommand(query);
                //command.Parameters.AddWithValue("@qrCode", rutaImagenQR);
            }
            else if (status == "R")
            {
                query = @"UPDATE Proyect 
                  SET approvalStatus = 'Rechazado', 
                      UpdateDate = CURRENT_TIMESTAMP, 
                      UserId = @UserId, 
                      Observation = @observation 
                  WHERE id = @id";

                // Crear el SqlCommand
                command = CreateBasicCommand(query);
                command.Parameters.AddWithValue("@observation", observation ?? string.Empty);
            }

            command.Parameters.AddWithValue("@id", t.id); // Agregar parámetro para el id del proyecto
            command.Parameters.AddWithValue("@UserId", Session_Class.Session_ID);

            try
            {
                // Ejecutar la consulta SQL
                int result = ExecuteBasicCommand(command);

                // Verificar si el estado es "Aceptado" y el resultado de la ejecución
                if (status == "A" && result > 0)
                {
                    // Generar la URL específica para el proyecto
                    string proyectoEncoded = Uri.EscapeDataString(t.proyectName);
                    string url = $"https://localhost:44377/ScoreWeb.aspx?proyecto={proyectoEncoded}&eventid={t.eventId}&projectid={t.id}&careerid={t.careerId}";

                    // Generar el código QR específico para este proyecto
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20); // Ajusta el tamaño según sea necesario

                    // Guardar la imagen del código QR en el sistema de archivos
                    Bitmap imagen = new Bitmap(qrCodeImage);

                    byte[] imageData = BitmapToBytes(imagen);
                    string query3 = @"UPDATE Proyect
                                 SET qrCode = @qrCode
                                     WHERE id = @id";

                    SqlCommand command3 = CreateBasicCommand(query3);
                    command3.Parameters.AddWithValue("@qrCode", imageData);
                    command3.Parameters.AddWithValue("@id", t.id);
                    ExecuteBasicCommand(command3);

                    // Guardar la imagen en la ruta especificada
                    imagen.Save(rutaImagenQR, ImageFormat.Png);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateStatusAsynAsync(Listado_proyecto t, string status, string observation, string evento, string date)
        {
            string nombreArchivoQR = $"{t.proyectName}.png"; // Nombre del archivo basado en el nombre del proyecto
            string directorio = @"C:\imagenqrAprobados\";
            string rutaImagenQR = Path.Combine(directorio, nombreArchivoQR);

            string query = ""; // Inicializar la consulta SQL
            SqlCommand command = null; // Inicializar el SqlCommand

            if (status == "A")
            {
                query = @"UPDATE Proyect 
                  SET approvalStatus = 'Aceptado', 
                      UpdateDate = CURRENT_TIMESTAMP, 
                      UserId = @UserId
                  WHERE id = @id";

                // Crear el SqlCommand
                command = CreateBasicCommand(query);
                //command.Parameters.AddWithValue("@qrCode", rutaImagenQR);
            }
            else if (status == "R")
            {
                query = @"UPDATE Proyect 
                  SET approvalStatus = 'Rechazado', 
                      UpdateDate = CURRENT_TIMESTAMP, 
                      UserId = @UserId, 
                      Observation = @observation 
                  WHERE id = @id";

                // Crear el SqlCommand
                command = CreateBasicCommand(query);
                command.Parameters.AddWithValue("@observation", observation ?? string.Empty);
            }

            command.Parameters.AddWithValue("@id", t.id); // Agregar parámetro para el id del proyecto
            command.Parameters.AddWithValue("@UserId", Session_Class.Session_ID);

            try
            {
                // Ejecutar la consulta SQL
                int result = ExecuteBasicCommand(command);

                // Verificar si el estado es "Aceptado" y el resultado de la ejecución
                if (status == "A" && result > 0)
                {
                    // Generar la URL específica para el proyecto
                    string proyectoEncoded = Uri.EscapeDataString(t.proyectName);
                    string url = $"https://localhost:44377/ScoreWeb.aspx?proyecto={proyectoEncoded}&eventid={t.eventId}&projectid={t.id}&careerid={t.careerId}";

                    // Generar el código QR específico para este proyecto
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20); // Ajusta el tamaño según sea necesario

                    // Guardar la imagen del código QR en el sistema de archivos
                    Bitmap imagen = new Bitmap(qrCodeImage);

                    string Folder_proyect = evento + " " + date;
                    string Qr_proyect_name = "Proyecto - " + t.proyectName;

                    string fileUrl = await DropboxUploaderProyecto.UploadQrCode(imagen, Qr_proyect_name, Folder_proyect);

                    byte[] imageData = BitmapToBytes(imagen);
                    string query3 = @"UPDATE Proyect
                                 SET qrCodeDir = @qrCodeDir
                                     WHERE id = @id";

                    SqlCommand command3 = CreateBasicCommand(query3);
                    command3.Parameters.AddWithValue("@qrCodeDir", fileUrl);
                    command3.Parameters.AddWithValue("@id", t.id);
                    ExecuteBasicCommand(command3);

                    // Guardar la imagen en la ruta especificada
                    imagen.Save(rutaImagenQR, ImageFormat.Png);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private byte[] BitmapToBytes(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                return stream.ToArray();
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





        public int UpdateStatus(Listado_proyecto t , string type)
        {
            SqlCommand command = null; // Declarar el SqlCommand fuera de los bloques if para que esté disponible en todo el método

            if (type == "A")
            {
                query = @"
UPDATE Proyect SET approvalStatus = 'Aceptado', UpdateDate = CURRENT_TIMESTAMP, UserId = 1 , qrCode = 'gg'
          WHERE id = 19";
            }
            else if (type == "R")
            {
                query = @"UPDATE Proyect SET approvalStatus = 'Rechazado', UpdateDate = CURRENT_TIMESTAMP, UserId = 1 
          WHERE id = @id";
            }

            command = CreateBasicCommand(query); // Crear el SqlCommand aquí después de determinar la consulta

            command.Parameters.AddWithValue("@id", t.id); // Agregar parámetros al SqlCommand

            try
            {
                return ExecuteBasicCommand(command); // Ejecutar la consulta
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public DataTable Select(Listado_proyecto t)
        {
            throw new NotImplementedException();
        }

        public DataTable Select2(Listado_proyecto t)
        {
            string query = @"
               SELECT 
                    P.id, 
                    P.proyectName, 
                    C.Name AS careerName, 
                    P.description, 
                    E.eventName, 
                    CONCAT(YEAR(E.startDate), '-',MONTH(E.startDate), '-', DAY(E.startDate)) AS FechaInicioEvento,
                    P.registerDate, 
                    P.approvalStatus,
	                ISNULL(P.qrCodeDir,'S/Qr') AS qrCodeDir
                FROM 
                    Proyect P
                INNER JOIN 
                    Career C ON C.id = P.careerId
                INNER JOIN 
                    Event E ON E.id = P.eventId
                WHERE 
                    C.id = @careerId 
                    AND E.status = 1
                ORDER BY 
                    CASE 
                        WHEN P.approvalStatus = 'Pendiente' THEN 1 
                        WHEN P.approvalStatus = 'Aceptado' THEN 2 
                        WHEN P.approvalStatus = 'Rechazado' THEN 3 
                        ELSE 4 
                    END
                ;
            ";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@careerId", t.careerId);

            try
            {
                return ExecuteDataTableCommand(command);
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



        public DataTable Select3(Listado_proyecto t)
        {
            string query = @"
        
SELECT 
    p.id, 
    p.proyectName AS 'Nombre del Proyecto', 
    p.[description] AS 'Descripcion del Proyecto', 
    p.approvalStatus AS 'Estado de Aprobacion', 
  
     u.userName AS 'Nombre de Usuario',
    p.registerDate AS 'Fecha de Registro',
    c.name AS 'Aprobado por'
FROM 
    Proyect p
JOIN 
    [User] u ON p.idUser = u.id
JOIN 
    Event e ON p.eventId = e.id
LEFT JOIN 
    Career c ON u.idCareer = c.id
WHERE 
    p.eventId = @id
ORDER BY 
    CASE 
        WHEN p.approvalStatus = 'Aceptado' THEN 1 
        WHEN p.approvalStatus = 'Pendiente' THEN 2
        WHEN p.approvalStatus = 'Rechazado' THEN 3 
        ELSE 4
    END,
    p.registerDate;

";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", t.eventId);

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
