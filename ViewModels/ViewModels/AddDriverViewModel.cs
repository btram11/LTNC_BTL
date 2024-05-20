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
using ViewModels.ValidationAttrs;
using System.Collections.ObjectModel;

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
        public ObservableCollection<DriverStatus> StatusTypeList { get; private set; } = new ObservableCollection<DriverStatus>(System.Enum.GetValues(typeof(DriverStatus)).Cast<DriverStatus>());
        public bool HasErrors => Helper.HasErrors;
        public IEnumerable GetErrors(string propertyName)
        {
            return Helper.GetErrors(propertyName);
        }


        #region Fields
        private string _firstName;
        private string _lastName;
        private DateTime? _dateOfBirth;
        private Gender _gender;
        private string _id;
        private string _placeOfIssue;
        private string _phone;
        private string _email;
        private string _address;
        private string _city;
        private string _country;

        private string _dLClass;
        private string _dLNumber;
        private string _education;
        private DriverStatus _driverStatus;
        private string _criminalRecord;
        private string _health;
        private string _exYear;
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

        [Required(ErrorMessage = "Date Of Birth is required")]
        [DateOfBirthValidate(nameof(Gender), ErrorMessage = "Your age seems not valid")]
        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                Helper.ClearErrors(nameof(DateOfBirth));
                Validate(nameof(DateOfBirth), value);
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }
        public Gender Gender
        {
            get => _gender; 
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
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
        [RegularExpression(@"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})", ErrorMessage = "Invalid phone number")]
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                Helper.ClearErrors(nameof(Phone));
                Validate(nameof(Phone), Phone);
                //ValidatePhoneNumber(nameof(Phone), Phone);
                OnPropertyChanged(nameof(Phone));
            }
        }

        [Required(ErrorMessage = "Email is required")]
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
                //ValidateEmail(nameof(Email), value);
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
                Helper.ClearErrors(nameof(Address));
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
        [RegularExpression("^[A-Z][A-Z]*\\d*([,\\s]+[A-Z][A-Z]*\\d*)*", ErrorMessage = "The input you just provided does not match the described format")]
        public string DrivingLicenseClass
        {
            get => _dLClass;
            set
            {
                _dLClass = value;
                Helper.ClearErrors(nameof(DrivingLicenseClass));
                Validate(nameof(DrivingLicenseClass), value);
                ValidateLicenseClass(nameof(DrivingLicenseClass), DrivingLicenseClass);
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

        public string Education
        {
            get => _education;
            set
            {
                _education = value;
                OnPropertyChanged(nameof(Education));
            }
        }
        public DriverStatus Status
        {
            get => _driverStatus;
            set
            {
                _driverStatus = value;
                OnPropertyChanged(nameof(DriverStatus));
            }
        }
        public string CriminalRecord
        {
            get => _criminalRecord;
            set
            {
                _criminalRecord = value;
                OnPropertyChanged(nameof(CriminalRecord));
            }
        }

        public string Health
        {
            get => _health;
            set
            {
                _health = value;
                OnPropertyChanged(nameof(Health));
            }
        }

        [RegularExpression("^[0-9]*\\.?[0-9]+$", ErrorMessage = "Please enter a valid positive Number")]
        [MaxLength(2, ErrorMessage = "It is not possible for having more than 100 years of driving experience")]
        public string ExYear
        {
            get => _exYear;
            set
            {
                _exYear = value;
                Helper.ClearErrors(nameof(ExYear));
                Validate(nameof(ExYear), value);
                OnPropertyChanged(nameof(ExYear));
            }
        }
        #endregion

        public AddDriverViewModel(INavigator navigator, IStoringDataManagementService storingDataManagementService)
        {
            Navigation = navigator;
            _storingDataManagementService = storingDataManagementService;

            AddDriverCommand = new AsyncRelayCommand(ExcuteAddDriver);

            ClearingCommand = new RelayCommand((p) => {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to clear all your inputs", "", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Cancel)
                    return;
                ClearingAllInputField();
            });

            LoadCommand = new RelayCommand((p) => ExcuteLoadViewCommand());

            GetGenderCommand = new RelayCommand<object>((p) => {
                Gender enumValue;
                if (Enum.TryParse((string)p, out enumValue))
                {
                    Gender = enumValue;
                }
            }, (p) => { return p != null ? true : false; });

            UpdateViewCommand = new RelayCommand<ViewType>((p) => Navigator.NavigateSwitch(Navigation, p));

            Helper.ErrorsChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(Helper));
                ErrorsChanged?.Invoke(this, e);
            };
        }

        private void ClearingAllInputField()
        {
            FirstName = null;
            LastName = null;
            DateOfBirth = null;
            ID = null;
            PlaceOfIssue = null;
            Phone = null;
            Email = null;
            Address = null;
            City = null;
            Country = null;
            DrivingLicenseClass = null;
            DrivingLicenseNumber = null;
            CriminalRecord = null;
            ExYear = null;
            Health = null;
            Status = DriverStatus.Available;
            Education = null;
            Helper.ClearAllErrors();
        }

        private int CalculateAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - DateOfBirth.Value.Year;

            if (DateOfBirth.Value.Date > today.AddYears(-age))
                age--;

            return age;
        }

        #region Command
        public ICommand AddDriverCommand { get; }
        public ICommand ClearingCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand GetGenderCommand { get; }
        public ICommand UpdateViewCommand { get; }

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
                MessageBox.Show("Please fill in the required fields or fix the fields with errors.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                Mouse.OverrideCursor = Cursors.Wait;
            });
            try
            {
            IReadOnlyCollection<DriverFirebase> temp = await _storingDataManagementService.WhereEqualToDriver(nameof(ID), ID);
            if (temp.Count > 0)
            {
                MessageBox.Show("Please check Identification number as there is a driver with that ID");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Are you sure you want to add this driver", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            DriverFirebase driver = new DriverFirebase
            {
                FirstName = FirstName,
                LastName = LastName,
                Age = CalculateAge(),
                DateOfBirth = DateOfBirth.ToString(),
                Gender = Gender,
                ID = ID,
                PlaceOfIssue = string.IsNullOrEmpty(PlaceOfIssue) ? null : PlaceOfIssue,
                Phone = Phone,
                Email = Email,
                Address = Address,
                City = string.IsNullOrEmpty(City) ? null : City,
                Country = string.IsNullOrEmpty(Country) ? null : Country,
                Status = Status,
                Education = string.IsNullOrEmpty(Education) ? null : Education,
                Health = string.IsNullOrEmpty(Health) ? null : Health, 
                ExYear = string.IsNullOrEmpty(ExYear) ? null : ExYear,
                DrivingLicenseClass = RemoveDuplicateCharacters(DrivingLicenseClass),
                DrivingLicenseNumber = DrivingLicenseNumber,
                CriminalRecord = string.IsNullOrEmpty(CriminalRecord) ? null : CriminalRecord,
            };
            DriverFirebase duplicatedIdVehicle = await _storingDataManagementService.GetDriverById(driver.Id);
            while (duplicatedIdVehicle != null)
            {
                driver.Id = Guid.NewGuid().ToString("N");
                duplicatedIdVehicle = await _storingDataManagementService.GetDriverById(driver.Id);
            }
            
                await _storingDataManagementService.AddOrUpdateDriver(driver);
                MessageBox.Show("Successfully added");
                ClearingAllInputField();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = null;
                });
            }

        }
        private void ExcuteLoadViewCommand()
        {
            ClearingAllInputField();
        }

        private string RemoveDuplicateCharacters(string input)
        {
            string[] parts = input.Split(',').Select(p => p.Trim()).ToArray();
            List<string> uniqueCharacters = new List<string>();

            foreach (string part in parts)
            {
                if (!uniqueCharacters.Contains(part))
                {
                    uniqueCharacters.Add(part);
                }
            }
            return string.Join(", ", uniqueCharacters);
        }

        #endregion



        #region Validate Methods
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

        public bool ValidateLicenseClass(string propertyName, string propertyValue)
        {
            if (string.IsNullOrEmpty(propertyValue)) return true;
            List<string>  ClassCanDrive = new List<string> { "C", "FC", "D", "E", "B2" };
            string[] driverClassess = propertyValue.Split(',').Select(p => p.Trim()).ToArray();

            bool IsInValid = driverClassess.Any(c => !ClassCanDrive.Contains(c));

            if (IsInValid)
            {
                Helper.AddError("Please delete unsupported License Classess", propertyName);
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
                    break;
                case 4:
                    error = Validate(nameof(Email), Email);
                    break;
                case 5:
                    error = Validate(nameof(Address), Address);
                    break;
                case 6:
                    error = Validate(nameof(DrivingLicenseClass), DrivingLicenseClass) || 
                        ValidateLicenseClass(nameof(DrivingLicenseClass), DrivingLicenseClass);
                    break;
                case 7:
                    error = Validate(nameof(DrivingLicenseNumber), DrivingLicenseNumber);
                    break;
                case 8:
                    error = Validate(nameof(DateOfBirth), DateOfBirth);
                    break;
                case 9:
                    error = Validate(nameof(ExYear), ExYear);
                    break;
            }
            return error;
        }
        #endregion

        ~AddDriverViewModel()
        {

        }
    }
}
