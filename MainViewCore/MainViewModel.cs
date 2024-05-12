using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using ViewModels;
using ViewModels.State.Authentication;
using ViewModels.State.Navigators;

namespace MainView
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

        private readonly IAuthenticator _authenticator;
        public bool IsLoggedIn => _authenticator._isLogin;
        public Account CurrentAccount => _authenticator.CurrentAccount;
        #region Private Fields
        //private WindowResizer resizer;
        private Window curWindow;
        private int curWindowRadius = 10;
        private int curOuterMarginSize = 10;
        /// <summary>
        /// The last known dock position
        /// </summary>
        private WindowDockPosition curDockPosition = WindowDockPosition.Undocked;
        #endregion

        #region Properties
        public bool Borderless { get { return (curWindow.WindowState == WindowState.Maximized || curDockPosition != WindowDockPosition.Undocked); } }

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder { get; set; } = 6;
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding { get { return new Thickness(0); } }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                // If it is maximized or docked, no border
                return Borderless ? 0 : curOuterMarginSize;
            }
            set
            {
                curOuterMarginSize = value;
            }
        }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }
        public int WindowRadius
        {
            get
            {
                // If it is maximized or docked, no border
                return Borderless ? 0 : curWindowRadius;
            }
            set
            {
                curWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }


        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 35;
        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public GridLength TitleHeightGridLength 
        { 
            get 
            {
                return new GridLength(TitleHeight + ResizeBorder); 
            } 
        }
        #endregion

        public MainViewModel(INavigator navigator, IAuthenticator authenticator, MainWindow window)
        {
            Navigation = navigator;
            _authenticator = authenticator;
            curWindow = window;
            Navigator.NavigateSwitch(Navigation, ViewType.Login);
            _authenticator.StateChanged += Authenticator_StateChanged;
            // Listen out for the window resizing
            curWindow.StateChanged += (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                WindowResized();
            };


            CloseWindowCommand = new RelayCommand((p) => curWindow.Close());

            MaximizeWindowCommand = new RelayCommand((p) => curWindow.WindowState ^= WindowState.Maximized);

            MinimizeWindowCommand = new RelayCommand((p) => curWindow.WindowState = WindowState.Minimized);

            UpdateViewModelCommand = new RelayCommand<ViewType>((p) => {
                if (p == ViewType.Login)
                {
                    _authenticator.LogOut();
                }
                Navigator.NavigateSwitch(Navigation, p);
             });


            var resizer = new WindowResizer(curWindow);

            // Listen out for dock changes
            resizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                curDockPosition = dock;

                // Fire off resize events
                WindowResized();
            };
        }

        #region WindowCommand
        public ICommand CloseWindowCommand { get; }
        public ICommand MinimizeWindowCommand { get; }
        public ICommand MaximizeWindowCommand { get; }
        public ICommand UpdateViewModelCommand { get; }
        #endregion
        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(CurrentAccount));
        }

        private void WindowResized()
        {
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }
        public override void Dispose()
        {
            _authenticator.StateChanged -= Authenticator_StateChanged;

            curWindow.StateChanged -= (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                WindowResized();
            };

            base.Dispose();
        }

    }
}
