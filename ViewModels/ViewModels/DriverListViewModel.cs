using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ViewModels.State.Navigators;

namespace ViewModels
{
    public class DriverListViewModel : ViewModelBase
    {
        private INavigator _navigation;
        public INavigator Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged(nameof(Navigation));
            }
        }
        public ICommand UpdateViewCommand { get; }
        public DriverListViewModel(INavigator navigator)
        {
            Navigation = navigator;
            UpdateViewCommand = new RelayCommand<ViewType>((p) =>
            {
                Navigator.NavigateSwitch(Navigation, p);
            });
        }
    }
}