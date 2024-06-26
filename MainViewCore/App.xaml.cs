﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModels;
using MainView.View;
using Microsoft.Extensions.DependencyInjection;
using ViewModels.State.Navigators;
using Google.Cloud.Firestore;
using API.Model;
using System.IO;
using Microsoft.Extensions.Hosting;
using MainView.HostBuilder;
using Models.Services;
using Models.Vehicles;
using API.Services;
using Microsoft.Extensions.Configuration;

namespace MainView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            var temp = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configBuilder.Build();

            _host = CreateHostBuilder(configuration).Build();

            //FirestoreDb Database = FirestoreDb.Create();


        }

        public static IHostBuilder CreateHostBuilder(IConfigurationRoot configuration, string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .AddVehicleAPI()
                .AddServices(configuration)
                .AddViewModels();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            //MainWindow = new MainWindow()
            //{
            //    DataContext = new MainViewModel()
            //};
            //Window MainWindow = new LoginWindow();
            //var fuel = _host.Services.GetRequiredService<IFuelPriceService>();
            //var price = await fuel.GetPrice();
            var MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.DataContext = _host.Services.GetRequiredService<MainViewModel>();
            MainWindow.Show();
            base.OnStartup(e);
        }


        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
