using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace KUtility.WPF.WebService
{
    public class BinaryMediaTypeFormatter : MediaTypeFormatter
    {

        private static readonly Type supportType = typeof(byte[]);

        public BinaryMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
        }

        public override bool CanReadType(Type type)
        {
            return supportType == type;
        }

        public override bool CanWriteType(Type type)
        {
            return supportType == type;
        }


        #region Override stream formatter

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {
            Task<object> readTask = GetReadTask(readStream);
            readTask.Start();
            return readTask;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext)
        {
            if (value == null) value = new byte[0];

            Task writeTask = GetWriteTask(writeStream, (byte[])value);

            writeTask.Start();

            return writeTask;
        }

        #endregion

        #region Actual Read & Write Task

        private Task<object> GetReadTask(Stream stream)
        {
            return new Task<object>(() =>
            {
                var ms = new MemoryStream();
                stream.CopyTo(ms);
                return ms.ToArray();
            });
        }


        private Task GetWriteTask(Stream stream, byte[] data)
        {
            return new Task(() =>
            {
                var ms = new MemoryStream(data);
                ms.CopyTo(stream);
            });
        } 
        #endregion
    }
}
