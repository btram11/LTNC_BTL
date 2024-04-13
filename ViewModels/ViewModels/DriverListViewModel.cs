using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace ViewModels
{
    public class DriverListViewModel : ViewModelBase
    {
        public ICommand AddDriverCommand { get; }
        public DriverListViewModel()
        {
            AddDriverCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                Window window = Window.GetWindow(p);
                var viewModel = (MainViewModel)window.DataContext;
                viewModel.currentViewModel = new AddDriverViewModel();
            });
        }
    }
}