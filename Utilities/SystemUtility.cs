using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.IO;

namespace KUtility.Utilities
{
    public class SystemUtility
    {

        /// <summary>
        /// Return the path that represents the current system desktop.
        /// </summary>
        /// <returns></returns>
        public static string GetDesktopPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        /// <summary>
        /// Return the path where the current web app is hosted in the server. If failed, return an empty string.
        /// </summary>
        /// <returns></returns>
        public static string GetWebAppServerPath()
        {
            try
            {
                return HttpContext.Current.Server.MapPath("~");
            }
            catch
            {
                return GetDesktopPath();
            }
        }

        /// <summary>
        /// Get the execution path of the application. If failed, return an empty string.
        /// </summary>
        /// <returns>Path to debug folder</returns>
        public static string GetDebugFolder()
        {
            try
            {
                return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Return a valid file path constructing from the args
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static string ConstructPath(string p1, string p2)
        {
            if (!Directory.Exists(p1))
            {
                Directory.CreateDirectory(p1);
            }
            return Path.Combine(p1, p2);
        }

        /// <summary>
        /// Return a valid file path constructing from the args
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static string ConstructPath(string p1, string p2, string p3)
        {
            var p12 = ConstructPath(p1, p2);
            return ConstructPath(p12, p3);
        }

        /// <summary>
        /// Return a valid file path constructing from the args
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        public static string ConstructPath(string p1, string p2, string p3, string p4)
        {
            var p123 = ConstructPath(p1, p2, p3);
            return ConstructPath(p123, p4);
        }

        public static string ConstructPath(string p1, string p2, string p3, string p4, string p5)
        {
            var p1234 = ConstructPath(p1, p2, p3, p4);
            return ConstructPath(p1234, p5);
        }

    }
}
