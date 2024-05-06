﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using ViewModels.State.Authentication;
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

        private readonly IAuthenticator _authenticator;

        public bool IsLoggedIn => _authenticator._isLogin;
        //public bool IsLoggedIn = true;

        public MainViewModel (INavigator navigator, IAuthenticator authenticator)
        {
            Navigation = navigator;
            _authenticator = authenticator;
            Navigator.NavigateSwitch(Navigation, ViewType.Login);

            _authenticator.StateChanged += Authenticator_StateChanged;

            CloseWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteCloseWindowCommand(p));

            MaximizeWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteMaximizeWindowCommand(p));

            MinimizeWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteMinimizeWindowCommand(p));

            MouseMoveWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteMouseMoveWindowCommand(p));

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
        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }
        public override void Dispose()
        {
            _authenticator.StateChanged -= Authenticator_StateChanged;

            base.Dispose();
        }

    }
}
