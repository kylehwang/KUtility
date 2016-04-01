using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.WPF.UI
{
    public class BindableFilter:BindableBase
    {
        private string filter;
        public string Filter
        {
            get { return filter; }
            set
            {
                SetProperty(ref filter, value);
            }
        }
    }
}
