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

namespace ViewModels
{
    public class MainViewModel : ViewModelBase 
    {
        public ViewModelBase CurrentViewModel { get; private set; }
        public ViewModelBase currentViewModel
        {
            get => CurrentViewModel;
            set
            {
                CurrentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public ViewModelBase menuBar { get; private set; }
        public ViewModelBase accessBar { get; private set; }

        public MainViewModel ()
        {
            menuBar = new MenuLeftBarViewModel();
            accessBar = new QuickAccessButtonViewModel();
            CurrentViewModel = new DashboardViewModel();

            

            CloseWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteCloseWindowCommand(p));

            MaximizeWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteMaximizeWindowCommand(p));

            MinimizeWindowCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => ExecuteMinimizeWindowCommand(p));

            AttachmentButtonCommand = new RelayCommand<Window>((p) => { return p != null ? true : false; }, (p) => AttachmentButton());
        }

        #region WindowCommand
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
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
        #endregion

        /// <summary>
        /// The command for when the attachment button is clicked
        /// </summary>
        public ICommand AttachmentButtonCommand { get; set; }

        /// <summary>
        /// True to show the attachment menu, false to hide it
        /// </summary>
        public bool AttachmentMenuVisible { get; set; }

        /// <summary>
        /// When the attachment button is clicked show/hide the attachment popup
        /// </summary>
        public void AttachmentButton()
        {
            // Toggle menu visibility
            AttachmentMenuVisible ^= true;
        }
    }
}
