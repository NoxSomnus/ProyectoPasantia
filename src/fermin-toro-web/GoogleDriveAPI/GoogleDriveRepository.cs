﻿using FerminToroWeb.ApiUrlConfig;
using FerminToroWeb.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;

namespace FerminToroWeb.GoogleDriveAPI
{
    public class GoogleDriveRepository
    {
        //defined scope.
        public static string[] Scopes = { DriveService.Scope.Drive };

        //create Drive API service.
        public static DriveService GetService()
        {
            DriveJsonConfig JsonLocation = new DriveJsonConfig();
            //get Credentials from client_secret.json file 
            UserCredential credential;
            using (var stream = new FileStream("client_secret_813929318264-3ogiponul0mg3gj9ue7ckr5ojg639t1i.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
            {
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

        //get all files from Google Drive.
        public static List<GoogleDriveFiles> GetDriveFiles()
        {
            DriveService service = GetService();

            // define parameters of request.
            FilesResource.ListRequest FileListRequest = service.Files.List();

            //listRequest.PageSize = 10;
            //listRequest.PageToken = 10;
            FileListRequest.Fields = "nextPageToken, files(id, name, size, version, createdTime)";

            //get file list.
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            List<GoogleDriveFiles> FileList = new List<GoogleDriveFiles>();

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    GoogleDriveFiles File = new GoogleDriveFiles
                    {
                        Id = file.Id,
                        Name = file.Name,
                        Size = file.Size,
                        Version = file.Version,
                        CreatedTime = file.CreatedTime
                    };
                    FileList.Add(File);
                }
            }
            return FileList;
        }
        public static string FileUpload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                DriveService service = GetService();

                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "GoogleDriveFiles", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = fileName,
                    Parents = new List<string> { "1W8p67Ugspg9Q8MWtz9YJcRqym09KKnmX" }
                };

                FilesResource.CreateMediaUpload request;

                using (var stream = new FileStream(path, FileMode.Open))
                {
                    request = service.Files.Create(fileMetadata, stream, file.ContentType);
                    request.Fields = "id";
                    request.Upload();
                }

                var uploadedFile = request.ResponseBody;
                string fileId = uploadedFile.Id;
                // Eliminar el archivo local después de la subida
                File.Delete(path);
                return fileId;
            }

            return null;
        }


        //file Upload to the Google Drive Drive Tests.
        public static string FileUploadCSV(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                DriveService service = GetService();

                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "GoogleDriveFiles", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = fileName,
                    Parents = new List<string> { "1W8p67Ugspg9Q8MWtz9YJcRqym09KKnmX" }
                };

                FilesResource.CreateMediaUpload request;

                using (var stream = new FileStream(path, FileMode.Open))
                {
                    request = service.Files.Create(fileMetadata, stream, file.ContentType);
                    request.Fields = "id";
                    request.Upload();
                }

                var uploadedFile = request.ResponseBody;
                string fileId = uploadedFile.Id;
                // Eliminar el archivo local después de la subida
                File.Delete(path);
                return fileId;
            }

            return null;
        }

        //Download file from Google Drive by fileId.
        public static string DownloadGoogleFile(string fileId)
        {
            DriveService service = GetService();

            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "GoogleDriveFiles");
            FilesResource.GetRequest request = service.Files.Get(fileId);

            string FileName = request.Execute().Name;
            string FilePath = Path.Combine(FolderPath, FileName);

            using (MemoryStream stream1 = new MemoryStream())
            {
                // Add a handler which will be notified on progress changes.
                // It will notify on each chunk download and when the
                // download is completed or failed.
                request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                Console.WriteLine(progress.BytesDownloaded);
                                break;
                            }
                        case DownloadStatus.Completed:
                            {
                                Console.WriteLine("Download complete.");
                                SaveStream(stream1, FilePath);
                                break;
                            }
                        case DownloadStatus.Failed:
                            {
                                Console.WriteLine("Download failed.");
                                break;
                            }
                    }
                };
                request.Download(stream1);
            }

            return FilePath;
        }

        // file save to server path
        private static void SaveStream(MemoryStream stream, string FilePath)
        {
            using (System.IO.FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }

        //Delete file from the Google drive
        public static void DeleteFile(GoogleDriveFiles files)
        {
            DriveService service = GetService();
            try
            {
                // Initial validation.
                if (service == null)
                    throw new ArgumentNullException("service");

                if (files == null)
                    throw new ArgumentNullException("file does not exist");

                // Make the request.
                service.Files.Delete(files.Id).Execute();
            }
            catch (Exception ex)
            {
                throw new Exception("Request Files.Delete failed.", ex);
            }
        }
    }
}
