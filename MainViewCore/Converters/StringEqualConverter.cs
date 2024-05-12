using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MainView.Converters
{
    public class StringEqualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                if (value == null && parameter.ToString() == "Overview")
                    return true;
                return false;
            }
            string TabName = value as string;
            string TabPara = parameter as string;
            if (TabName == TabPara)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;
            string TabName = value as string;
            string TabPara = parameter as string;
            if (TabName == TabPara)
                return true;
            return false;
        }
    }
}
