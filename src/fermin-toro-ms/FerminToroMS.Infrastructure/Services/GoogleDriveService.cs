using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Infrastructure.Services
{
    public class GoogleDriveService : IGoogleDriveService
    {
        //defined scope.
        public static string[] Scopes = { DriveService.Scope.Drive };

        //create Drive API service.
        public static DriveService GetService()
        {
            //get Credentials from client_secret.json file 
            UserCredential credential;
            using (var stream = new FileStream("client_secret_813929318264-3ogiponul0mg3gj9ue7ckr5ojg639t1i.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
            {
                var baa = "aa";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore("DriveServiceCredentials", true)).Result;
            }

            //create Drive API service.
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleDriveRestAPI-v3",
            });
            return service;
        }

        public byte[] DownloadFile(string fileId)
        {
            DriveService service = GetService();

            var request = service.Files.Get(fileId);

            using (var memoryStream = new MemoryStream())
            {
                request.Download(memoryStream);
                memoryStream.Position = 0; // Reiniciar la posición del stream al principio

                return memoryStream.ToArray();
            }
        }
    }
}
