using System.Globalization;
using System.Windows.Data;

namespace Tchoukball_Scoreboard_MJ.Converter;

public class TimeSpanToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        TimeSpan ts = (TimeSpan)value;
        return ts.ToString("mm\\:ss");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        TimeSpan.TryParseExact(value.ToString(), "mm\\:ss", CultureInfo.InvariantCulture, out var ts);
        return ts;
    }
}
