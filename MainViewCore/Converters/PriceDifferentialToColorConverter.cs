using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MainView.Converters
{
    public class PriceDifferentialToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (value is string && value?.ToString() == "0")
            {
                return null;
            }
            else if (value is string && value.ToString().StartsWith("-"))
            {
                return Brushes.SpringGreen;
            }
            else
            {
                return Brushes.OrangeRed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
