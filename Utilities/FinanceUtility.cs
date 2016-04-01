using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KUtility.GovRules;
using KUtility.PreDefined;
using System.Globalization;

namespace KUtility.Utilities
{
    public static class FinanceUtility
    {

        private readonly static int currentFinancialYear = GetCurrentFinancialYear();

        #region public Financial-related utilities/helpers
        /// <summary>
        /// Get current financial year.
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentFinancialYear()
        {
            DateTime today = DateTime.Today;
            if (today.Month >= 7)
            {
                return today.Year + 1;
            }
            return today.Year;
        }

        /// <summary>
        /// Get the last date of a given financial year.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime GetLastDateInFinancialYear(int year)
        {
            if (1000 > year || year > 9999) year = DateTime.Today.Year;
            string date = "30/06/" + year;
            try
            {
                return (DateTime)(date).ToDate("dd/MM/yyyy");
            }
            catch
            {
                return Convert.ToDateTime(date, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Convert a future value to a current value by a given value change per period and total number of periods
        /// </summary>
        /// <param name="futureValue"></param>
        /// <param name="valueChangePerPeriod"></param>
        /// <param name="numberOfPeriods"></param>
        /// <returns></returns>
        public static double ToCurrentValue(this double futureValue, double valueChangePerPeriod, int numberOfPeriods)
        {
            if (futureValue == 0) return 0;
            var temp = Math.Pow((1 + valueChangePerPeriod), numberOfPeriods);
            var currentValue = futureValue / temp;
            return currentValue;
        }


        /// <summary>
        /// Get the maximum salary sacrifice allowed in an age. This rule is designed by the government. Can vary each year. Updates need to be reflected in the code.
        /// Need yearly maintenance.
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static double GetMaxAnnuallySalarySacrifice(int age)
        {
            return SalarySacrificeRule.MaxAnnuallySalarySacrifice(age);
        }


        /// <summary>
        /// Get the employer superannuation contribution rate. This rule is designed by the government. Can vary each year. Updates need to be reflected in the code.
        /// Need yearly maintenance.
        /// </summary>
        /// <returns></returns>
        public static double GetEmployerContributionRate()
        {
            return EmployerContributionRule.EmployerContributionRate();
        }

        /// <summary>
        /// Check with the given info to see if it is eligible for age pension.
        /// </summary>
        /// <param name="dob"></param>
        /// <param name="dobFormat"></param>
        /// <param name="futureDate">get eligibility for this date</param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static bool GetAgePensionEligibility(string dob, string dobFormat, string futureDate, string gender)
        {
            return AgePensionRule.IsEligibleForAgePension(dob, dobFormat, futureDate, gender);
        }

        /// <summary>
        /// Get the asset test's rate of reduction
        /// </summary>
        /// <returns></returns>
        public static double GetAssetTestRateOfReduction()
        {
            return AgePensionRule.AssetTestThreshold.GetAssetTestRateOfReduction();
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
            return AgePensionRule.AssetTestThreshold.GetAssetTestThreshold(isHomeOwner, isMarried);
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
            return AgePensionRule.AssetTestThreshold.GetUpToYearAssetTestThreshold(isHomeOwner, isMarried, year, centerlinkIncreaseRate);
        }

        /// <summary>
        /// Get the deeming threshold for one individual
        /// </summary>
        /// <param name="isMarried"></param>
        /// <returns></returns>
        public static double GetDeemingThreshold(bool isMarried, bool receivedAgePensionBefore)
        {
            return AgePensionRule.DeemingThreshold.GetDeemingThreshold(isMarried, receivedAgePensionBefore);
        }

        /// <summary>
        /// Calculate the deeming income in lower rate. (centerlink)
        /// </summary>
        public static double GetDeemingLowerRate()
        {
            return AgePensionRule.DeemingThreshold.GetDeemingLowerRate();
        }

        /// <summary>
        /// Calculate the deeming income in higher rate. (centerlink)
        /// </summary>
        public static double GetDeemingHigherRate()
        {
            return AgePensionRule.DeemingThreshold.GetDeemingHigherRate();
        }

        /// <summary>
        /// Get the up to date deeming threshold which has taken the centerlink increase rate into account.
        /// </summary>
        /// <param name="isMarried"></param>
        /// <param name="receivedAgePensionBefore"></param>
        /// <param name="centerlinkIncreaseRate"></param>
        /// <param name="year">the year projected</param>
        /// <returns></returns>
        public static double GetUpToYearDeemingThreshold(bool isMarried, bool receivedAgePensionBefore, double centerlinkIncreaseRate, int year)
        {
            return AgePensionRule.DeemingThreshold.GetUpToYearDeemingThreshold(isMarried, receivedAgePensionBefore, centerlinkIncreaseRate, year);
        }

        /// <summary>
        /// Get the annual income test reduction threshold.
        /// </summary>
        /// <param name="isMarried"></param>
        /// <returns></returns>
        public static double GetIncomeReductionThreshold(bool isMarried)
        {
            return AgePensionRule.IncomeTestThreshold.GetIncomeReductionThreshold(isMarried);
        }


        /// <summary>
        /// get the income test's rate of reduction.
        /// </summary>
        /// <returns></returns>
        public static double GetIncomeTestRateOfReduction()
        {
            return AgePensionRule.IncomeTestThreshold.GetIncomeTestRateOfReduction();
        }

        /// <summary>
        /// Get full age pension amount per annum for a single individual
        /// </summary>
        /// <returns></returns>
        public static double GetFullAgePensionSingle()
        {
            return AgePensionRule.fullAgePensionAmoun_single;
        }

        /// <summary>
        /// Get full age pension amount per annum for a couple.
        /// </summary>
        /// <returns></returns>
        public static double GetFullAgePensionCouple()
        {
            return AgePensionRule.fullAgePensionAmoun_couple;
        }

        /// <summary>
        /// Determine whether the person with the given dob should retire or not.
        /// </summary>
        /// <param name="dob"></param>
        /// <param name="dobFormat"></param>
        /// <returns></returns>
        public static int GetRetirementAge(string dob, string dobFormat)
        {
            return RetirementAgeRule.GetRetirementAge(dob, dobFormat);
        }

        /// <summary>
        /// Check if the person with the dob is retired or not.
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        public static bool IsRetired(double currentAge, double retirementAge)
        {
            return RetirementAgeRule.IsRetired(currentAge, retirementAge);
        }


        public static double ToPeriodRate(this double fromRate, PaymentFrequency toFrequency)
        {
            double ratio = 1.0 / (int)toFrequency;
            var toRate = Math.Pow((1 + fromRate), ratio) - 1;
            return toRate;
        }

        /// <summary>
        /// Get next payment date based on this payment date and the frequency of payment.
        /// </summary>
        /// <param name="thisPaymentDate"></param>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public static DateTime? GetNextPaymentDate(DateTime thisPaymentDate, PaymentFrequency frequency)
        {
            DateTime? nextDate = null;
            switch (frequency)
            {
                case PaymentFrequency.Daily: nextDate = thisPaymentDate.AddDays(1); break;
                case PaymentFrequency.Fortnightly: nextDate = thisPaymentDate.AddDays(14); break;
                case PaymentFrequency.Monthly: nextDate = thisPaymentDate.AddMonths(1); break;
                case PaymentFrequency.Quarterly: nextDate = thisPaymentDate.AddMonths(3); break;
                case PaymentFrequency.Weekly: nextDate = thisPaymentDate.AddDays(7); break;
                case PaymentFrequency.Yearly: nextDate = thisPaymentDate.AddYears(1); break;
                case PaymentFrequency.OneOff: nextDate = thisPaymentDate; break;
                default: break;
            }
            return nextDate;
        }

        /// <summary>
        /// Generate a dictionary with DateTime as Key and T as value, and populate every date from the start date to last date in the financial year after the number of years.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="startDate"></param>
        /// <param name="numberOfYears"></param>
        /// <returns></returns>
        public static Dictionary<DateTime, T> GetDailyDictionary<T>(DateTime startDate, int numberOfYears)
        {
            Dictionary<DateTime, T> result = new Dictionary<DateTime, T>();
            if (startDate == null) return result;
            var thisYear = startDate.Year;
            for (var i = 0; i < numberOfYears; i++)
            {
                DateTime lastDateInThisYear = GetLastDateInFinancialYear(thisYear);
                while (startDate < lastDateInThisYear)
                {
                    var value = typeof(T).GetDefaultValue();
                    if (value == null) return result;
                    T v = (T)(object)value;
                    result.Add(startDate, v);
                    startDate = startDate.AddDays(1);
                }
                thisYear++;
            }
            return result;
        }

        /// <summary>
        /// Generate a dictionary with int as Key and T as value, and populate every key from the start year to a certain number of years.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="startYear"></param>
        /// <param name="numberOfYears"></param>
        /// <returns></returns>
        public static Dictionary<int, T> GetYearlyDictionary<T>(int startYear, int numberOfYears)
        {
            Dictionary<int, T> result = new Dictionary<int, T>();
            if (startYear == 0) return result;
            for (var i = 0; i < numberOfYears; i++)
            {
                var value = typeof(T).GetDefaultValue();
                if (value == null) return result;
                T v = (T)(object)value;
                result.Add(startYear, v);
                startYear++;
            }
            return result;
        }


        #endregion
    }
}
