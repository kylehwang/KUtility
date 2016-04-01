using KUtility.PreDefined;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.Utilities
{
    public static class GenericUtility
    {

        #region Generic utilities/helper/extensions

        /// <summary>
        /// Calculates the current age for the given dob.
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        public static double GetCurrentAge(string dob, string dobFormat)
        {
            DateTime today = DateTime.Today;
            return GetAge(dob, today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), "dd/MM/yyyy");
        }

        public static double GetCurrentAge(DateTime dob, string dobFormat)
        {
            DateTime today = DateTime.Today;
            return GetAge(dob.ToString(dobFormat, CultureInfo.InvariantCulture), today.ToString(dobFormat, CultureInfo.InvariantCulture), dobFormat);
        }


        public static int GetAge(string dob, string futureDate,string dobFormat)
        {

            DateTime? bday = dob.ToDate(dobFormat);
            DateTime? fday = futureDate.ToDate(dobFormat);
            if (bday == null || fday == null) return -1;
            DateTime birthday = (DateTime)bday;
            DateTime futureDay = (DateTime)fday;
            int integerAge = futureDay.Year - birthday.Year;
            int bornedMonth = birthday.Month - futureDay.Month;
            if (bday > futureDay.AddYears(-integerAge))
            {
                integerAge--;
            }
            return integerAge;
        }

        /// <summary>
        /// Get current year.
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentYear()
        {
            DateTime today = DateTime.Today;
            return today.Year;
        }


        /// <summary>
        /// Get the current life expectency of an individual.
        /// </summary>
        /// <param name="age"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static double GetLifeExpectency(int age, string gender)
        {
            return LifeExpectency.GetLifeExpectency(age, gender);
        }

        #endregion

    }
}
