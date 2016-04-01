using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KUtility.Utilities;
using KUtility.PreDefined;

namespace KUtility.GovRules
{
    internal static class AgePensionRule
    {

        #region Private properties

        private static readonly int year_2016 = 2016;
        private static readonly string AgePensionDateFrom1 = "01/01/1900";
        private static readonly string AgePensionDateTo1 = "31/12/1948";
        private static readonly float MaleAgePensionAge1 = 65;
        private static readonly float FemaleAgePensionAge1 = 64.5f;
        private static readonly string AgePensionDateFrom2 = "01/01/1949";
        private static readonly string AgePensionDateTo2 = "30/06/1952";
        private static readonly float MaleAgePensionAge2 = 65;
        private static readonly float FemaleAgePensionAge2 = 65;
        private static readonly string AgePensionDateFrom3 = "01/07/1952";
        private static readonly string AgePensionDateTo3 = "31/12/1953";
        private static readonly float MaleAgePensionAge3 = 65.5f;
        private static readonly float FemaleAgePensionAge3 = 65.5f;
        private static readonly string AgePensionDateFrom4 = "01/01/1954";
        private static readonly string AgePensionDateTo4 = "30/06/1955";
        private static readonly float MaleAgePensionAge4 = 66;
        private static readonly float FemaleAgePensionAge4 = 66;
        private static readonly string AgePensionDateFrom5 = "01/07/1955";
        private static readonly string AgePensionDateTo5 = "31/12/1956";
        private static readonly float MaleAgePensionAge5 = 66.5f;
        private static readonly float FemaleAgePensionAge5 = 66.5f;
        private static readonly string AgePensionDateFrom6 = "01/01/1957";
        private static readonly float MaleAgePensionAge6 = 67;
        private static readonly float FemaleAgePensionAge6 = 67;

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

        private static double _fullAgePensionAmoun_single;
        private static double _fullAgePensionAmoun_couple;
        public static double fullAgePensionAmoun_single { get { return 782.20; } private set { _fullAgePensionAmoun_single = 782.20; } }
        public static double fullAgePensionAmoun_couple { get { return 1179.20; } private set { _fullAgePensionAmoun_couple = 1179.20; } }
        #endregion

        #region public methods

        /// <summary>
        /// Take a dob (dd/mm/yyyy) to test whether the person is eligible for age pension. 
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        public static bool IsEligibleForAgePension(string dob, string dobFormat, string futureDate, string gender)
        {
            InitRuleDates();
            DateTime? birthday = dob.ToDate(dobFormat);
            double currentAge = GenericUtility.GetAge(dob, futureDate, dobFormat);
            if (birthday == null)
            {
                return false;
            }
            DateTime bday = (DateTime)birthday;
            DateTime[] dateFroms = { rule1DateFrom, rule2DateFrom, rule3DateFrom, rule4DateFrom, rule5DateFrom, rule6DateFrom };
            DateTime[] dateTos = { rule1DateTo, rule2DateTo, rule3DateTo, rule4DateTo, rule5DateTo, DateTime.Today };
            float[] maleAgePensionAges = { MaleAgePensionAge1, MaleAgePensionAge2, MaleAgePensionAge3, MaleAgePensionAge4, MaleAgePensionAge5, MaleAgePensionAge6 };
            float[] femaleAgePensionAges = { FemaleAgePensionAge1, FemaleAgePensionAge2, FemaleAgePensionAge3, FemaleAgePensionAge4, FemaleAgePensionAge5, FemaleAgePensionAge6 };
            bool res = false;
            for (var i = 0; i < dateFroms.Count(); i++)
            {
                res = DetermineAgePensionByRules(dateFroms[i], dateTos[i], bday, gender, currentAge, maleAgePensionAges[i], femaleAgePensionAges[i]);
                if (res)
                {
                    break;
                }
            }
            return res;
        }


        #endregion

        #region private helpers

        /// <summary>
        /// Init rule dates.
        /// </summary>
        private static void InitRuleDates()
        {
            try
            {
                rule1DateFrom = (DateTime)AgePensionDateFrom1.ToDate("dd/MM/yyyy");
                rule2DateFrom = (DateTime)AgePensionDateFrom2.ToDate("dd/MM/yyyy");
                rule3DateFrom = (DateTime)AgePensionDateFrom3.ToDate("dd/MM/yyyy");
                rule4DateFrom = (DateTime)AgePensionDateFrom4.ToDate("dd/MM/yyyy");
                rule5DateFrom = (DateTime)AgePensionDateFrom5.ToDate("dd/MM/yyyy");
                rule6DateFrom = (DateTime)AgePensionDateFrom6.ToDate("dd/MM/yyyy");
                rule1DateTo = (DateTime)AgePensionDateTo1.ToDate("dd/MM/yyyy");
                rule2DateTo = (DateTime)AgePensionDateTo2.ToDate("dd/MM/yyyy");
                rule3DateTo = (DateTime)AgePensionDateTo3.ToDate("dd/MM/yyyy");
                rule4DateTo = (DateTime)AgePensionDateTo4.ToDate("dd/MM/yyyy");
                rule5DateTo = (DateTime)AgePensionDateTo5.ToDate("dd/MM/yyyy");
            }
            catch (FormatException)
            {
                //error occurs -- Format
            }
            catch (ArgumentNullException)
            {
                //error occurs -- parameters
            }
            catch (Exception)
            {
                //other errors
            }
        }

