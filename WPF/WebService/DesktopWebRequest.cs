using KConverter.Interface;
using KUtility.Utilities;
using KUtility.WPF.WebService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KUtility.WPF.WebService
{
    public class DesktopWebRequest
    {
        private HttpWebRequest request;
        private Stream dataStream;
        private WebRequestMultiParameterModel parameters = new WebRequestMultiParameterModel();

        private readonly IKConverter converter = new KConverter.Implementation.KConverter();

        private static readonly string AppFolderHeader = "appfolder";
        private static readonly string FilenameHeader = "filename";
        private static readonly string UsernameHeader = "username";
        private static readonly string PasswordHeader = "password";

        private string downloadPath;

        private bool isUploadFile = false;

        private bool needToWriteParameter = false;

        private string status;

        public String Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        /// <summary>
        /// initiate the url request
        /// </summary>
        /// <param name="url"></param>
        public DesktopWebRequest(string url)
        {
            //create a request using a given url
            request = (HttpWebRequest)WebRequest.Create(url);
        }

        /// <summary>
        /// Initiate a GET or POST. the method could only be GET/POST
        /// </summary>
        /// <param name="url">target url string</param>
        /// <param name="method">GET/POST</param>
        public DesktopWebRequest(string url, string method)
            : this(url)
        {
            //FILE is not a valid HTTP request, we convert it back to POST
            if (method.Equals(RequestMethod.FILE)) method = RequestMethod.POST;
            if (method.Equals(RequestMethod.GET) || method.Equals(RequestMethod.POST) || method.Equals(RequestMethod.FILE))
            {
                request.Method = method;
            }
            else
            {
                throw new Exception("Invalid Method Type");
            }
        }

        /// <summary>
        /// Initiate a GET or POST with the given data. the method could only be GET/POST. If need to pass multiple parameters, please set the third parameter to String.empty / null, 
        /// then use AddParameter method to add a parameter each time.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        public DesktopWebRequest(string url, string method, string data)
            : this(url, method)
        {

            request.ProtocolVersion = HttpVersion.Version11;

            if (method == RequestMethod.FILE)
            {
                if (!string.IsNullOrEmpty(data))
                {
                    isUploadFile = true;

                    //get the file as stream to put in requestStream
                    Stream fs = File.OpenRead(data);
                    using (var requestStream = request.GetRequestStream())
                    {
                        byte[] fileDataInByte = null;
                        using (BinaryReader binaryReader = new BinaryReader(fs))
                        {
                            fileDataInByte = binaryReader.ReadBytes((int)fs.Length);
                        }
                        requestStream.Write(fileDataInByte, 0, fileDataInByte.Length);
                    }
                }
                else
                {
                    request.ContentLength = 0;
                }
                //set contentType
                request.ContentType = "application/octet-stream";
            }
            else
            {
                // Create POST data and convert it to a byte array.
                string postData = data;
                if (!string.IsNullOrEmpty(postData))
                {
                    WriteDataToRequest(postData);
                }
                // Set the ContentType property of the WebRequest.
                //request.ContentType = "application/x-www-form-urlencoded";
                request.ContentType = "application/json; charset=utf-8";

            }
        }


        /// <summary>
        /// Get the web response in string format.
        /// </summary>
        /// <returns></returns>
        public string GetResponse()
        {

            if (isUploadFile)
            {
                throw new Exception("For file upload, please use UploadFile() or UploadFile(string appfolder, string filename) instead.");
            }
            if (needToWriteParameter)
            {
                WriteDataToRequest(converter.ObjectToJsonString(parameters));
            }
            try
            {
                // Get the original response.
                WebResponse response = request.GetResponse();

                this.Status = ((HttpWebResponse)response).StatusDescription;

                // Get the stream containing all content returned by the requested server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);

                // Read the content fully up to the end.
                string responseFromServer = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (Exception e)
            {
                var msg = e.Message;
                Console.WriteLine(msg);
                return string.Empty;
            }
        }

        /// <summary>
        /// Upload file to the server and get response from it.
        /// </summary>
        /// <returns></returns>
        public string UploadFile()
        {
            if (!request.Headers.AllKeys.Contains(FilenameHeader))
                throw new Exception("You need to provide a file name; Call SetFileName(string filename) first.");
            isUploadFile = false;
            var response = GetResponse();
            isUploadFile = true;
            return response;
        }

        /// <summary>
        /// Upload file to the server and get response from it.
        /// </summary>
        /// <param name="appFolder"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string UploadFile(string appFolder, string filename)
        {
            SetFileName(appFolder, filename);
            return UploadFile();
        }

        /// <summary>
        /// Download a file from the server by an appFolder and a filename
        /// </summary>
        /// <param name="appFolder"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool DownloadFile(string appFolder, string filename)
        {
            if (string.IsNullOrEmpty(this.downloadPath)) throw new Exception("Please specify a path for downloaded files.");
            SetFileName(appFolder, filename);
            // Get the original response.
            WebResponse response = request.GetResponse();
            // Check status is OK
            this.Status = ((HttpWebResponse)response).StatusDescription;
            var stream = response.GetResponseStream();
            // save stream as file
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        byte[] buf = new byte[1024];
                        count = stream.Read(buf, 0, 1024);
                        ms.Write(buf, 0, count);
                    } while (stream.CanRead && count > 0);

                    using (var fileStream = File.Create(SystemUtility.ConstructPath(downloadPath, filename)))
                    {
                        ms.Seek(0, SeekOrigin.Begin);
                        ms.CopyTo(fileStream);
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void SetDownloadPath(string path)
        {
            this.downloadPath = path;
        }

        /// <summary>
        /// Provide your app folder to be created on the server, and provide the filename for your file to be saved in the server.
        /// </summary>
        /// <param name="appFolder"></param>
        /// <param name="filename"></param>
        public void SetFileName(string appFolder, string filename)
        {
            SetAppFolder(appFolder);
            request.Headers.Add(FilenameHeader, filename);
        }

        public void SetFileName(string filename)
        {
            request.Headers.Add(FilenameHeader, filename);
        }

        public void SetAppFolder(string appFolder)
        {
            request.Headers.Add(AppFolderHeader, appFolder);
        }

        public void SetAuthentication(string username, string password)
        {
            request.Headers.Add(UsernameHeader, username);
            request.Headers.Add(PasswordHeader, password);
        }

        public void AddParameter<T>(string parameterName, T parameterValue)
        {
            parameters.AddParameter<T>(parameterName, parameterValue);
            needToWriteParameter = true;
        }

        private void WriteDataToRequest(string data)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(data);

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();
        }

    }

    public static class RequestMethod
    {
        public static string POST = "POST";

        public static string GET = "GET";

        public static string FILE = "FILE";
    }
}
