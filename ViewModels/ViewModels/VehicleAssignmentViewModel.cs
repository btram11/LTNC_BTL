using API;
using API.Services;
using Google.Cloud.Firestore;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using Models;
using Models.ModelFirebase;
using Models.Services;
using Models.Services.Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels
{
    public class VehicleAssignmentViewModel: ViewModelBase
    {
        private readonly IStoringDataManagementService _storingDataManagementService;
        private readonly IDistanceService _distanceService;
        private readonly ValidationHelper Helper = new ValidationHelper();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => Helper.HasErrors;
        public IEnumerable GetErrors(string propertyName)
        {
            return Helper.GetErrors(propertyName);
        }

        #region Fields Input
        private string _from;
        private string _to;
        private DateTime _departureDateTime;
        private string _transportType;
        private string _weight;
        private string _passengers;
        private bool _returned = false;
        private bool _trailers = false;
        #endregion

        #region Properties
        public string From 
        { 
            get => _from; 
            set
            {
                _from = value;
                OnPropertyChanged(nameof(From));   
            } 
        }

        public string To
        {
            get => _to;
            set
            {
                _to = value;
                OnPropertyChanged(nameof(To));
            }
        }
        public DateTime DepartureDate
        {
            get => (_departureDateTime != null) ?_departureDateTime.Date : _departureDateTime;
            set
            {
                if (value != null && _departureDateTime != null)
                {
                    TimeSpan oldTime = _departureDateTime.TimeOfDay;
                    _departureDateTime = new DateTime(value.Year, value.Month, value.Day).Add(oldTime);
                }
                //if (_departureDateTime != null)
                //{
                //    string temp = _departureDateTime.Value.ToString("h:mm tt");
                //}
                    
                OnPropertyChanged(nameof(DepartureDate));
            }
        }
        public string DepartureTime
        {
            get => (_departureDateTime != null ) ? _departureDateTime.ToString("h:mm tt") : string.Empty;
            set
            {
                //_departureTime = value
                if (value != null && value != string.Empty)
                {
                    string format = "h:mm tt";
                    DateTime dateTime = DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
                    if (_departureDateTime != null)
                    {
                        _departureDateTime = new DateTime(_departureDateTime.Year, _departureDateTime.Month, _departureDateTime.Day).Add(dateTime.TimeOfDay);
                    }
                    else
                    {
                        _departureDateTime = DateTime.Today.Add(dateTime.TimeOfDay); 
                    }
                }

                
                OnPropertyChanged(nameof(DepartureTime));
            }
        }
        public object TransportationType 
        {
            get => _transportType; 
            set
            {
                ComboBoxItem comboBoxItem = value as ComboBoxItem;
                if (comboBoxItem != null)
                {
                    // Nếu là ComboBoxItem, trích xuất giá trị Content và gán vào _transportType
                    _transportType = comboBoxItem.Content?.ToString();
                }
                else
                {
                    // Nếu không phải là ComboBoxItem, giữ nguyên giá trị value và gán vào _transportType
                    _transportType = value.ToString();
                }
                OnPropertyChanged(nameof(TransportationType));
            }
        }

        [Required(ErrorMessage = "Total Weight is required")]
        public string Passenger 
        {
            get => _passengers;
            set
            {
                _passengers = value;
                if (TransportationType.ToString() == nameof(Passenger))
                {
                    Helper.ClearErrors(nameof(Passenger));
                    Validate(nameof(Passenger), Passenger);
                }
                OnPropertyChanged(nameof(Passenger));
            }
        }

        [Required(ErrorMessage = "Total Weight is required")]
        public string Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                if (TransportationType.ToString() == "Cargo")
                {
                    Helper.ClearErrors(nameof(Weight));
                    Validate(nameof(Weight), Weight);
                }
                OnPropertyChanged(nameof(Weight));
            }
        }
        public bool NeedTrailer 
        {
            get => _trailers;
            set
            {
                _trailers = value;
                OnPropertyChanged(nameof(NeedTrailer));
            }
        }
        public bool Returned 
        { 
            get => _returned;
            set
            {
                _returned = value;
                OnPropertyChanged(nameof(Returned));
            } 
        }
        #endregion

        public VehicleAssignmentViewModel(IStoringDataManagementService storingDataManagementService, IDistanceService distanceService)
        {
            _storingDataManagementService = storingDataManagementService;
            _distanceService = distanceService;

            AddTripCommand = new AsyncRelayCommand(ExecuteAddTripCommand);
        }

        private async Task ExecuteAddTripCommand()
        {
            Element element = await _distanceService.GetDistance(From, To);
            double distance = element.Distance.Value;
            distance /= 1000;

            DateTime ArrivingTime = GetScheduledArrivalTime(element);
            List<TrailerFirebase> trailers = null;
            TrailerFirebase selectedTrailer = null;
            if (NeedTrailer)
            {
                IReadOnlyCollection<TrailerFirebase> temp = await _storingDataManagementService.WhereEqualToTrailer("PayloadCapacity", Weight);
                trailers = temp.ToList();
                trailers.RemoveAll(trailer => trailer.OngoingTrip > 0 && trailer.OngoingTripList.Any(trip =>
                    ((DateTime)trip["startDay"] <= _departureDateTime && (DateTime)trip["endDay"] >= _departureDateTime) ||
                    ((DateTime)trip["startDay"] <= ArrivingTime && (DateTime)trip["endDay"] >= ArrivingTime))
                );
                if (trailers.Count == 0)
                {
                    MessageBox.Show("Currently there are no available trailers at the time you choosing");
                    return;
                }
                trailers.Sort((a, b) => a.PayloadCapacity.CompareTo(b.PayloadCapacity));
                selectedTrailer = trailers.First();
            }
            IReadOnlyCollection<VehicleFirebase> vehicleTemp = await _storingDataManagementService.WhereNotEqualToVehicle("VehicleType", "Trailer");
            if (vehicleTemp.Count == 0)
            {
                MessageBox.Show("It seems like you haven't added any driving vehicles");
                return;
            }
            List<VehicleFirebase> vehicles = vehicleTemp.ToList();
            VehicleFirebase selectedDrivingVehicle = null;
            if (TransportationType.ToString() == "Passenger")
            {
                selectedDrivingVehicle = await PassengerVehicleAssign(vehicles, ArrivingTime);
            }
            else
            {
                selectedDrivingVehicle = await CargoVehicleAssign(vehicles, selectedTrailer, ArrivingTime);
            }
            if (selectedDrivingVehicle == null) return;
            DriverFirebase driver = await GetSuitableDriver(selectedDrivingVehicle, ArrivingTime);
            if (driver == null) return;

            Dictionary<string, object> driverFirestore = new Dictionary<string, object>
            {
                {nameof(driver.Id), driver.Id},
                {nameof(driver.LastName), driver.LastName},
                {nameof(driver.FirstName), driver.FirstName},
                {nameof(driver.Phone), driver.Phone},
                {nameof(driver.Address), driver.Address},
                {nameof (driver.City), driver.City},
                {nameof(driver.Country), driver.Country},
                {nameof(driver.DrivingLicenseClass), driver.DrivingLicenseClass },
                {nameof(driver.DrivingLicenseNumber), driver.DrivingLicenseNumber}
            };
            Dictionary<string, object> vehicleFirestore = new Dictionary<string, object>
            {
                {nameof(selectedDrivingVehicle.Id), selectedDrivingVehicle.Id},
                {nameof(selectedDrivingVehicle.LicensePlate), selectedDrivingVehicle.LicensePlate},
                {nameof(selectedDrivingVehicle.VehicleType), selectedDrivingVehicle.VehicleType},
                {nameof(selectedDrivingVehicle.Make), selectedDrivingVehicle.Make},
                {nameof(selectedDrivingVehicle.Models), selectedDrivingVehicle.Models},
                {nameof(selectedDrivingVehicle.FuelTypePrimary), selectedDrivingVehicle.FuelTypePrimary },
                {nameof(selectedDrivingVehicle.FuelEfficiency), selectedDrivingVehicle.FuelEfficiency},
                {nameof(selectedDrivingVehicle.FuelCapacity), selectedDrivingVehicle.FuelCapacity},
                {nameof(selectedDrivingVehicle.GCWR), selectedDrivingVehicle.GCWR},
                {nameof(selectedDrivingVehicle.GVWR), selectedDrivingVehicle.GVWR},
                {"PayloadCapacity", selectedDrivingVehicle.GVWR - selectedDrivingVehicle.CurbWeight},
                {nameof(selectedDrivingVehicle.CurbWeight), selectedDrivingVehicle.CurbWeight},
            };
            TripFirebase newTrip = new TripFirebase
            {
                Status = "Scheduled",
                Origin = From,
                Destination = To,
                Duration = (_departureDateTime - ArrivingTime).TotalMinutes,
                ScheduledDepartureTime = Timestamp.FromDateTime(_departureDateTime),
                ScheduledArrivalTime = Timestamp.FromDateTime(ArrivingTime),
                HasTrailer = NeedTrailer,
                Returned = Returned,
                Vehicle = driverFirestore,
                Driver = vehicleFirestore,
                Trailer = !NeedTrailer ? null : new Dictionary<string, object>
                {
                    {nameof(selectedTrailer.Id), selectedTrailer.Id},
                    {nameof(selectedTrailer.TrailerType), selectedTrailer.TrailerType},
                    {nameof(selectedTrailer.LicensePlate), selectedTrailer.LicensePlate},
                    {nameof(selectedTrailer.GVWR), selectedTrailer.GVWR},
                    {nameof(selectedTrailer.Make), selectedTrailer.Make},
                    {nameof(selectedTrailer.PayloadCapacity), selectedTrailer.PayloadCapacity},
                }
            };

            await _storingDataManagementService.AddOrUpdateTrip(newTrip);
        }
        private async Task<DriverFirebase> GetSuitableDriver(VehicleFirebase vehicle, DateTime ArrivingTime)
        {
            IReadOnlyCollection<DriverFirebase> tempList = await _storingDataManagementService.GetAllDrivers();
            if (tempList.Count == 0)
            {
                MessageBox.Show("Seems like you havent added any drivers. Please add a driver first");
                return null;
            }
            List<DriverFirebase> drivers = tempList.ToList();
            drivers.RemoveAll(driver => driver.OngoingTrip > 0 && driver.OngoingTripList.Any(trip =>
                    ((DateTime)trip["startDay"] <= _departureDateTime && (DateTime)trip["endDay"] >= _departureDateTime) ||
                    ((DateTime)trip["startDay"] <= ArrivingTime && (DateTime)trip["endDay"] >= ArrivingTime))
            );
            if (drivers.Count == 0)
            {
                MessageBox.Show("Seems like you havent added any drivers. Please add a driver first");
                return null;
            }
            List<string> ClassCanDrive = null;
            switch (vehicle)
            {
                // NOTE: CÓ VẺ SẼ THÊM DOC CHỈ 
                case var _ when TransportationType.ToString() == "Cargo" && vehicle.GVWR > 3500 && NeedTrailer == false:
                    // Class C (smallest) && FC
                    ClassCanDrive = new List<string> {"C", "FC", "D", "E"};
                    drivers = drivers.Where(driver =>
                    {
                        string[] driverClassess = driver.DrivingLicenseClass.Split(',').Select(w => w.Trim()).ToArray();
                        return driverClassess.All(word => ClassCanDrive.Contains(word));
                    }).ToList();
                    //return drivers.OrderBy(driver => driver.)
                    break;
                case var _ when TransportationType.ToString() == "Cargo" && vehicle.GVWR > 3500 && NeedTrailer == true:
                    // Class FC (only)
                    ClassCanDrive = new List<string> {"FC", "D", "E" }; 
                    // Use for priority that License contains only FC will get the highest priority
                    drivers = drivers.Where(driver => driver.DrivingLicenseClass.Contains("FC")).ToList();
                    break;
                case var _ when vehicle.GVWR < 3500 && vehicle.TotalSeats <= 9:
                    // Class B2 (smallest)
                    ClassCanDrive = new List<string> {"B2", "C", "FC", "D", "E" };
                    break;
                case var _ when vehicle.TotalSeats > 9 && vehicle.TotalSeats <= 30:
                    // Class D (smallest)
                    ClassCanDrive = new List<string> { "D", "E" };
                    drivers = drivers.Where(driver =>
                    {
                        string[] driverClassess = driver.DrivingLicenseClass.Split(',')
                                                        .Select(w => w.Trim()).ToArray();
                        return driverClassess.All(word => ClassCanDrive.Contains(word));
                    }).ToList();
                    ClassCanDrive.Add("FC");
                    //To prioritize the list, add FC at the end so that the order after sorting becomes D -> (D, FC) -> E -> (E, FC).
                    break;
                case var _ when vehicle.TotalSeats > 30:
                    // Class E (only as it was the highest)
                    ClassCanDrive = new List<string> {"E" ,"FC"};
                    drivers = drivers.Where(driver => driver.DrivingLicenseClass.Contains("E")).ToList();
                    ClassCanDrive.Add("FC");
                    break;
                default:
                    MessageBox.Show("This vehicle does not currently support automatic driver assignment.");
                    return null;
            }
            if (ClassCanDrive == null) return null;
            drivers = drivers.OrderBy(driver => GetPriorityIndexListPreferMin(driver.DrivingLicenseClass, ClassCanDrive))
                        .ThenBy(driver => GetPriorityIndexListPreferMax(driver.DrivingLicenseClass, ClassCanDrive))
                        .ThenByDescending(driver => driver.CompletedTrip)
                        .ToList();
            return drivers.FirstOrDefault();
        }

        public int GetPriorityIndexListPreferMin (string className, List<string> priorityList)
        {
            string[] driverClassess = className.Split(',').Select(w => w.Trim()).ToArray();
            int minIndex = priorityList.Count;
            foreach (var classItem in driverClassess)
            {
                int index = priorityList.IndexOf(classItem); 
                if (index != -1 && index < minIndex) 
                {
                    minIndex = index;
                }
            }
            return minIndex;
        }
        public int GetPriorityIndexListPreferMax(string className, List<string> priorityList)
        {
            string[] driverClassess = className.Split(',').Select(w => w.Trim()).ToArray();
            int maxIndex = priorityList.Count;
            foreach (var classItem in driverClassess)
            {
                int index = priorityList.IndexOf(classItem);
                if (index != -1 && index < maxIndex)
                {
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        private async Task<VehicleFirebase> CargoVehicleAssign(List<VehicleFirebase> vehicles, TrailerFirebase selectedTrailer, DateTime ArrivingTime)
        {
            vehicles.RemoveAll(vehicle => vehicle.VehicleType != "Truck" || vehicle.VehicleType.Contains("Passenger") || vehicle.VehicleType.Contains("Incomplete"));
            vehicles.RemoveAll(vehicle => vehicle.OngoingTrip > 0 && vehicle.OngoingTripList
            .Any(trip =>
                    ((DateTime)trip["startDay"] <= _departureDateTime && (DateTime)trip["endDay"] >= _departureDateTime) ||
                    ((DateTime)trip["startDay"] <= ArrivingTime && (DateTime)trip["endDay"] >= ArrivingTime))
            ); //Vì ArrivingTime đã cộng spare time nên chỉ cần thời gian khởi hành & kết thúc dự kiến ko nằm trong các trip đã đc lên kế hoạch là đc
            if (selectedTrailer != null)
            {
                IOrderedEnumerable<VehicleFirebase> list = vehicles
                    .Where(vehicle => vehicle.BodyType == "Truck-Tractor" || vehicle.VehicleType.Contains("Incomplete"))
                    .Where(vehicle => { 
                        Random random = new Random();
                        return vehicle.GCWR - vehicle.GVWR >= selectedTrailer.GVWR &&
                        vehicle.GVWR >= selectedTrailer.GVWR * 20 / 100 + vehicle.FuelCapacity * 0.84 + random.Next(150, 500);
                    })
                    .OrderBy(vehicle => vehicle.GCWR - vehicle.GVWR);
                // selectedTrailer.GVWR * 20 / 100 count as weight of Fifth Wheels 
                if (list.Count() == 0)
                {
                    MessageBox.Show("It seems like there are no vehicles from your data that can load all your goods at once");
                    return null;
                }
                return list.FirstOrDefault();
            }
            else
            {
                IOrderedEnumerable<VehicleFirebase> list = vehicles
                    .Where(vehicle => vehicle.BodyType != "Truck-Tractor")
                    .Where(vehicle => vehicle.GVWR >= vehicle.CurbWeight + int.Parse(Weight) + vehicle.FuelCapacity * 0.84 + 500)
                    .OrderBy(vehicle => vehicle.GVWR);
                // Adding spare 500kg also calculate every vehicle that fulfil the total weight of empty vehicle weight, cargo weight, weight of fuels when fuel tanks is full and that spare 500 kg
                // Truck-Tractor need trailer to transport goods, container, etc
                if (list.Count() == 0)
                {
                    MessageBox.Show("It seems like there are no vehicles from your data that can load all your goods at once");
                    return null;
                }

                return list.FirstOrDefault();
            }
            
        }

        private async Task<VehicleFirebase> PassengerVehicleAssign(List<VehicleFirebase> vehicles, DateTime ArrivingTime) 
        {
            vehicles.RemoveAll(vehicle => vehicle.VehicleType != "Bus" || !vehicle.VehicleType.Contains("Passenger"));
            vehicles.RemoveAll(vehicle => vehicle.OngoingTrip > 0 && vehicle.OngoingTripList.Any(trip =>
                    ((DateTime)trip["startDay"] <= _departureDateTime && (DateTime)trip["endDay"] >= _departureDateTime) ||
                    ((DateTime)trip["startDay"] <= ArrivingTime && (DateTime)trip["endDay"] >= ArrivingTime))
            );
            vehicles.RemoveAll(vehicle => vehicle.TotalSeats <= int.Parse(Passenger) + 3);
            if (vehicles.Count() == 0)
            {
                MessageBox.Show("Oh no, there are no vehicles left in system that can carry all passengers");
                return null;
            }
            VehicleFirebase vehicleFirebase = vehicles.OrderBy(vehicle => vehicle.TotalSeats).ThenBy(vehicle => vehicle.GVWR).FirstOrDefault();
            return vehicleFirebase;
            
        }
        private DateTime GetScheduledArrivalTime(Element element)
        {
            int day = 0, hour = 0, minute = 0, second = 0;
            if (Returned) second = element.Duration.Value * 2;
            else second = element.Duration.Value;
            day = (int)Math.Floor(second / (Math.Pow(60, 2) * 24));
            hour = (int)Math.Floor(second / Math.Pow(60, 2));
            minute = second / 60;
            second %= 60;
            TimeSpan duration = new TimeSpan(day, hour + 2, minute, second); // adding spare 2 hours
            return _departureDateTime + duration;
        }

        public ICommand AddTripCommand { get; }

        private bool Validate(string propertyName, object propertyValue)
        {
            Helper.ClearErrors(propertyName);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
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
    }
}
