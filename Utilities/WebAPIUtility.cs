using KConverter.Interface;
using KUtility.WPF.WebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.Utilities
{
    public static class WebAPIUtility
    {

        private readonly static IKConverter convert = new KConverter.Implementation.KConverter();
        /// <summary>
        /// Add parameter to the WebRequestMultiParameterModel which can be used along with the DesktopWebRequest for HTTP POST with multiple parameters.
        /// </summary>
        /// <param name="paras"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        public static void AddParameter<T>(this WebRequestMultiParameterModel paras, string parameterName, T parameterValue)
        {
            if (parameterValue != null)
            {
                paras.parameters.Add(parameterName, new WebRequestParameterModel { jsonValue = convert.ObjectToJsonString(parameterValue) });
            }
        }

        public static async Task<WebRequestMultiParameterModel> GetParameters(this HttpRequestMessage request)
        {
            var stringParams = await request.Content.ReadAsStringAsync();
            WebRequestMultiParameterModel parameters = convert.JsonStringToObject<WebRequestMultiParameterModel>(stringParams);
            return parameters;
        }


        public static T GetParameter<T>(this WebRequestMultiParameterModel parameters, string parameterName)
        {
            WebRequestParameterModel param;
            parameters.parameters.TryGetValue(parameterName, out param);
            if (param == null) return (T)ObjectUtility.GetDefaultValue(typeof(T));
            var value = param.jsonValue;
            var result = convert.JsonStringToObject<T>(value);
            return result;
        }


    }
}
