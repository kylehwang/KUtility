using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KUtility.Utilities;

namespace KUtility.GovRules
{
    class RetirementAgeRule
    {


        private static string RetirementAgeDateFrom1 = "01/01/1900";
        private static string RetirementAgeDateTo1 = "30/06/1960";
        private static int RetirementAge1 = 55;
        private static string RetirementAgeDateFrom2 = "01/07/1960";
        private static string RetirementAgeDateTo2 = "30/06/1961";
        private static int RetirementAge2 = 56;
        private static string RetirementAgeDateFrom3 = "01/07/1961";
        private static string RetirementAgeDateTo3 = "30/06/1962";
        private static int RetirementAge3 = 57;
        private static string RetirementAgeDateFrom4 = "01/07/1962";
        private static string RetirementAgeDateTo4 = "30/06/1963";
        private static int RetirementAge4 = 58;
        private static string RetirementAgeDateFrom5 = "01/07/1963";
        private static string RetirementAgeDateTo5 = "30/06/1964";
        private static int RetirementAge5 = 59;
        private static string RetirementAgeDateFrom6 = "01/07/1964";
        private static int RetirementAge6 = 60;

        private static DateTime rule1DateFrom;
        private static DateTime rule2DateFrom;
        private static DateTime rule3DateFrom;
        private static DateTime rule4DateFrom;
        private static DateTime rule5DateFrom;
        private static DateTime rule6DateFrom;
        private static DateTime rule1DateTo;
        private static DateTime rule2DateTo;
        private static DateTime rule3DateTo;
        private static DateTime rule4DateTo;
        private static DateTime rule5DateTo;

        /// <summary>
        /// Initialise the age pension rules datefrom and dateto if they are null
        /// </summary>
        private static void InitDates()
        {
            if (rule1DateFrom == null)
            {
                try
                {
                    rule1DateFrom = (DateTime)RetirementAgeDateFrom1.ToDate("dd/MM/yyyy");
                    rule2DateFrom = (DateTime)RetirementAgeDateFrom2.ToDate("dd/MM/yyyy");
                    rule3DateFrom = (DateTime)RetirementAgeDateFrom3.ToDate("dd/MM/yyyy");
                    rule4DateFrom = (DateTime)RetirementAgeDateFrom4.ToDate("dd/MM/yyyy");
                    rule5DateFrom = (DateTime)RetirementAgeDateFrom5.ToDate("dd/MM/yyyy");
                    rule6DateFrom = (DateTime)RetirementAgeDateFrom6.ToDate("dd/MM/yyyy");
                    rule1DateTo = (DateTime)RetirementAgeDateTo1.ToDate("dd/MM/yyyy");
                    rule2DateTo = (DateTime)RetirementAgeDateTo2.ToDate("dd/MM/yyyy");
                    rule3DateTo = (DateTime)RetirementAgeDateTo3.ToDate("dd/MM/yyyy");
                    rule4DateTo = (DateTime)RetirementAgeDateTo4.ToDate("dd/MM/yyyy");
                    rule5DateTo = (DateTime)RetirementAgeDateTo5.ToDate("dd/MM/yyyy");
                }
                catch (FormatException)
                {
                    //error occurs -- Format
                }
                catch (ArgumentNullException)
                {
                    //error occurs -- parameters
                }
            }
        }

        /// <summary>
        /// Check if the person with the dob is retired or not.
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        public static bool IsRetired(double currentAge, double retirementAge)
        {
            if (currentAge >= retirementAge)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get retirement age of an individual based on his/her dob.
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        public static int GetRetirementAge(string dob, string dobFormat)
        {
            InitDates();
            DateTime bday = (DateTime)dob.ToDate(dobFormat);
            DateTime[] dateFroms = { rule1DateFrom, rule2DateFrom, rule3DateFrom, rule4DateFrom, rule5DateFrom, rule6DateFrom };
            DateTime[] dateTos = { rule1DateTo, rule2DateTo, rule3DateTo, rule4DateTo, rule5DateTo, DateTime.Today };
            int[] retirementAges = { RetirementAge1, RetirementAge2, RetirementAge3, RetirementAge4, RetirementAge5, RetirementAge6 };
            int retirementAge = 0;
            for (int i = 0; i < dateFroms.Count(); i++)
            {
                retirementAge = GetRetirementAgeByRules(dateFroms[i], dateTos[i], bday, retirementAges[i]);
                if (retirementAge != 0)
                {
                    break;
                }
            }
            return retirementAge;
        }

        /// <summary>
        /// a function to get the retirement age by comparing birthday and the rules.
        /// </summary>
        /// <param name="datefrom"></param>
        /// <param name="dateto"></param>
        /// <param name="bday"></param>
        /// <param name="retirementAge"></param>
        /// <returns></returns>
        private static int GetRetirementAgeByRules(DateTime datefrom, DateTime dateto, DateTime bday, int retirementAge)
        {
            if (bday >= datefrom && bday <= dateto)
            {
                return retirementAge;
            }
            return 0;
        }


    }
}
