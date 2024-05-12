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
                case ObservableCollection<DriverFirebase>:
                    return ConvertOfObservableCollectionDriver(value);
                case ObservableCollection<TripFirebase>:
                    return ConvertOfObservableCollectionTrip(value);
                default:
                    return Visibility.Collapsed;
            }

        }

        

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        private object ConvertOfObservableCollectionTrip(object value)
        {
            ObservableCollection<TripFirebase> ViewValue = value as ObservableCollection<TripFirebase>;
            if (ViewValue.Count == 0) return Visibility.Visible;
            return Visibility.Collapsed;
        }
        private object ConvertOfObservableCollectionDriver(object value)
        {
            ObservableCollection<DriverFirebase> ViewValue = value as ObservableCollection<DriverFirebase>;
            if (ViewValue.Count == 0) return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertOfObservableCollectionVehicle(object value)
        {
            ObservableCollection<IVehicleDataFirebase> ViewValue = value as ObservableCollection<IVehicleDataFirebase>;
            if (ViewValue.Count == 0) return Visibility.Visible;
            return Visibility.Collapsed;
        }
    }
}
