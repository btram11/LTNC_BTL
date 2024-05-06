using GoogleApi.Entities.Search.Video.Common;
using Models.ModelFirebase;
using Models.Services.AuthenticationServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ViewModels.Commands;
using ViewModels.State.Authentication;
using ViewModels.State.Navigators;

namespace ViewModels
{
    public class SignUpViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private readonly ValidationHelper Helper = new ValidationHelper();

        public bool HasErrors => Helper.HasErrors;
        public IEnumerable GetErrors(string propertyName)
        {
            return Helper.GetErrors(propertyName);
        }

        private string _firstName;
        private string _lastName;
        private string _username;
        private string _email;
        private string _password;
        private string _confirmPassword;

        [Required(ErrorMessage ="Please enter your First Name")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                Helper.ClearErrors(nameof(FirstName));
                Validate(nameof(FirstName), value);
                OnPropertyChanged(nameof(FirstName));
            }
        }

        [Required(ErrorMessage = "Please enter your Last Name")]
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                Helper.ClearErrors(nameof(LastName));
                Validate(nameof(LastName), value);
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Username 
        {
            get => _username;
            set
            {
                _username = value;
                Helper.ClearErrors(nameof(Username));
                OnPropertyChanged(nameof(Username));
            }
        }


        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                          @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                          @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email address")]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                Helper.ClearErrors(nameof(Email));
                Validate(nameof(Email), value);
                OnPropertyChanged(nameof(Email));
            }
        }

        [Required(ErrorMessage = "Please enter your password")]
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

        [Required(ErrorMessage = "Please enter your password again")]
        public string ConfirmPassword 
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                Helper.ClearErrors(nameof(ConfirmPassword));
                Validate(nameof(ConfirmPassword), value);
                OnPropertyChanged(nameof(ConfirmPassword));
            } 
        }


        public SignUpViewModel(IAuthenticator authenticator, INavigator navigator)
        {
            _authenticator = authenticator;
            _navigator = navigator;
            SignUpCommand = new AsyncRelayCommand(ExecuteSignUpCommand);
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ClearingAllInputField();
            });

            WindowMinimizeCommand = new RelayCommand<UserControl>((p) => { return p != null ? true : false; }, (p) => ExecuteMinimizeWindowCommand(p));
            WindowCloseCommand = new RelayCommand<UserControl>((p) => { return p != null ? true : false; }, (p) => ExecuteCloseWindowCommand(p));

            UpdateViewModelCommand = new RelayCommand<object>((p) => { return true; }, (p) => Navigator.NavigateSwitch(_navigator, p));


            Helper.ErrorsChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(Helper));
                ErrorsChanged?.Invoke(this, e);
            };
        }
        private void ClearingAllInputField()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            Helper.ClearAllErrors();
        }

        #region Commands
        public ICommand SignUpCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand UpdateViewModelCommand { get; }
        public ICommand WindowMinimizeCommand { get; }
        public ICommand WindowCloseCommand { get; }
        private async Task ExecuteSignUpCommand()
        {
            bool HasValidationError = false;
            for (int i = 0; i < 6; i++)
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

            Account newUser = new Account
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                Username = Username,
            };
            RegistrationResult result = await _authenticator.Register(newUser);
            switch (result)
            {
                case RegistrationResult.Success:
                    MessageBox.Show("Registration successful. You will be redirected to the login page.");
                    ClearingAllInputField();
                    Navigator.NavigateSwitch(_navigator, ViewType.Login);
                    break;
                case RegistrationResult.PasswordsDoNotMatch:
                    MessageBox.Show("Password does not match confirm password.");
                    break;
                case RegistrationResult.EmailAlreadyExists:
                    MessageBox.Show("An account for this email already exists.");
                    break;
                case RegistrationResult.UsernameAlreadyExists:
                    MessageBox.Show("An account for this username already exists.");
                    break;
                default:
                    MessageBox.Show("Registration failed.");
                    break;
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
        private void ExecuteCloseWindowCommand(UserControl p)
        {
            Window curwindow = Window.GetWindow(p);
            if (curwindow != null)
            {
                curwindow.Close();
            }
        }
        
        #endregion

        public bool Validate(string propertyName, object propertyValue)
        {
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
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
                    error = Validate(nameof(FirstName), FirstName);
                    break;
                case 1:
                    error = Validate(nameof(LastName), LastName);
                    break;
                case 2:
                    error = Validate(nameof(Username), Username);
                    break;
                case 3:
                    error = Validate(nameof(Email), Email);
                    break;
                case 4:
                    error = Validate(nameof(Password), Password);
                    break;
                case 5:
                    error = Validate(nameof(ConfirmPassword), ConfirmPassword);
                    break;
            } 
            return error;
        }
        
    }
}
