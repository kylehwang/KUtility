using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using KUtility.Utilities;

namespace KUtility.Test
{
    [TestClass]
    public class UtitlityTest
    {

        [TestMethod]
        public void GetAgeTest()
        {
            //preparation
            string dob1 = "04/09/1989";
            string dob2 = "03/09/1989";
            string dob3 = "02/09/1989";

            var futureDay = "03/09/2015";

            string dobFormat = "dd/MM/yyyy";

            int age1 = GenericUtility.GetAge(dob1, futureDay, dobFormat);
            int age2 = GenericUtility.GetAge(dob2, futureDay, dobFormat);
            int age3 = GenericUtility.GetAge(dob3, futureDay, dobFormat);

            Assert.AreEqual(age1, 25);
            Assert.AreEqual(age2, 26);
            Assert.AreEqual(age3, 26);
            
        }

    }
}
