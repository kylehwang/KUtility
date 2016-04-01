using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KUtility.Utilities;

namespace KUtility.GovRules
{
    internal static class EmployerContributionRule
    {
        #region private properties

        private static readonly int year_2016 = 2016;
        private static readonly double contributionRate = 0.095;
        private static int currentFinancialYear = FinanceUtility.GetCurrentFinancialYear();

        #endregion

        #region public methods

        public static double EmployerContributionRate()
        {
            if (currentFinancialYear <= year_2016)
            {
                return contributionRate;
            }
            //use for future update for upcoming years.
            else
            {
                return contributionRate;
            }
        }

        #endregion

    }
}
