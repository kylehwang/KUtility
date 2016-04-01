using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.PreDefined
{
    public static class AUStatesList
    {
        public const string ACT_F = "Australian Capital Territory";
        public const string NSW_F = "New South Wales";
        public const string NT_F = "Northern Territory";
        public const string QLD_F = "Queensland";
        public const string SA_F = "South Australia";
        public const string TAS_F = "Tasmania";
        public const string VIC_F = "Victoria";
        public const string WA_F = "Western Australia";

        public static List<string> States_F = new List<string>()
        {
            ACT_F,NSW_F,NT_F,QLD_F,SA_F,TAS_F,VIC_F,WA_F
        };

        public const string ACT_S = "ACT";
        public const string NSW_S = "NSW";
        public const string NT_S = "NT";
        public const string QLD_S = "QLD";
        public const string SA_S = "SA";
        public const string TAS_S = "TAS";
        public const string VIC_S = "VIC";
        public const string WA_S = "WA";

        public static List<string> States_S = new List<string>()
        {
            ACT_S,NSW_S,NT_S,QLD_S,SA_S,TAS_S,VIC_S,WA_S
        };

        public const int ACT_I = 0;
        public const int NSW_I = 1;
        public const int NT_I = 2;
        public const int QLD_I = 3;
        public const int SA_I = 4;
        public const int TAS_I = 5;
        public const int VIC_I = 6;
        public const int WA_I = 7;

        public static List<int> States_I = new List<int>() { 0,1,2,3,4,5,6,7 };

        public const string STATE_ERROR_S = "State Error";
        public const int STATE_ERROR_I = -1;
    }
}
