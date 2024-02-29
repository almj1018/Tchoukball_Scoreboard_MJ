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
            string returnVal;
            if (StaticSettings.CountdownByMilliseconds)
                returnVal = ts.ToString("mm\\:ss\\.ff");
            else
                returnVal = ts.ToString("mm\\:ss");
            return returnVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan ts;
            if (StaticSettings.CountdownByMilliseconds)
                TimeSpan.TryParseExact(value.ToString(), "mm\\:ss\\.ff", CultureInfo.InvariantCulture, out ts);     
            else
                TimeSpan.TryParseExact(value.ToString(), "mm\\:ss", CultureInfo.InvariantCulture, out ts);
            return ts;
        }
    }
}
