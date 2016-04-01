using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using KUtility.Utilities;
using System.Windows.Markup;

namespace KUtility.WPF.UI.Converter
{
    public class DateConverter : MarkupExtension, IValueConverter
    {
        private readonly static string dateFormat = "dd/MM/yyyy";
        private readonly static CultureInfo culture = CultureInfo.InvariantCulture;
        private static DateConverter _converter = null;
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null) _converter = new DateConverter();
            return _converter;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Func<DateTime, string> ToDateString = (date) => { return date.ToString(dateFormat, culture); };
            if (value != null)
                return ToDateString((DateTime)value);
            return ToDateString(DateTime.Today);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateValue = value.ToString();
            return dateValue.ToDate(dateFormat);
        }
    }
}
