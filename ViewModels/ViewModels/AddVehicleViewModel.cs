using Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using System.Windows.Navigation;
using Newtonsoft.Json.Linq;
using ViewModels.State.Navigators;
using API;
using Models.Services;
using ViewModels.Commands;

namespace ViewModels
{
    
    public class AddVehicleViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private readonly IDataFromNHTSAService _dataFromNHTSAService;
        private readonly IVINDecoderService _vinDecoderService;
        private readonly ValidationHelper Helper = new ValidationHelper();
        public ObservableCollection<string> MakeList { get; private set; } 
        public ObservableCollection<string> VehicleTypeList { get; private set; }
        public ObservableCollection<string> BodyTypeList { get; private set; }
        public ObservableCollection<string> TrailerTypeList { get; private set; }

        public bool HasErrors => Helper.HasErrors;
        public IEnumerable GetErrors(string propertyName)
        {
            return Helper.GetErrors(propertyName);
        }

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

        #region Fields
        private string _name;
        private string _vin;
        private string _licensePlate;
        private string _vehicleType;
        private string _year;
        private string _make;
        private string _model;
        private string _trim;
        private string _regisState;

        private string _status;
        private string _ownership;

        private string _curbWeight;
        private string _gvwr;
        private string _gcwr;

        private string _fuelEfficiency;
        private string _totalSeats;
        private string _bodyType;
        private string _trailerType;
        #endregion

        #region Properties
        #region Identificaiton Props
        public string Name 
        {
            get => _name;
            set => _name = value; 
        }

        [Required(ErrorMessage = "VIN is required")]
        [StringLength(17, ErrorMessage = "VIN cannot be longer than 17 charaters")]
        public string VIN 
        { 
            get => _vin; 
            set
            {
                _vin = value;
                Helper.ClearErrors(nameof(VIN));
                Validate(nameof(VIN), value);
                OnPropertyChanged(nameof(VIN));
            } 
        }
        public string LicensePlate
        {
            get => _licensePlate;
            set => _licensePlate = value;
        }
        public string VehicleType 
        {
            get => _vehicleType;
            set
            {
                _vehicleType = value;
                Helper.ClearErrors(nameof(VehicleType));
                //if (/*value == "(None)" ||*/ string.IsNullOrWhiteSpace(value))
                //{
                //    Helper.AddError("Vehicle Type is required", nameof(VehicleType));
                //}
                ValidateComboBox("Vehicle Type", value);
                OnPropertyChanged(nameof(VehicleType));
            } 
        }

