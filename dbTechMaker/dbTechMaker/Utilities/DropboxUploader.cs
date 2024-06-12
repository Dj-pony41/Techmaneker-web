using System;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace dbTechMaker.Utilities
{
    public class DropboxUploader
    {
        private static string AccessToken = "sl.B3BCScwGz-AFyTOvSUXECqCEHpvydBuGOmdNT9Rk_ecWLpespvV4vvYj-1e4a4xEuISRtMY5DIPrCR6il38tFLGJzZtv5JrQmsDr_IPHLXotWDuPq1sSSqJ4BJC5HnXaRur8-aTWJf_bMjXsjTeCZNU";
        private static string RefreshToken = "Udd11QFC4REAAAAAAAAAAe6XyXP-OyvQZv58T1fb2yYEwgGTYkAYZHnPMZ2z42L-";
        private static string ClientId = "kzixxhfczl0w98f";
        private static string ClientSecret = "vxo9r7l8jglivur";


        public static async Task<string> UploadQrCode(Bitmap qrCodeBitmap, string fileName, string folderName)
        {
            await EnsureAccessToken();

            // Generar un identificador único de 7 caracteres
            string uniqueIdentifier = Guid.NewGuid().ToString("N").Substring(0, 7);

            // Asegurarse de que el nombre del archivo tenga la extensión .png y agregar el identificador único
            fileName = $"{fileName}-{uniqueIdentifier}.png";

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
                    if (ex is ApiException<ListFolderError>)
                    {
                        await dbx.Files.CreateFolderV2Async(new CreateFolderArg(folderName));
                    }
                    else
                    {
                        throw;
                    }
                }

                using (var memoryStream = new MemoryStream())
                {
                    // Guardar el Bitmap en el MemoryStream en formato PNG
                    qrCodeBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    memoryStream.Position = 0; // Resetear la posición del MemoryStream

                    // Subir la imagen a Dropbox
                    var updated = await dbx.Files.UploadAsync(
                        $"{folderName}/{fileName}",
                        WriteMode.Overwrite.Instance,
                        body: memoryStream);

                    // Obtener el enlace compartido directo para la imagen subida
                    var sharedLink = await dbx.Sharing.CreateSharedLinkWithSettingsAsync(updated.PathLower);
                    string sharedUrl = sharedLink.Url.Replace("?dl=0", ""); // Eliminar el fragmento de consulta '?dl=0'

                    // Reemplazar www.dropbox.com por dl.dropboxusercontent.com para obtener el enlace directo
                    sharedUrl = sharedUrl.Replace("www.dropbox.com", "dl.dropboxusercontent.com");

                    return sharedUrl;
                }
            }
        }





        //public static async Task<string> UploadQrCode(Bitmap qrCodeBitmap, string fileName, string folderName)
        //{
        //    // Generar un identificador único de 7 caracteres
        //    string uniqueIdentifier = Guid.NewGuid().ToString("N").Substring(0, 7);

        //    // Asegurarse de que el nombre del archivo tenga la extensión .png y agregar el identificador único
        //    fileName = $"{fileName}-{uniqueIdentifier}.png";

        //    using (var dbx = new DropboxClient(AccessToken))
        //    {
        //        // Asegurarse de que el folderName empiece con '/'
        //        if (!folderName.StartsWith("/"))
        //        {
        //            folderName = "/" + folderName;
        //        }

        //        try
        //        {
        //            // Verificar si el folder existe
        //            var listFolderResult = await dbx.Files.ListFolderAsync(folderName);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Si el folder no existe, crear el folder
        //            if (ex is ApiException<ListFolderError>)
        //            {
        //                await dbx.Files.CreateFolderV2Async(new CreateFolderArg(folderName));
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        using (var memoryStream = new MemoryStream())
        //        {
        //            // Guardar el Bitmap en el MemoryStream en formato PNG
        //            qrCodeBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
        //            memoryStream.Position = 0; // Resetear la posición del MemoryStream

        //            // Subir la imagen a Dropbox
        //            var updated = await dbx.Files.UploadAsync(
        //                $"{folderName}/{fileName}",
        //                WriteMode.Overwrite.Instance,
        //                body: memoryStream);

        //            // Obtener el enlace compartido directo para la imagen subida
        //            var sharedLink = await dbx.Sharing.CreateSharedLinkWithSettingsAsync(updated.PathLower);
        //            string sharedUrl = sharedLink.Url.Replace("?dl=0", ""); // Eliminar el fragmento de consulta '?dl=0'

        //            // Reemplazar www.dropbox.com por dl.dropboxusercontent.com para obtener el enlace directo
        //            sharedUrl = sharedUrl.Replace("www.dropbox.com", "dl.dropboxusercontent.com");

        //            return sharedUrl;
        //        }
        //    }
        //}

        private static async Task EnsureAccessToken()
        {
            // Check if the token is expired or about to expire and refresh it
            if (TokenNeedsRefresh())
            {
                using (var httpClient = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.dropbox.com/oauth2/token");
                    var content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"grant_type", "refresh_token"},
                        {"refresh_token", RefreshToken},
                        {"client_id", ClientId},
                        {"client_secret", ClientSecret}
                    });
                    request.Content = content;

                    var response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var responseJson = JObject.Parse(responseBody);

                    AccessToken = responseJson["access_token"].ToString();
                }
            }
        }

        private static bool TokenNeedsRefresh()
        {
            // Implement logic to check if the token is expired or about to expire.
            // This could be based on a stored expiry time or a simple flag.
            // For simplicity, we assume it needs refresh every time.
            return true;
        }
    }
}