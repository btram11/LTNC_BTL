using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using ViewModels.State.Navigators;
using ViewModels.Commands;
using Models.Services;

namespace ViewModels
{
    public class AddDriverViewModel : ViewModelBase, INotifyDataErrorInfo
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
        private readonly ValidationHelper Helper = new ValidationHelper();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #region Fields
        private string _firstName;
        private string _lastName;
        private DateTime? _dateOfBirth;
        private string _gender;
        private string _id;
        private string _placeOfIssue;
        private string _phone;
        private string _email;
        private string _address;
        private string _city;
        private string _country;

        private string _dLClass;
        private string _dLNumber;
        #endregion

        #region Properties
        #region Identification
        [Required(ErrorMessage = "First Name is required")]
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
        
        [Required(ErrorMessage = "Last Name is required")]
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
        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set => _dateOfBirth = value;
        }
        public string Gender
        {
            get => _gender; set => _gender = value;
        }
        
        [Required(ErrorMessage = "Identification Number is required")]
        public string Id
        {
            get => _id ; 
            set
            {
                _id = value;
                Helper.ClearErrors(nameof(Id));
                Validate(nameof(Id), value);
                OnPropertyChanged(nameof(Id));
            }
        }
        public string PlaceOfIssue
        {
            get => _placeOfIssue ; set => _placeOfIssue = value;
        }

        [Required(ErrorMessage = "Phone Number is required")]
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                Helper.ClearErrors(nameof(Phone));
                ValidatePhoneNumber(nameof(Phone), value);
                OnPropertyChanged(nameof(Phone));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                Helper.ClearErrors(nameof(Email));
                ValidateEmail(nameof(Email), value);
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Address
        {
            get => _address; set => _address = value;
        }
        public string City
        {
            get => _city; set => _city = value;
        }
        public string Country
        {
            get => _country; set => _country = value;
        }
        #endregion

        [Required(ErrorMessage = "Driver License Class is required")]
        public string DLClass
        {
            get => _dLClass;
            set
            {
                _dLClass = value;
                Helper.ClearErrors(nameof(DLClass));
                Validate(nameof(DLClass), value);
                OnPropertyChanged(nameof(DLClass));
            }
        }

        [Required(ErrorMessage = "Driver License Number is required")]
        public string DLNumber
        {
            get => _dLNumber;
            set
            {
                _dLNumber = value;
                Helper.ClearErrors(nameof(DLNumber));
                Validate(nameof(DLNumber), value);
                OnPropertyChanged(nameof(DLNumber));
            }
        }
        #endregion

        public AddDriverViewModel(INavigator navigator, IDataFromNHTSAService dataFromNHTSA)
        {
            Navigation = navigator;
            BackToDriverListView = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                //Window window = Window.GetWindow(p);
                //var viewModel = (MainViewModel)window.DataContext;
                //viewModel.CurrentViewModel = new DriverListViewModel();
                Navigation.NavigateTo<VehicleListViewModel>();
            });

            GetGenderCommand = new RelayCommand<object>((p) => { return p != null ? true : false; }, (p) => 
            {
                Gender = (string)p;
            });

            Helper.ErrorsChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(Helper));
                ErrorsChanged?.Invoke(this, e);
            };

            
        }

        
        #region Command
        public ICommand AddVehicleCommand { get; }
        public ICommand BackToDriverListView { get; }
        public ICommand GetGenderCommand { get; }

        #endregion

        public bool HasErrors => Helper.HasErrors;

        public IEnumerable GetErrors(string propertyName)
        {
            return Helper.GetErrors(propertyName);
        }


        #region Validate Methods
        public bool Validate(string propertyName, object propertyValue)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);

            if (results.Any())
            {
                Helper.AddError(results.Select(r => r.ErrorMessage).First(), propertyName);
                return true;
            }
            return false;
        }

        public void ValidateEmail(string propertyName, string emailAddress)
        {
            var pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                          @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                          @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            var regex = new Regex(pattern);

            if (!regex.IsMatch(emailAddress))
            {
                Helper.AddError("Invalid email address", propertyName);
            }

        }
        public void ValidatePhoneNumber(string propertyName, string pNumber)
        {
            if (!Validate(propertyName, pNumber))
            {
                var pattern = @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})";

                var regex = new Regex(pattern);

                if (!regex.IsMatch(pNumber))
                {
                    Helper.AddError("Invalid phone number", propertyName);
                }
            }
        }
        #endregion

        ~AddDriverViewModel()
        {

        }
    }
}
