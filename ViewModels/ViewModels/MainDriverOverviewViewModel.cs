using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels.DriverViewModel;

namespace ViewModels
{
    public class MainDriverOverviewViewModel: ViewModelBase
    {
        private ViewModelBase _tabView;

        public ViewModelBase TabView
        {
            get { return _tabView; }
            set
            {
                _tabView = value;
                OnPropertyChanged(nameof(TabView));
            }
        }
        public ICommand OverviewTabCommand { get; }
        public ICommand AssignmentHistoryTabCommand { get; }
        public MainDriverOverviewViewModel()
        {
            TabView = new OverviewDriverViewModel();

            OverviewTabCommand = new RelayCommand<UserControl>((p) =>
            {
                TabView = new OverviewDriverViewModel();
            });

            AssignmentHistoryTabCommand = new RelayCommand<UserControl>((p) =>
            {
                TabView = new AssignmentHistoryViewModel();
            });
        }
    }
}
