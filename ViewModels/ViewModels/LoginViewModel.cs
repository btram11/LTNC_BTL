using GoogleApi.Entities.Search.Video.Common;
using Models.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using ViewModels.Commands;
using ViewModels.State.Authentication;
using ViewModels.State.Navigators;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace ViewModels
{
    public class LoginViewModel: ViewModelBase, INotifyDataErrorInfo
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private readonly ValidationHelper Helper = new ValidationHelper();

        public bool HasErrors => Helper.HasErrors;
        public IEnumerable GetErrors(string propertyName)
        {
            return Helper.GetErrors(propertyName);
        }

        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;


        [Required(ErrorMessage = "Please enter Username/Email")]
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                Helper.ClearErrors(nameof(Username));
                Validate(nameof(Username), value);
                OnPropertyChanged(nameof(Username));
            }
        }
        [Required(ErrorMessage = "Please enter Password")]
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                Helper.ClearErrors(nameof(Password));
                Validate(nameof(Password), value);
                OnPropertyChanged(nameof(Password));
            }
        }    
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }    
        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }


        


        public LoginViewModel(INavigator navigator, IAuthenticator authenticator)
        {
            _navigator = navigator;
            _authenticator = authenticator;
            LoginCommand = new AsyncRelayCommand(ExecuteLoginCommand);
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) => ClearingAllInputField());
            UpdateViewModelCommand = new RelayCommand<object>((p) => { return true; }, (p) => Navigator.NavigateSwitch(_navigator, p));
            WindowCloseCommand = new RelayCommand<UserControl>((p) => { return p != null ? true : false; }, (p) => ExecuteCloseWindowCommand(p));
            WindowMinimizeCommand = new RelayCommand<UserControl>((p) => { return p != null ? true : false; }, (p) => ExecuteMinimizeWindowCommand(p));

            Helper.ErrorsChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(Helper));
                ErrorsChanged?.Invoke(this, e);
            };
        }

        private void ClearingAllInputField()
        {
            Username = null;
            Password = null;
            Helper.ClearAllErrors();
        }

        #region Commands
        public ICommand LoginCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand UpdateViewModelCommand { get; }
        public ICommand WindowCloseCommand { get; }
        public ICommand WindowMinimizeCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
       

        private async Task ExecuteLoginCommand()
        {
            bool HasValidationError = false;
            for (int i = 0; i < 11; i++)
            {
                if (ValidationSwitch(i))
                {
                    HasValidationError = true;
                }
            }
            if (HasValidationError)
            {
                MessageBox.Show("Please fill in the required fields or fix the fields with errors.");
                return;
            }
            ErrorMessage = string.Empty;
            try
            {
                await _authenticator.Login(Username, Password);
                ClearingAllInputField();
                Navigator.NavigateSwitch(_navigator, ViewType.Home);
            }
            catch (UserNotFoundException)
            {
                ErrorMessage = "Invalid Username or Password";
            }
            catch (InvalidPasswordException)
            {
                ErrorMessage = "Invalid Username or Password";
            }

        }
        private void ExecuteCloseWindowCommand(UserControl p)
        {
            Window curwindow = Window.GetWindow(p);
            if (curwindow != null)
            {
                curwindow.Close();
            }
        }
        private void ExecuteMinimizeWindowCommand(UserControl p)
        {
            Window curwindow = Window.GetWindow(p);
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
        
        private void ExecuteRecoverPassCommand(object obj)
        {
            throw new NotImplementedException();
        }
        #endregion

        public bool Validate(string propertyName, object propertyValue)
        {
            Helper.ClearErrors(propertyName);
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);

            if (results.Any())
            {
                Helper.AddError(results.Select(r => r.ErrorMessage).First(), propertyName);
                return true;
            }
            return false;
        }

        private bool ValidationSwitch(int index)
        {
            bool error = false;
            switch (index)
            {
                case 0:
                    error = Validate(nameof(Username), Username);
                    break;
                case 1:
                    error = Validate(nameof(Password), Password);
                    break;
            }
            return error;
        }
    }
}
