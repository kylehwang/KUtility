using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KUtility.WPF.WebService.Model
{

    public class RTFModel
    {

        public string RTFFormatedString { get; set; }

        public TextDataFormat TextFormat { get { return TextDataFormat.Rtf; } }

    }
}
