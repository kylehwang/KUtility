using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.Utilities
{
    public static class StringUtility
    {

        #region Public utilities, helpers, extensions

        /// <summary>
        /// Convert a string to a date with a given format indicating the format of the given date. 
        /// For example, date format could be "dd/MM/yyyy", "yyyy-MM-dd" etcs.
        /// If conversion fails, return null.
        /// </summary>
        /// <param name="dateString"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        public static DateTime? ToDate(this string dateString, string dateFormat)
        {
            DateTime? date = null;
            try
            {
                date = DateTime.ParseExact(dateString, dateFormat, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
            }
            return date;
        }

        /// <summary>
        /// Convert a string to a date. This method doesn't guarantee the correctness of the conversion as a format is not given.
        /// If fail to convert, return null.
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime? ToDate(this string dateString)
        {
            DateTime? date = null;
            try
            {
                date = Convert.ToDateTime(dateString);
            }
            catch (Exception)
            {
            }
            return date;
        }

        public static T ToEnum<T>(this string enumString)
        {
            var enumResult = (T)Enum.Parse(typeof(T), enumString);
            return enumResult;
        }

        public static byte[] ToByte(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        #endregion

    }
}
