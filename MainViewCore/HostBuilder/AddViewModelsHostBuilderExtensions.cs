using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.State.Navigators;
using API.Model;
using Google.Cloud.Firestore;
using MainView;
using ViewModels;
using Models.Services;
using System.Windows;
using Models;

namespace MainView.HostBuilder
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<DashboardViewModel>();
                services.AddSingleton<AddDriverViewModel>();
                services.AddSingleton<AddVehicleViewModel>();
                services.AddSingleton<VehicleAssignmentViewModel>();
                services.AddTransient<VehicleListViewModel>();
                services.AddTransient<DriverListViewModel>();
                services.AddSingleton<MainDriverOverviewViewModel>();
                services.AddSingleton<MainVehicleOverviewViewModel>();
                services.AddSingleton<RemindersListViewModel>();
                services.AddSingleton<LoginViewModel>();
                services.AddSingleton<SignUpViewModel>();
                services.AddTransient<AssignmentListViewModel>();
                services.AddSingleton<TripInformationViewModel>();
                services.AddTransient<ViewModels.DriverViewModel.AssignmentHistoryViewModel>();
                services.AddTransient<ViewModels.VehicleViewModel.AssignmentHistoryViewModel>();
                services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));


                
            });
            return host;
        }
    }
}
