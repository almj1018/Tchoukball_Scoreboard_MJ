using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tchoukball_Scoreboard_MJ.Converter
{
    public class TimeSpanToTextControlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan ts = (TimeSpan)value;
            return ts.ToString("mm\\:ss\\.ff");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan.TryParseExact(value.ToString(), "mm\\:ss\\.ff", CultureInfo.InvariantCulture, out var ts);
            return ts;
        }
    }
}
