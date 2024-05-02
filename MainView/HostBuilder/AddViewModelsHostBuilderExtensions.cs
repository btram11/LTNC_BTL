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

namespace MainView.HostBuilder
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<DashboardViewModel>();
                services.AddSingleton<AddDriverViewModel>();
                services.AddSingleton<AddVehicleViewModel>();
                services.AddSingleton<VehicleAssignmentViewModel>();
                services.AddSingleton<VehicleListViewModel>();
                services.AddSingleton<DriverListViewModel>();
                services.AddSingleton<MainDriverOverviewViewModel>();
                services.AddSingleton<MainVehicleOverviewViewModel>();
                services.AddSingleton<RemindersListViewModel>();
                services.AddSingleton<VehicleMakeListingViewModel>();
                services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

                //services.AddSingleton(_ => new FirestoreProvider(
                //    new FirestoreDbBuilder
                //    {
                //        ProjectId = "fleetmanagement-8b359",
                //        JsonCredentials = jsonString // <-- service account json file
                //    }.Build()
                //));
                //services.AddSingleton<FirestoreProvider>();
                //services.AddSingleton<FirestoreDb>(_ => new FirestoreDbBuilder
                //{
                //    ProjectId = "fleetmanagement-8b359",
                //    JsonCredentials = jsonString // <-- service account json file
                //}.Build());

                services.AddSingleton<MainWindow>(provider => new MainWindow(provider.GetRequiredService<MainViewModel>()));
            });
            return host;
        }
    }
}
