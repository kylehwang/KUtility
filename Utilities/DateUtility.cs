using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.Utilities
{
    public static class DateUtility
    {

        /// <summary>
        /// Return the total number of days in that year.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int NumberOfDaysInYear(this DateTime date)
        {
            if (date == null) return 365;
            bool isLeapYear = DateTime.IsLeapYear(date.Year);
            int numberOfDays = isLeapYear ? 366 : 365;
            return numberOfDays;
        }

        /// <summary>
        /// Mutate the given date by adding days.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="days"></param>
        public static void AddDaysToMe(this DateTime date, int days)
        {
            date = date.AddDays(days);
        }

        /// <summary>
        /// Get all days in the month of the given date
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public static List<DateTime> GetDaysInMonthByADate(this DateTime aDate)
        {
            var days = new List<DateTime>();
            var year = aDate.Year;
            var month = aDate.Month;
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                    .Select(day => new DateTime(year, month, day)) // Map each day to a date
                    .ToList();
        }
    }
}
