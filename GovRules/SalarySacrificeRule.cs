using KUtility.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.GovRules
{
    internal static class SalarySacrificeRule
    {
        #region private properties

        //Data for before 2016
        private static readonly int ageSeparator_2016 = 49;
        private static readonly double tier1_2016 = 30000.00;
        private static readonly double tier2_2016 = 35000.00;
        private static readonly int year_2016 = 2016;
        private static int currentFinancialYear = FinanceUtility.GetCurrentFinancialYear();

        #endregion

        #region public methods

        public static double MaxAnnuallySalarySacrifice(int age)
        {
            if (currentFinancialYear <= year_2016)
            {
                if (age <= ageSeparator_2016)
                {
                    return tier1_2016;
                }
                else
                {
                    return tier2_2016;
                }
            }
            //use for future update for new rules in coming years
            else
            {
                if (age <= ageSeparator_2016)
                {
                    return tier1_2016;
                }
                else
                {
                    return tier2_2016;
                }
            }
        }

        #endregion
    }
}
