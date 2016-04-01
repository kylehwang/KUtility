using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.Utilities
{
    public class FileUtility
    {

        /// <summary>
        /// Read a txt file as string.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadStringFromTxt(string path)
        {
            try
            {
                var result = System.IO.File.ReadAllText(path);
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Write a string to a file
        /// </summary>
        /// <param name="str"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SaveStringAsFile(string str, string path)
        {
            try
            {
                System.IO.File.WriteAllText(path, str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Write a byte array to a file
        /// </summary>
        /// <param name="ba"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SaveByteArrayAsFile(byte[] ba, string path)
        {
            try
            {
                System.IO.File.WriteAllBytes(path, ba);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public static string UploadFileFromClient(byte[] fileContent, string appFolder, string filename)
        {
            if (fileContent != null)
            {
                if (fileContent.Length > 0)
                {
                    string webServerPath = SystemUtility.GetWebAppServerPath();
                    string defaultFolder = "Uploaded Files";
                    var filePath = string.IsNullOrEmpty(appFolder) ?
                        SystemUtility.ConstructPath(webServerPath, defaultFolder, filename) :
                        SystemUtility.ConstructPath(webServerPath, defaultFolder, appFolder, filename);
                    //filename = validateAndFixFileName(filename);
                    //file.SaveAs(filePath);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        Stream stream = new MemoryStream(fileContent);
                        stream.CopyTo(fs);
                        stream.Close();
                    }
                    return filePath;
                }
            }
            return "";
        }

        public static HttpResponseMessage DownLoadFile(string appfolder, string filename)
        {
            string webServerPath = SystemUtility.GetWebAppServerPath();
            string defaultFolder = "Uploaded Files";
            var path = string.IsNullOrEmpty(appfolder) ? SystemUtility.ConstructPath(webServerPath, defaultFolder, filename) :
                        SystemUtility.ConstructPath(webServerPath, defaultFolder, appfolder, filename);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var stream = new FileStream(path, FileMode.Open);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
                return result;
            }
            catch (FileNotFoundException)
            {
                result.Content = new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes("File not found on alfa focus server...")));
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
                return result;
            }
            catch (Exception ex)
            {
                result.Content = new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(ex.Message)));
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
                return result;
            }
        }

        public static List<KUtility.WPF.WebService.Model.FileModel> GetListOfFilesInUploadFolder(string appFolder)
        {
            string webServerPath = SystemUtility.GetWebAppServerPath();
            string defaultFolder = "Uploaded Files";
            var directoryPath = string.IsNullOrEmpty(appFolder) ?
                        SystemUtility.ConstructPath(webServerPath, defaultFolder) :
                        SystemUtility.ConstructPath(webServerPath, defaultFolder, appFolder);
            var fileModelList = new List<KUtility.WPF.WebService.Model.FileModel>();
            try
            {
                var fileList = Directory.GetFiles(directoryPath);
                var i = 0;
                foreach (var item in fileList)
                {
                    var filename = Path.GetFileNameWithoutExtension(item);
                    var createDate = File.GetCreationTime(item);
                    var filetype = Path.GetExtension(item);
                    var index = i++;
                    KUtility.WPF.WebService.Model.FileModel file = new KUtility.WPF.WebService.Model.FileModel
                    {
                        CreateDate = createDate.ToString("yyyy-MM-dd"),
                        Filename = filename,
                        Index = i,
                        FileType = filetype
                    };
                    fileModelList.Add(file);
                }
                return fileModelList;
            }
            catch
            {
                return fileModelList;
            }
        }

        public static bool DeleteFile(string filepath, string filename)
        {
            string webServerPath = SystemUtility.GetWebAppServerPath();
            string defaultFolder = "Uploaded Files";
            var fullpath = string.IsNullOrEmpty(filepath) ?
                        SystemUtility.ConstructPath(webServerPath, defaultFolder, filename) :
                        SystemUtility.ConstructPath(webServerPath, defaultFolder, filepath, filename);
            try
            {
                File.Delete(fullpath);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
