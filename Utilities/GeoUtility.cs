
using KUtility.PreDefined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.Utilities
{
    /// <summary>
    /// Geography utilities
    /// </summary>
    public static class GeoUtility
    {
        #region public utilities regarding geography
        /// <summary>
        /// This method takes a state name in any form: 1/ACT/Australian Capital Territory and return a fullname of the state
        /// </summary>
        /// <param name="anyState"></param>
        /// <returns></returns>
        public static string ConvertToStateFullname(string anyState)
        {
            string stateFullname = AUStatesList.STATE_ERROR_S;
            var stateInt = 0;
            var isInt = Int32.TryParse(anyState, out stateInt);
            var stateIsValid = false;
            //the input is a number
            if (isInt)
            {
                stateIsValid = ValidateAUState(stateInt);
                if (stateIsValid) stateFullname = ConvertState_IntToFullname(stateInt);
            }
            else
            {
                stateIsValid = ValidateAUState(anyState);
                if (stateIsValid)
                {
                    if (anyState.Length <= 3)
                    {
                        //the input is a short name
                        stateFullname = ConvertState_ShortnameToFullname(anyState);
                    }
                    else
                    {
                        stateFullname = anyState;
                    }
                }
            }
            return stateFullname;
        }

        public static string ConvertToStateShortname(string anyState)
        {
            string stateShortname = AUStatesList.STATE_ERROR_S;
            var stateInt = 0;
            var isInt = Int32.TryParse(anyState, out stateInt);
            var stateIsValid = false;
            //the input is a number
            if (isInt)
            {
                stateIsValid = ValidateAUState(stateInt);
                if (stateIsValid) stateShortname = ConvertState_IntToShortname(stateInt);
            }
            else
            {
                stateIsValid = ValidateAUState(anyState);
                if (stateIsValid)
                {
                    if (anyState.Length > 3)
                    {
                        //the input is a short name
                        stateShortname = ConvertState_FullnameToShortname(anyState);
                    }
                    else
                    {
                        stateShortname = anyState;
                    }
                }
            }
            return stateShortname;
        }

        public static int ConvertToStateInteger(string anyState)
        {
            int stateInt = AUStatesList.STATE_ERROR_I;
            var stateParsedInt = 0;
            var isInt = Int32.TryParse(anyState, out stateParsedInt);
            var stateIsValid = false;
            //the input is a number
            if (isInt)
            {
                stateIsValid = ValidateAUState(stateInt);
                if (stateIsValid) stateInt = stateParsedInt;
            }
            else
            {
                stateIsValid = ValidateAUState(anyState);
                if (stateIsValid)
                {
                    if (anyState.Length > 3)
                    {
                        //the input is a full name
                        stateInt = ConvertState_FullnameToInt(anyState);
                    }
                    else
                    {
                        stateInt = ConvertState_ShortnameToInt(anyState);
                    }
                }
            }
            return stateInt;
        }


        /// <summary>
        /// validate string states
        /// </summary>
        /// <param name="anyState">string</param>
        /// <returns></returns>
        public static bool ValidateAUState(string anyState)
        {
            return AUStatesList.States_F.IndexOf(anyState) >= 0 || AUStatesList.States_S.IndexOf(anyState) >= 0;
        }


        /// <summary>
        /// validate int states
        /// </summary>
        /// <param name="anyState">int</param>
        /// <returns></returns>
        public static bool ValidateAUState(int anyState)
        {
            return AUStatesList.States_I.IndexOf(anyState) >= 0;
        }

        #endregion



        #region private methods

        private static string ConvertState_IntToFullname(int stateInt)
        {
            string stateFullname = AUStatesList.STATE_ERROR_S;
            switch (stateInt)
            {
                case AUStatesList.ACT_I: stateFullname = AUStatesList.ACT_F; break;
                case AUStatesList.NSW_I: stateFullname = AUStatesList.NSW_F; break;
                case AUStatesList.NT_I: stateFullname = AUStatesList.NT_F; break;
                case AUStatesList.QLD_I: stateFullname = AUStatesList.QLD_F; break;
                case AUStatesList.SA_I: stateFullname = AUStatesList.SA_F; break;
                case AUStatesList.TAS_I: stateFullname = AUStatesList.TAS_F; break;
                case AUStatesList.VIC_I: stateFullname = AUStatesList.VIC_F; break;
                case AUStatesList.WA_I: stateFullname = AUStatesList.WA_F; break;
            }
            return stateFullname;
        }

        private static int ConvertState_FullnameToInt(string stateFullname)
        {
            int stateInt = -1;
            switch (stateFullname)
            {
                case AUStatesList.ACT_F: stateInt = AUStatesList.ACT_I; break;
                case AUStatesList.NSW_F: stateInt = AUStatesList.NSW_I; break;
                case AUStatesList.NT_F: stateInt = AUStatesList.NT_I; break;
                case AUStatesList.QLD_F: stateInt = AUStatesList.QLD_I; break;
                case AUStatesList.SA_F: stateInt = AUStatesList.SA_I; break;
                case AUStatesList.TAS_F: stateInt = AUStatesList.TAS_I; break;
                case AUStatesList.VIC_F: stateInt = AUStatesList.VIC_I; break;
                case AUStatesList.WA_F: stateInt = AUStatesList.WA_I; break;
            }
            return stateInt;
        }


        private static string ConvertState_IntToShortname(int stateInt)
        {
            string stateShortname = AUStatesList.STATE_ERROR_S;
            switch (stateInt)
            {
                case AUStatesList.ACT_I: stateShortname = AUStatesList.ACT_S; break;
                case AUStatesList.NSW_I: stateShortname = AUStatesList.NSW_S; break;
                case AUStatesList.NT_I: stateShortname = AUStatesList.NT_S; break;
                case AUStatesList.QLD_I: stateShortname = AUStatesList.QLD_S; break;
                case AUStatesList.SA_I: stateShortname = AUStatesList.SA_S; break;
                case AUStatesList.TAS_I: stateShortname = AUStatesList.TAS_S; break;
                case AUStatesList.VIC_I: stateShortname = AUStatesList.VIC_S; break;
                case AUStatesList.WA_I: stateShortname = AUStatesList.WA_S; break;
            }
            return stateShortname;
        }

        private static int ConvertState_ShortnameToInt(string stateShortname)
        {
            int stateInt = -1;
            switch (stateShortname)
            {
                case AUStatesList.ACT_S: stateInt = AUStatesList.ACT_I; break;
                case AUStatesList.NSW_S: stateInt = AUStatesList.NSW_I; break;
                case AUStatesList.NT_S: stateInt = AUStatesList.NT_I; break;
                case AUStatesList.QLD_S: stateInt = AUStatesList.QLD_I; break;
                case AUStatesList.SA_S: stateInt = AUStatesList.SA_I; break;
                case AUStatesList.TAS_S: stateInt = AUStatesList.TAS_I; break;
                case AUStatesList.VIC_S: stateInt = AUStatesList.VIC_I; break;
                case AUStatesList.WA_S: stateInt = AUStatesList.WA_I; break;
            }
            return stateInt;
        }

        private static string ConvertState_ShortnameToFullname(string stateShortname)
        {
            string stateFullname = AUStatesList.STATE_ERROR_S;
            switch (stateShortname)
            {
                case AUStatesList.ACT_S: stateFullname = AUStatesList.ACT_F; break;
                case AUStatesList.NSW_S: stateFullname = AUStatesList.NSW_F; break;
                case AUStatesList.NT_S: stateFullname = AUStatesList.NT_F; break;
                case AUStatesList.QLD_S: stateFullname = AUStatesList.QLD_F; break;
                case AUStatesList.SA_S: stateFullname = AUStatesList.SA_F; break;
                case AUStatesList.TAS_S: stateFullname = AUStatesList.TAS_F; break;
                case AUStatesList.VIC_S: stateFullname = AUStatesList.VIC_F; break;
                case AUStatesList.WA_S: stateFullname = AUStatesList.WA_F; break;
            }
            return stateFullname;
        }


        private static string ConvertState_FullnameToShortname(string stateFullname)
        {
            string stateShortname = AUStatesList.STATE_ERROR_S;
            switch (stateFullname)
            {
                case AUStatesList.ACT_F: stateShortname = AUStatesList.ACT_S; break;
                case AUStatesList.NSW_F: stateShortname = AUStatesList.NSW_S; break;
                case AUStatesList.NT_F: stateShortname = AUStatesList.NT_S; break;
                case AUStatesList.QLD_F: stateShortname = AUStatesList.QLD_S; break;
                case AUStatesList.SA_F: stateShortname = AUStatesList.SA_S; break;
                case AUStatesList.TAS_F: stateShortname = AUStatesList.TAS_S; break;
                case AUStatesList.VIC_F: stateShortname = AUStatesList.VIC_S; break;
                case AUStatesList.WA_F: stateShortname = AUStatesList.WA_S; break;
            }
            return stateShortname;
        }



        #endregion
    }
}
