using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.WPF.WebService.Model
{
    public class WebRequestMultiParameterModel
    {

        public Dictionary<string, WebRequestParameterModel> parameters { get; set; }

        public WebRequestMultiParameterModel()
        {
            parameters = new Dictionary<string, WebRequestParameterModel>();
        }

    }

    public class WebRequestParameterModel
    {
        public string jsonValue{get;set;}
    }
}
