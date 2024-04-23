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
        public ICommand AddDriverCommand { get; }
        public DriverListViewModel(INavigator navigator)
        {
            Navigation = navigator;
            AddDriverCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                //Window window = Window.GetWindow(p);
                //var viewModel = (MainViewModel)window.DataContext;
                //viewModel.CurrentViewModel = new AddDriverViewModel();
                Navigation.NavigateTo<AddDriverViewModel>();
            });
        }
    }
}