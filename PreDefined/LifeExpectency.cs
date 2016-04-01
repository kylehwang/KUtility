using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.PreDefined
{
    class LifeExpectency
    {
        private readonly static int[] age = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100 };
        private readonly static double[] maleExpectency = { 79.5, 78.9, 77.9, 77, 76, 75, 74, 73, 72, 71, 70, 69, 68, 67, 66, 65.1, 64.1, 63.1, 62.1, 61.2, 60.2, 59.2, 58.3, 57.3, 56.3, 55.4, 54.4, 53.5, 52.5, 51.5, 50.6, 49.6, 48.7, 47.7, 46.7, 45.8, 44.8, 43.9, 42.9, 42, 41, 40.1, 39.1, 38.2, 37.3, 36.3, 35.4, 34.5, 33.6, 32.7, 31.7, 30.8, 29.9, 29.1, 28.2, 27.3, 26.4, 25.5, 24.7, 23.8, 23, 22.1, 21.3, 20.5, 19.7, 18.9, 18.1, 17.3, 16.5, 15.7, 15, 14.3, 13.6, 12.9, 12.2, 11.5, 10.9, 10.2, 9.6, 9, 8.5, 7.9, 7.4, 6.9, 6.5, 6, 5.6, 5.3, 4.9, 4.5, 4.2, 3.9, 3.7, 3.5, 3.3, 3.1, 3, 2.9, 2.7, 2.6, 2.5 };
        private readonly static double[] femaleExpectency = { 84, 83.3, 82.4, 81.4, 80.4, 79.4, 78.4, 77.4, 76.4, 75.4, 74.4, 73.4, 72.4, 71.4, 70.4, 69.4, 68.5, 67.5, 66.5, 65.5, 64.5, 63.5, 62.5, 61.6, 60.6, 59.6, 58.6, 57.6, 56.6, 55.7, 54.7, 53.7, 52.7, 51.7, 50.8, 49.8, 48.8, 47.8, 46.9, 45.9, 44.9, 44, 43, 42.1, 41.1, 40.1, 39.2, 38.2, 37.3, 36.4, 35.4, 34.5, 33.5, 32.6, 31.7, 30.8, 29.9, 28.9, 28, 27.1, 26.2, 25.3, 24.5, 23.6, 22.7, 21.8, 21, 20.1, 19.3, 18.4, 17.6, 16.8, 16, 15.2, 14.4, 13.7, 12.9, 12.2, 11.5, 10.8, 10.1, 9.4, 8.8, 8.2, 7.6, 7.1, 6.6, 6.1, 5.6, 5.2, 4.8, 4.5, 4.1, 3.8, 3.6, 3.4, 3.2, 3, 2.9, 2.8, 2.7 };

        /// <summary>
        /// Get life expectency.
        /// </summary>
        /// <param name="age"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static double GetLifeExpectency(int age, string gender)
        {
            return age + GetYearsToLive(age, gender);
        }

        /// <summary>
        /// Get how many years to live according to life expectency
        /// </summary>
        /// <param name="age"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        private static double GetYearsToLive(int age, string gender)
        {
            if (gender == Gender.FEMALE)
            {
                return femaleExpectency[age];
            }
            else
            {
                return maleExpectency[age];
            }
        }
    }
}
