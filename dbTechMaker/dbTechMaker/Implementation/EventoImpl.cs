using dbTechMaker.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbTechMaker.Model;
using dbTechMaker.Utilities;

using QRCoder;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;



namespace dbTechMaker.Implementation
{
    public class EventoImpl : BaseImpl, IEvento
    {
        public int Delete(Evento t)
        {
            query = @"UPDATE Event SET status = 0, UpdateDate = CURRENT_TIMESTAMP, UserId = @UserId
                      WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@UserId", 1); //ojo
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

        public Evento Get(int id)
        {
            Evento t = null;
            query = @"SELECT id, eventName AS 'Nombre del evento', description AS 'Descripcion del evento', management AS 'Gestion del evento', startDate AS 'Fecha de inicio', endDate AS 'Fecha de finalisacion' 
                      FROM Event
                      WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                DataTable table = ExecuteDataTableCommand(command);
                if (table.Rows.Count > 0)
                {
                    t = new Evento();
                    t.Id = short.Parse(table.Rows[0]["id"].ToString());
                    t.Name = table.Rows[0]["Nombre del evento"].ToString();
                    t.Description = table.Rows[0]["Descripcion del evento"].ToString();
                    t.Gestion = table.Rows[0]["Gestion del evento"].ToString();
                    t.Fecha_inicio = DateTime.Parse(table.Rows[0]["Fecha de inicio"].ToString());
                    t.Fecha_fin = DateTime.Parse(table.Rows[0]["Fecha de finalisacion"].ToString());

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return t;
        }

        public async Task<int> InsertAsync(Evento t)
        {
            string nombreArchivoQR = $"{t.Name}.png";
            string directorio = @"C:\imagenqrEventos\";
            string rutaImagenQR = Path.Combine(directorio, nombreArchivoQR);

            query = @"INSERT INTO Event(eventName, description, management, startDate, endDate, UserId)
              VALUES (@eventName, @description, @management, @startDate, @endDate, @UserId)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@eventName", t.Name);
            command.Parameters.AddWithValue("@description", t.Description);
            command.Parameters.AddWithValue("@management", t.Gestion);
            command.Parameters.AddWithValue("@startDate", t.Fecha_inicio);
            command.Parameters.AddWithValue("@endDate", t.Fecha_fin);
            command.Parameters.AddWithValue("@UserId", 1);//ojo
            try
            {
                int result = ExecuteBasicCommand(command);

                string query2 = @"SELECT TOP 1 id, CONCAT(YEAR(startDate), '-',MONTH(startDate), '-', DAY(startDate))
                          FROM Event
                          ORDER BY registerDate DESC";

                SqlCommand comand2 = CreateBasicCommand(query2);
                DataTable td = ExecuteDataTableCommand(comand2);
                string eventoEncoded = Uri.EscapeDataString(t.Name);
                string url = $"https://localhost:44377/Listado_evaluador_home.aspx?evento={eventoEncoded}&eventid={td.Rows[0][0]}";
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20); // Ajusta el tamaño según sea necesario

                // Guardar la imagen del código QR en el sistema de archivos
                Bitmap imagen = new Bitmap(qrCodeImage);

                byte[] imageData = BitmapToBytes(imagen);
                string Folder_name = t.Name + " " + td.Rows[0][1];
                string QrEventName = "Evento - " + t.Name;
                // Recuperar aquí en una variable el URL de la imagen
                string fileUrl = await DropboxUploader.UploadQrCode(imagen, QrEventName, Folder_name);

                string query3 = @"UPDATE Event
                         SET qrCodeDir = @qrCodeDir
                             WHERE id = @id";

                SqlCommand command3 = CreateBasicCommand(query3);
                command3.Parameters.AddWithValue("@qrCodeDir", fileUrl);
                command3.Parameters.AddWithValue("@id", td.Rows[0][0]);
                ExecuteBasicCommand(command3);

                // Guardar la imagen en la ruta especificada
                imagen.Save(rutaImagenQR, ImageFormat.Png);

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

        public DataTable Select()
        {
            query = @"SELECT id, eventName, description, management, startDate, endDate, qrCodeDir 
					  FROM Event
                      WHERE status = 1";
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

        public int Update(Evento t)
        {
            query = @"UPDATE Event SET eventName = @eventName, description = @description, management = @management, startDate = @startDate, endDate = @endDate, UpdateDate = CURRENT_TIMESTAMP, UserId = @UserId
                      WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@eventName", t.Name);
            command.Parameters.AddWithValue("@description", t.Description);
            command.Parameters.AddWithValue("@management", t.Gestion);
            command.Parameters.AddWithValue("@startDate", t.Fecha_inicio);
            command.Parameters.AddWithValue("@endDate", t.Fecha_fin);
            command.Parameters.AddWithValue("@UserId", 1); //ojo
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
        public DataTable Eventos()
        {
            query = @"SELECT id, eventName AS 'Evento'
                      FROM [Event]
                      WHERE status=1";
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

        public int Insert(Evento t)
        {
            return InsertAsync(t).GetAwaiter().GetResult();
        }
    }
}
