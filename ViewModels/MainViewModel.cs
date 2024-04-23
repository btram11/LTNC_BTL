using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using ViewModels.State.Navigators;

namespace ViewModels
{
    public class MainViewModel : ViewModelBase 
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

        //private ViewModelBase currentViewModel;
        //public ViewModelBase CurrentViewModel
        //{
        //    get => Navigation.CurrentViewModel;
        //    set
        //    {
        //        currentViewModel?.Dispose();
        //        currentViewModel = value;
        //        OnPropertyChanged(nameof(CurrentViewModel));
        //    }
        //}
        //public ViewModelBase menuBar { get; }
        //public ViewModelBase accessBar { get; }

        public MainViewModel (INavigator navigator)
        {
            Navigation = navigator;
            //menuBar = new MenuLeftBarViewModel();
            //accessBar = new QuickAccessButtonViewModel();
            ////CurrentViewModel = new DashboardViewModel();
            //currentViewModel = new MainVehicleOverviewViewModel();
            Navigator.NavigateSwitch(Navigation, ViewType.Home);

            CloseWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteCloseWindowCommand(p));

            MaximizeWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteMaximizeWindowCommand(p));

            MinimizeWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteMinimizeWindowCommand(p));

            MouseMoveWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteMouseMoveWindowCommand(p));

            //AttachmentButtonCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => AttachmentButton());
            UpdateViewModelCommand = new RelayCommand<object>((p) => { return true; }, (p) => Navigator.NavigateSwitch(Navigation, p));
        }

        #region WindowCommand
        public ICommand CloseWindowCommand { get; }
        public ICommand MinimizeWindowCommand { get; }
        public ICommand MaximizeWindowCommand { get; }
        public ICommand MouseMoveWindowCommand { get; }
        public ICommand UpdateViewModelCommand { get; }
        private void ExecuteCloseWindowCommand(Window curwindow)
        {
            if (curwindow != null)
            {
                curwindow.Close();
            }
        }

        private void ExecuteMinimizeWindowCommand(Window curwindow)
        {
            if (curwindow != null)
            {
                if (curwindow.WindowState != WindowState.Minimized)
                {
                    curwindow.WindowState = WindowState.Minimized;
                }
                else
                {
                    curwindow.WindowState = WindowState.Maximized;
                }
            }
        }

        private void ExecuteMaximizeWindowCommand(Window curwindow)
        {
            if (curwindow != null)
            {
                if (curwindow.WindowState != WindowState.Maximized)
                {
                    curwindow.WindowState = WindowState.Maximized;
                }
                else
                {
                    curwindow.WindowState = WindowState.Normal;
                }
            }
        }

        private void ExecuteMouseMoveWindowCommand(Window curwindow)
        {
            if (curwindow != null)
            {
                curwindow.DragMove();
            }
        }
        #endregion

        ///// <summary>
        ///// The command for when the attachment button is clicked
        ///// </summary>
        //public ICommand AttachmentButtonCommand { get; set; }

        ///// <summary>
        ///// True to show the attachment menu, false to hide it
        ///// </summary>
        //public bool AttachmentMenuVisible { get; set; }

        ///// <summary>
        ///// When the attachment button is clicked show/hide the attachment popup
        ///// </summary>
        //public void AttachmentButton()
        //{
        //    // Toggle menu visibility
        //    AttachmentMenuVisible ^= true;
        //}
    }
}
