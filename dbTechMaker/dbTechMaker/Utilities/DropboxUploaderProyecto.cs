using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace dbTechMaker.Utilities
{
    public class DropboxUploaderProyecto
    {
        private const string AccessToken = "sl.B29mxfGIdZXjI06AjXRnM3WVVqJSiND51frOgvebGbOn8iO-0MnR9MeH4IeEIRWf3GarZoL3NTGUWZVFZ-HWx_QbdBiydMgwCZvay6bNvNOU7_Ai_5MUQeinVjjKGBiEhzzDjh_ktcSEYWE";

        public static async Task<string> UploadQrCode(Bitmap qrCodeBitmap, string fileName, string folderName)
        {
            using (var dbx = new DropboxClient(AccessToken))
            {
                // Asegurarse de que el folderName empiece con '/'
                if (!folderName.StartsWith("/"))
                {
                    folderName = "/" + folderName;
                }

                try
                {
                    // Verificar si el folder existe
                    var listFolderResult = await dbx.Files.ListFolderAsync(folderName);
                }
                catch (Exception ex)
                {
                    // Si el folder no existe, lanzar una excepción
                    if (ex is ApiException<ListFolderError>)
                    {
                        throw new Exception("El folder especificado no existe.");
                    }
                    else
                    {
                        throw;
                    }
                }

                // Subir el archivo usando DropboxUploader
                return await DropboxUploader.UploadQrCode(qrCodeBitmap, fileName, folderName);
            }
        }
    }
}