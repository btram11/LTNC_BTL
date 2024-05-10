using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MainView.Converters
{
    public class RegularVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value)
            {
                case ObservableCollection<IVehicleDataFirebase>:
                    return ConvertOfObservableCollectionVehicle(value);
                default:
                    return Visibility.Collapsed;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertOfObservableCollectionVehicle(object value)
        {
            ObservableCollection<IVehicleDataFirebase> ViewValue = value as ObservableCollection<IVehicleDataFirebase>;
            if (ViewValue.Count == 0) return Visibility.Visible;
            return Visibility.Collapsed;
        }
    }
}
