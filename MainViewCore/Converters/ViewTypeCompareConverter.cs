using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ViewModels.State.Navigators;

namespace MainView.Converters
{
    public class ViewTypeCompareConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ViewType && parameter is ViewType)
            {
                return (ViewType)value == (ViewType)parameter;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && parameter is ViewType)
            {
                if ((bool)value)
                {
                    return (ViewType)parameter;
                }
            }
            return ViewType.Home;
        }
    }
}