        /// <summary>
        /// Determine whether a person has reached his/her age pension age.
        /// </summary>
        /// <param name="dateFrom">Age pension rules: date from</param>
        /// <param name="dateTo">Age pension rules: date to</param>
        /// <param name="bday">birthday</param>
        /// <param name="gender"></param>
        /// <param name="currentAge"></param>
        /// <param name="maleAgePensionAge"></param>
        /// <param name="femaleAgePensionAge"></param>
        /// <returns></returns>
        private static bool DetermineAgePensionByRules(DateTime dateFrom, DateTime dateTo, DateTime bday, string gender, double currentAge, float maleAgePensionAge, float femaleAgePensionAge)
        {
            if (bday >= dateFrom && bday <= dateTo)
            {
                if (gender == Gender.MALE)
                {
                    if (currentAge >= MaleAgePensionAge2)
                    {
                        return true;
                    }
                }
                else if (gender == Gender.FEMALE)
                {
                    if (currentAge >= FemaleAgePensionAge2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region Class for asset test threshold
        internal static class AssetTestThreshold
        {

            private static int threshold1 = 202000;
            private static int threshold2 = 286500;
            private static int threshold3 = 348500;
            private static int threshold4 = 433000;

            private static double assetTestRateOfReduction = 1.5;

            /// <summary>
            /// Get the asset test's rate of reduction
            /// </summary>
            /// <returns></returns>
            public static double GetAssetTestRateOfReduction()
            {
                return assetTestRateOfReduction;
            }

            /// <summary>
            /// Get asset test threshold for calculating age pension based on two conditions. Home owner and marriage status.
            /// This is based on current year data without considering centerlink rate changing.
            /// </summary>
            /// <param name="isHomeOwner"></param>
            /// <param name="isMarried"></param>
            /// <returns></returns>
            public static int GetAssetTestThreshold(bool isHomeOwner, bool isMarried)
            {
                if (isHomeOwner && isMarried)
                {
                    return threshold2;
                }
                else if (isHomeOwner)
                {
                    return threshold1;
                }
                else if (isMarried)
                {
                    return threshold4;
                }
                return threshold3;
            }


            /// <summary>
            /// Calculates the actual threshold for the given year.
            /// </summary>
            /// <param name="basedYearThreshold"></param>
            /// <param name="year"></param>
            /// <param name="centerlinkIncreaseRate"></param>
            /// <returns></returns>
            public static double GetUpToYearAssetTestThreshold(bool isHomeOwner, bool isMarried, int year, double centerlinkIncreaseRate)
            {
                double basedYearThreshold = GetAssetTestThreshold(isHomeOwner, isMarried);
                int currentYear = GenericUtility.GetCurrentYear();
                int yearDifference = year - currentYear;
                double threshold = basedYearThreshold * Math.Pow((1 + centerlinkIncreaseRate), yearDifference);
                return threshold;
            }

        }
        #endregion

        #region Class for deeming test threshold
        internal class DeemingThreshold
        {

            private static double deemingThreshold_single = 48000;
            private static double deemingThreshold_married_first = 39800;
            private static double deemingThreshold_married_notFirst = 79600;
            private static double deemingLowerRate = 0.0175;
            private static double deemingHigherRate = 0.0325;

            /// <summary>
            /// Get the deeming threshold for one individual
            /// </summary>
            /// <param name="isMarried"></param>
            /// <returns></returns>
            public static double GetDeemingThreshold(bool isMarried, bool receivedAgePensionBefore)
            {
                if (isMarried)
                {
                    if (!receivedAgePensionBefore)
                    {
                        return deemingThreshold_married_first;
                    }
                    else
                    {
                        return deemingThreshold_married_notFirst;
                    }
                }
                return deemingThreshold_single;
            }

            public static double GetDeemingLowerRate()
            {
                return deemingLowerRate;
            }

            public static double GetDeemingHigherRate()
            {
                return deemingHigherRate;
            }

            /// <summary>
            /// Get the up to date deeming threshold which has taken the centerlink increase rate into account.
            /// </summary>
            /// <param name="isMarried"></param>
            /// <param name="receivedAgePensionBefore"></param>
            /// <param name="centerlinkIncreaseRate"></param>
            /// <param name="year">the year projected, for example 2020</param>
            /// <returns></returns>
            public static double GetUpToYearDeemingThreshold(bool isMarried, bool receivedAgePensionBefore, double centerlinkIncreaseRate, int year)
            {
                double basedYearDeemingThreshold = GetDeemingThreshold(isMarried, receivedAgePensionBefore);
                int currentYear = GenericUtility.GetCurrentYear();
                int yearDifference = year - currentYear;
                double deemingThreshold = basedYearDeemingThreshold * Math.Pow((1 + centerlinkIncreaseRate), yearDifference);
                return deemingThreshold;
            }

        }
        #endregion

        #region Class for income test threshold
        internal class IncomeTestThreshold
        {
            private static double incomeReductionThreshold_single_fortnight = 160;
            private static double incomeReductionThreshold_couple_fortnight = 284;
            private static double incomeTestRateOfReduction = 0.5;

            /// <summary>
            /// Get the annual income test reduction threshold.
            /// </summary>
            /// <param name="isMarried"></param>
            /// <returns></returns>
            public static double GetIncomeReductionThreshold(bool isMarried)
            {
                if (isMarried)
                {
                    return incomeReductionThreshold_couple_fortnight * 26;
                }
                return incomeReductionThreshold_single_fortnight * 26;
            }

            /// <summary>
            /// get the income test's rate of reduction.
            /// </summary>
            /// <returns></returns>
            public static double GetIncomeTestRateOfReduction()
            {
                return incomeTestRateOfReduction;
            }
        }
        #endregion
    }
}
