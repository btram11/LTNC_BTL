//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Automation;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Media.Media3D;
//using System.Windows.Navigation;
//using ViewModels.State.Authentication;
//using ViewModels.State.Navigators;

//namespace ViewModels
//{
//    public class MainViewModel : ViewModelBase
//    {
//        private INavigator _navigation;
//        public INavigator Navigation
//        {
//            get => _navigation;
//            set
//            {
//                _navigation = value;
//                OnPropertyChanged(nameof(Navigation));
//            }
//        }

//        private readonly IAuthenticator _authenticator;
//        public bool IsLoggedIn => _authenticator._isLogin;
//        //public bool IsLoggedIn = true;


//        #region Private Fields
//        private Window curWindow /*=> Application.Current.MainWindow*/;
//        private int curWindowRadius = 12;
//        private int curOuterMarginSize = 10;
//        /// <summary>
//        /// The last known dock position
//        /// </summary>
//        private WindowDockPosition curDockPosition = WindowDockPosition.Undocked;
//        private WindowState _windowState;
//        #endregion



//        #region Properties
//        public bool Borderless { get { return (curWindow.WindowState == WindowState.Maximized || curDockPosition != WindowDockPosition.Undocked); } }

//        /// <summary>
//        /// The size of the resize border around the window
//        /// </summary>
//        public int ResizeBorder { get; set; } = 6;
//        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

//        /// <summary>
//        /// The margin around the window to allow for a drop shadow
//        /// </summary>
//        public int OuterMarginSize
//        {
//            get
//            {
//                // If it is maximized or docked, no border
//                return Borderless ? 0 : curOuterMarginSize;
//            }
//            set
//            {
//                curOuterMarginSize = value;
//            }
//        }

//        /// <summary>
//        /// The margin around the window to allow for a drop shadow
//        /// </summary>
//        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }
//        public int WindowRadius
//        {
//            get
//            {
//                // If it is maximized or docked, no border
//                return Borderless ? 0 : curWindowRadius;
//            }
//            set
//            {
//                curWindowRadius = value;
//            }
//        }
//        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

//        public WindowState WindowState
//        {
//            set
//            {
//                _windowState = value;
//            }
//        }
//        #endregion
//        private WindowResizer resizer = null;
//        public MainViewModel(INavigator navigator, IAuthenticator authenticator, Window window)
//        {
//            Navigation = navigator;
//            _authenticator = authenticator;
//            curWindow = window;
//            Navigator.NavigateSwitch(Navigation, ViewType.Login);

//            _authenticator.StateChanged += Authenticator_StateChanged;

//            CloseWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteCloseWindowCommand(p));

//            MaximizeWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteMaximizeWindowCommand(p));

//            MinimizeWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteMinimizeWindowCommand(p));

//            MouseMoveWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteMouseMoveWindowCommand(p));

//            UpdateViewModelCommand = new RelayCommand<object>((p) => { return true; }, (p) => Navigator.NavigateSwitch(Navigation, p));
//            Console.WriteLine(Application.Current);

//            // Listen for the main window loaded event
//            Application.Current.Activated += Current_Activated;

//            //Application.Current.MainWindow.StateChanged += (sender, e) =>
//            //{
//            //    // Fire off events for all properties that are affected by a resize
//            //    WindowResized();
//            //};

//            //// Fix window resize issue
//            //var resizer = new WindowResizer(Application.Current.MainWindow);

//            //// Listen out for dock changes
//            //resizer.WindowDockChanged += (dock) =>
//            //{
//            //    // Store last position
//            //    curDockPosition = dock;

//            //    // Fire off resize events
//            //    WindowResized();
//            //};
//            MainWindowStateChangedCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
//            {
//                if (resizer == null) resizer = new WindowResizer(Application.Current.MainWindow);
//                resizer.WindowDockChanged += (dock) =>
//                {
//                    // Store last position
//                    curDockPosition = dock;

//                    // Fire off resize events
//                    WindowResized();
//                };
//            });
//        }

//        private void Current_Activated(object sender, EventArgs e)
//        {
//            if (Application.Current.MainWindow != null)
//            {
//                // Listen for the loaded event
//                Application.Current.MainWindow.Loaded += MainWindow_Loaded;
//            }
//        }

//        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
//        {
//            // Fire off events for all properties that are affected by a resize
//            WindowResized();

//            // Fix window resize issue
//            var resizer = new WindowResizer(Application.Current.MainWindow);

//            // Listen out for dock changes
//            resizer.WindowDockChanged += (dock) =>
//            {
//                // Store last position
//                curDockPosition = dock;

//                // Fire off resize events
//                WindowResized();
//            };
//        }

//        private void WindowResized()
//        {
//            OnPropertyChanged(nameof(Borderless));
//            OnPropertyChanged(nameof(ResizeBorderThickness));
//            OnPropertyChanged(nameof(OuterMarginSize));
//            OnPropertyChanged(nameof(OuterMarginSizeThickness));
//            OnPropertyChanged(nameof(WindowRadius));
//            OnPropertyChanged(nameof(WindowCornerRadius));
//        }

//        #region WindowCommand
//        public ICommand MainWindowStateChangedCommand { get; }
//        public ICommand CloseWindowCommand { get; }
//        public ICommand MinimizeWindowCommand { get; }
//        public ICommand MaximizeWindowCommand { get; }
//        public ICommand MouseMoveWindowCommand { get; }
//        public ICommand UpdateViewModelCommand { get; }
//        private void ExecuteCloseWindowCommand(Window curwindow)
//        {
//            if (curwindow != null)
//            {
//                curwindow.Close();
//            }
//        }

//        private void ExecuteMinimizeWindowCommand(Window curwindow)
//        {
//            if (curwindow != null)
//            {
//                if (curwindow.WindowState != WindowState.Minimized)
//                {
//                    curwindow.WindowState = WindowState.Minimized;
//                }
//                else
//                {
//                    curwindow.WindowState = WindowState.Maximized;
//                }
//            }
//        }

//        private void ExecuteMaximizeWindowCommand(Window curwindow)
//        {
//            if (curwindow != null)
//            {
//                if (curwindow.WindowState != WindowState.Maximized)
//                {
//                    curwindow.WindowState = WindowState.Maximized;
//                }
//                else
//                {
//                    curwindow.WindowState = WindowState.Normal;
//                }
//            }
//        }

//        private void ExecuteMouseMoveWindowCommand(Window curwindow)
//        {
//            if (curwindow != null)
//            {
//                curwindow.DragMove();
//            }
//        }
//        #endregion
//        private void Authenticator_StateChanged()
//        {
//            OnPropertyChanged(nameof(IsLoggedIn));
//        }
//        public override void Dispose()
//        {
//            _authenticator.StateChanged -= Authenticator_StateChanged;

//            base.Dispose();
//        }

//    }
//}
