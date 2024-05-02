﻿using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Services;
using Models.Services;
using Microsoft.Extensions.DependencyInjection;
using API.Model;
using Google.Cloud.Firestore;
using System.Text.Json;
using System.IO;
using ViewModels.State.Authentication;
using Models.Services.Firebase;
using Models.Services.AuthenticationServices;
using ViewModels.State.Accounts;

namespace MainView.HostBuilder
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            //FirebaseSettings firebaseSettings = new FirebaseSettings();
            var jsonString = File.ReadAllText("D:/Misa/Code/fleetmanagement-8b359-firebase-adminsdk-r32vj-cab42b1a38.json");
            //var firebaseJson = JsonSerializer.Serialize(firebaseSettings);
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IDataFromNHTSAService, DataFromNHTSAService>();
                services.AddSingleton<IVINDecoderService, VINDecoderService>();
                services.AddSingleton(_ => new FirestoreDbBuilder{
                    ProjectId = "fleetmanagement-8b359",
                    JsonCredentials = jsonString // <-- service account json file
                }.Build());
                services.AddSingleton<IAuthenticationService, AuthenticationService>();
                services.AddSingleton<IAuthenticator, Authenticator>();
                services.AddSingleton<IStoringDataManagementService, StoringDataManagementService>();
                services.AddSingleton<IAccountStore, AccountStore>();
                services.AddSingleton<IAccountDataService, AccountDataService>();
                services.AddSingleton<IDistanceService, DistanceService>();
            });

            return host;
        }
    }
}