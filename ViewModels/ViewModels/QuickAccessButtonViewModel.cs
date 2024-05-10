using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ViewModels.State.Navigators;

namespace ViewModels
{
    public class QuickAccessButtonViewModel: ViewModelBase
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
        public ICommand UpdateViewModelCommand { get; }
        public QuickAccessButtonViewModel(INavigator navigator)
        {
            Navigation = navigator;
            UpdateViewModelCommand = new RelayCommand<ViewType>((p) =>
            {
                Navigator.NavigateSwitch(Navigation, p);
            });
        }
    }
}
