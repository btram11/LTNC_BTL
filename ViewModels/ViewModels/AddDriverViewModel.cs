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
using Models.Services.Firebase;
using Models.ModelFirebase;
using GoogleApi.Entities.Translate.Common.Enums;
using System.Reflection;
using MaterialDesignThemes.Wpf.Internal;
using Models;
using System.Xml.Linq;

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

        private readonly IStoringDataManagementService _storingDataManagementService;
        private readonly ValidationHelper Helper = new ValidationHelper();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => Helper.HasErrors;
        public IEnumerable GetErrors(string propertyName)
        {
            return Helper.GetErrors(propertyName);
        }


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
        public string ID
        {
            get => _id ; 
            set
            {
                _id = value;
                Helper.ClearErrors(nameof(ID));
                Validate(nameof(ID), value);
                OnPropertyChanged(nameof(ID));
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
                Validate(nameof(Phone), Phone);
                ValidatePhoneNumber(nameof(Phone), Phone);
                OnPropertyChanged(nameof(Phone));
            }
        }

        [Required(ErrorMessage = "Email is required")]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                Helper.ClearErrors(nameof(Email));
                Validate(nameof(Email), value);
                ValidateEmail(nameof(Email), value);
                OnPropertyChanged(nameof(Email));
            }
        }

        [Required(ErrorMessage = "Address is required")]
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                Validate(nameof(Address), value);
                OnPropertyChanged(nameof(Address));
            }
        }
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
        #endregion

        [Required(ErrorMessage = "Driving License Class is required")]
        public string DrivingLicenseClass
        {
            get => _dLClass;
            set
            {
                _dLClass = value;
                Helper.ClearErrors(nameof(DrivingLicenseClass));
                Validate(nameof(DrivingLicenseClass), value);
                OnPropertyChanged(nameof(DrivingLicenseClass));
            }
        }

        [Required(ErrorMessage = "Driving License Number is required")]
        public string DrivingLicenseNumber
        {
            get => _dLNumber;
            set
            {
                _dLNumber = value;
                Helper.ClearErrors(nameof(DrivingLicenseNumber));
                Validate(nameof(DrivingLicenseNumber), value);
                OnPropertyChanged(nameof(DrivingLicenseNumber));
            }
        }

        public string CriminalRecord { get; set; }
        #endregion

        public AddDriverViewModel(INavigator navigator, IStoringDataManagementService storingDataManagementService)
        {
            Navigation = navigator;
            _storingDataManagementService = storingDataManagementService;

            AddDriverCommand = new AsyncRelayCommand(ExcuteAddDriver);

            BackToDriverListView = new RelayCommand<UserControl>((p) => { return true; }, (p) => Navigation.NavigateTo<VehicleListViewModel>());

            GetGenderCommand = new RelayCommand<object>((p) => { return p != null ? true : false; }, (p) => 
            {
                Gender = (string)p;
            });

            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) => ExcuteLoadViewCommand());

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
            DateOfBirth = null;
            ID = string.Empty;
            PlaceOfIssue = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            Country = string.Empty;
            DrivingLicenseClass = string.Empty;
            DrivingLicenseNumber = string.Empty;
            CriminalRecord = string.Empty;
            Helper.ClearAllErrors();
        }
        


        #region Command
        public ICommand AddDriverCommand { get; }
        public ICommand BackToDriverListView { get; }
        public ICommand LoadCommand { get; set; }
        public ICommand GetGenderCommand { get; }

        private async Task ExcuteAddDriver()
        {
            bool HasValidationError = false;
            for (int i = 0; i < 10; i++)
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
            IReadOnlyCollection<DriverFirebase> temp = await _storingDataManagementService.WhereEqualToDriver(nameof(ID), ID);
            if (temp.Count > 0)
            {
                MessageBox.Show("Please check Identification number as there is a driver with that ID");
                return;
            }
            DriverFirebase driver = new DriverFirebase
            {
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth.ToString(),
                Gender = Gender,
                ID = ID,
                PlaceOfIssue = PlaceOfIssue,
                Phone = Phone,
                Email = Email,
                Address = Address,
                City = City,
                Country = Country,
                DrivingLicenseClass = DrivingLicenseClass,
                DrivingLicenseNumber = DrivingLicenseNumber,
                CriminalRecord = CriminalRecord,
            };
            DriverFirebase duplicatedIdVehicle = await _storingDataManagementService.GetDriverById(driver.Id);
            while (duplicatedIdVehicle != null)
            {
                driver.Id = Guid.NewGuid().ToString("N");
                duplicatedIdVehicle = await _storingDataManagementService.GetDriverById(driver.Id);
            }
            try
            {
                await _storingDataManagementService.AddOrUpdateDriver(driver);
                MessageBox.Show("Successfully added");
                ClearingAllInputField();
            }
            catch (Exception ex)
            {

            }
            
        }
        private void ExcuteLoadViewCommand()
        {
            ClearingAllInputField();
        }

        #endregion

        

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

        public bool ValidateEmail(string propertyName, string emailAddress)
        {
            var pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                          @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                          @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            var regex = new Regex(pattern);

            if (!regex.IsMatch(emailAddress))
            {
                Helper.AddError("Invalid email address", propertyName);
                return true;
            }
            return false;
        }
        public bool ValidatePhoneNumber(string propertyName, string pNumber)
        {
            
            var pattern = @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})";

            var regex = new Regex(pattern);

            if (!regex.IsMatch(pNumber))
            {
                Helper.AddError("Invalid phone number", propertyName);
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
                    error = Validate(nameof(ID), ID);
                    break;
                case 3:
                    error = Validate(nameof(Phone), Phone);
                    error = ValidatePhoneNumber(nameof(Phone), Phone);
                    break;
                case 4:
                    error = Validate(nameof(Email), Email);
                    error = ValidateEmail(nameof(Email), Email);
                    break;
                case 5:
                    error = Validate(nameof(Address), Address);
                    break;
                case 6:
                    error = Validate(nameof(DrivingLicenseClass), DrivingLicenseClass);
                    break;
                case 7:
                    error = Validate(nameof(DrivingLicenseNumber), DrivingLicenseNumber);
                    break;
                    //case 8:
                    //    error = Validate(nameof(FuelEfficiency), FuelEfficiency);
                    //    break;
                    //case 9:
                    //    error = Validate(nameof(TotalSeats), TotalSeats);
                    //    break;
            }
            return error;
        }
        #endregion

        ~AddDriverViewModel()
        {

        }
    }
}