        [Required(ErrorMessage = "Year is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Please enter valid YEAR NUMBER")]
        public string Year { 
            get => _year;
            set
            {
                _year = value;
                Helper.ClearErrors(nameof(Year));
                Validate(nameof(Year), value);
                //OnPropertyChanged(nameof(Year));
            }
        }
        public string Make
        {
            get => _make;
            set
            {
                _make = value;
                Helper.ClearErrors(nameof(Make));
                ValidateComboBox(nameof(Make), value);
                OnPropertyChanged(nameof(Make));
            }
        }
        public string NewMake
        {
            get => Make;
            set
            {
                if (Make != null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(value) && !MakeList.Contains(value))
                {
                    MakeList.Add(value);
                    Make = value;
                }
            }
        }
        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                Helper.ClearErrors(nameof(Model));
                ValidateComboBox(nameof(Model), value);
                OnPropertyChanged(nameof(Model));
            }
        }
        
        public string Trim
        {
            get => _trim; set => _trim = value;
        }
        public string RegisState 
        {
            get => _regisState; 
            set => _regisState = value;
        }
        #endregion
        public string Status { get => _status; set => _status = value; }
        public string Ownership { get => _ownership; set => _ownership = value; }

        #region Weight Props
        [Required(ErrorMessage = "Curb Weight is required")]
        [RegularExpression("^[0-9]*\\.?[0-9]+$", ErrorMessage = "Please enter valid Number")]
        public string CurbWeight 
        { 
            get => _curbWeight; 
            set
            {
                _curbWeight = value;
                Helper.ClearErrors(nameof(CurbWeight));
                Validate(nameof(CurbWeight), value);
                OnPropertyChanged(nameof(CurbWeight));
            } 
        }

        [Required(ErrorMessage = "Gross Vehicle Weight Rating is required")]
        [RegularExpression("^[0-9]*\\.?[0-9]+$", ErrorMessage = "Please enter valid Number")]
        public string GVWR 
        {
            get => _gvwr; 
            set
            {
                _gvwr = value;
                Helper.ClearErrors(nameof(GVWR));
                Validate(nameof(GVWR), value);
                OnPropertyChanged(nameof(GVWR));
            }
        }
        
        [RegularExpression("^[0-9]*\\.?[0-9]+$", ErrorMessage = "Please enter valid Number")]
        public string GCWR {
            get => _gcwr; 
            set
            {
                _gcwr = value;
                Helper.ClearErrors(nameof(GCWR));
                Validate(nameof(GCWR), value);
                OnPropertyChanged(nameof(GCWR));
            }
        }
        #endregion

        #region Additional Detail Props
        [Required(ErrorMessage = "Fuel Efficiency is required")]
        [RegularExpression("^[0-9]*\\.?[0-9]+$", ErrorMessage = "Please enter valid FLOAT NUMBER")]
        public string FuelEfficiency 
        {
            get => _fuelEfficiency; 
            set
            {
                _fuelEfficiency = value;
                Helper.ClearErrors(nameof(FuelEfficiency));
                Validate(nameof(FuelEfficiency), value);
                OnPropertyChanged(nameof(FuelEfficiency));
            }
        }

        [Required(ErrorMessage = "Total Seats is required")]
        [RegularExpression("([2-9][0-9]*)", ErrorMessage = "Please enter valid INTERGER Number starting at 2")]
        public string TotalSeats
        {
            get => _totalSeats;
            set
            {
                _totalSeats = value;
                Helper.ClearErrors(nameof(TotalSeats));
                Validate(nameof(TotalSeats), value);
                OnPropertyChanged(nameof(TotalSeats));
            }
        }

        public string BodyType
        {
            get => _bodyType;
            set
            {
                _bodyType = value;
                OnPropertyChanged(nameof(BodyType));
            }
        }
        public string NewBodyType
        {
            get => _bodyType;
            set
            {
                if (BodyType != null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(value) && !BodyTypeList.Contains(value))
                {
                    BodyTypeList.Add(value);
                    BodyType = value;
                }
            }
        }

        public string TrailerType
        {
            get => _trailerType;
            set
            {
                _trailerType = value;
                OnPropertyChanged(nameof(TrailerType));
            }
        }
        #endregion
        #endregion

        public AddVehicleViewModel(INavigator navigator, IDataFromNHTSAService dataFromNHTSA, IVINDecoderService vinDecoderService, I)
        {
            Navigation = navigator;
            _dataFromNHTSAService = dataFromNHTSA;
            _vinDecoderService = vinDecoderService;
            //VehicleTypeList = new List<string>() {"(None)", "Passenger Car", "Bus", "Truck", "Incomplete Vehicle", "Motorcycle"};
            //VehicleType = VehicleTypeList.FirstOrDefault();

            //MakeList = new ObservableCollection<string>();

            BackToVehicleListView = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                Navigation.NavigateTo<VehicleListViewModel>();
            });

            Helper.ErrorsChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(Helper));
                ErrorsChanged?.Invoke(this, e);
            };

            DecodeVINCommand = new AsyncRelayCommand(ExcuteDecodeVIN, (ex) => { });

            AddVehicleCommand = new RelayCommand<object>((p) => { return true; }, ExcuteAddVehicle);

            LoadCommand = new AsyncRelayCommand(ExcuteloadViewCommand, (ex) => { });
        }

        #region Commands
        public ICommand AddVehicleCommand { get; }
        public ICommand DecodeVINCommand { get; }
        public ICommand SaveVehicleCommand { get; set; }
        public ICommand BackToVehicleListView { get; }
        public ICommand LoadCommand { get; }

        private void ExcuteAddVehicle(object obj)
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

        }
        private async Task ExcuteDecodeVIN()
        {
            if (Validate(nameof(VIN), VIN))
            {
                OnPropertyChanged(nameof(VIN));
                return;
            }
            try
            {
                VehicleData data = await _vinDecoderService.GetVehicleDataByVIN(VIN);
                VehicleType = VehicleTypeList.FirstOrDefault(s => s.Equals(data.Results.ElementAt(0).VehicleType, StringComparison.OrdinalIgnoreCase));
                Year = data.Results.ElementAt(0).ModelYear;
                Make = MakeList.FirstOrDefault(s => s.Equals(data.Results.ElementAt(0).Make, StringComparison.OrdinalIgnoreCase));
                Model = data.Results.ElementAt(0).Model;
                Trim = data.Results.ElementAt(0).Trim;
                TotalSeats = data.Results.ElementAt(0).Seats;
                BodyType = data.Results.ElementAt(0).BodyClass;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private async Task ExcuteloadViewCommand()
        {
            try
            {
                VehicleType = "(None)";
                Make = "(None)";
                BodyType = "(None)";
                TrailerType = "(None)";
                

                VehicleTypeList = await _dataFromNHTSAService.GetDataListByNameFromNHTSA("Vehicle Type");
                VehicleTypeList.Insert(0, "(None)");
                OnPropertyChanged(nameof(VehicleTypeList));

                MakeList = await _dataFromNHTSAService.GetDataListByNameFromNHTSA("Make");
                MakeList.Insert(0, "(None)");
                OnPropertyChanged(nameof(MakeList));

                BodyTypeList = await _dataFromNHTSAService.GetDataListByNameFromNHTSA("Body Class");
                BodyTypeList.Insert(0, "(None)");
                OnPropertyChanged(nameof(BodyTypeList));

                TrailerTypeList = await _dataFromNHTSAService.GetDataListByNameFromNHTSA("Trailer Body Type");
                TrailerTypeList.Insert(0, "(None)");
                OnPropertyChanged(nameof(TrailerTypeList));
            }
            catch (Exception ex)
            {

            }
            
        }
        
        
        #endregion


        #region Validation
        private bool ValidationSwitch(int index)
        {
            bool error = false;
            switch (index)
            {
                case 0:
                    error = Validate(nameof(VIN), VIN);
                    break;
                case 1:
                    error = ValidateComboBox("Vehicle Type", VehicleType);
                    break;
                case 2:
                    error = Validate(nameof(Year), Year);
                    break;
                case 3:
                    error = ValidateComboBox(nameof(Make), Make);
                    break;
                case 4:
                    error = ValidateComboBox(nameof(Model), Model);
                    break;
                case 5:
                    error = Validate(nameof(CurbWeight), CurbWeight);
                    break;
                case 6:
                    error = Validate(nameof(GVWR), GVWR);
                    break;
                case 7:
                    error = Validate(nameof(GCWR), GCWR);
                    break;
                case 8:
                    error = Validate(nameof(FuelEfficiency), FuelEfficiency);
                    break;
                case 9:
                    error = Validate(nameof(TotalSeats), TotalSeats);
                    break;
            }
            return error;
        }
        private string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
        private bool ValidateComboBox(string propertyNameMess, string propertyValue)
        {
            string propertyName = RemoveWhitespace(propertyNameMess);
            Helper.ClearErrors(propertyName);
            if (propertyValue == "(None)" || string.IsNullOrWhiteSpace(propertyValue))
            {
                Helper.AddError($"{propertyNameMess} is required", propertyName);
                OnPropertyChanged(propertyName);
                return true;
            }
            OnPropertyChanged(propertyName);
            return false;
            
        }
        private bool Validate(string propertyName, object propertyValue)
        {
            Helper.ClearErrors(propertyName);
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);

            if (results.Any())
            {
                Helper.AddError(results.Select(r => r.ErrorMessage).First(), propertyName);
                OnPropertyChanged(propertyName);
                return true;
            }
            OnPropertyChanged(propertyName);
            return false;
        }
        #endregion
    }
}
