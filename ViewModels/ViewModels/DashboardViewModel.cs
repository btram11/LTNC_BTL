using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.State.Navigators;

namespace ViewModels
{
    public class DashboardViewModel: ViewModelBase
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
        public DashboardViewModel(INavigator navigator)
        {
            Navigation = navigator;
        }

        ~DashboardViewModel()
        {

        }
    }
}
