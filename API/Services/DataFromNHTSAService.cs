using Models;
using Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Services;
using API.Model;
using GoogleApi.Entities.Maps.Directions.Response;
using System.Collections.ObjectModel;

namespace API.Services
{
    public class DataFromNHTSAService : IDataFromNHTSAService
    {
        private readonly VehicleModelingPrepHttpClient _client;
        public DataFromNHTSAService(VehicleModelingPrepHttpClient Client)
        {
            _client = Client;
        }
        public async Task<ObservableCollection<string>> GetAllMakesListFromNHTSA()
        {
            string url = $"getallmakes?format=json";
            MakeDataFromNHTSA data = await _client.GetAsync<MakeDataFromNHTSA>(url);

            if (data.Message != "Response returned successfully")
            {
                throw new Exception(data.Message);
            }
            ObservableCollection<string> makeList = new ObservableCollection<string>(
                data.Results.Select(makeData => makeData.Make_Name)
            );
            return makeList;
        }

        public async Task<ObservableCollection<string>> GetDataListByNameFromNHTSA(string name)
        {
            string url = $"getvehiclevariablevalueslist/{name}?format=json";
            GeneralDataFromNHTSA data = await _client.GetAsync<GeneralDataFromNHTSA>(url);
            if (data.Message != "Results returned successfully")
            {
                throw new Exception(data.Message);
            }
            else if (data.Count == 0)
            {
                throw new Exception($"Please enter {name} as our system dont include it yet");
            }
            ObservableCollection<string> dataList = new ObservableCollection<string>(
                data.Results.Select(gData => gData.Name)
            ); 
            return dataList;
        }

        public async Task<List<string>> GetAllModelsFromNHTSA(string make)
        {
            string url = $"GetModelsForMake/{make}?format=json";
            ModelDataFromNHTSA data = await _client.GetAsync<ModelDataFromNHTSA>(url);

            if (data.Message != "Response returned successfully" || data.Count == 0)
            {
                throw new Exception(data.Message);
            }
            List<string> models = data.Results.Select(obj => obj.Model_Name).ToList();
            return models;


        }

    }
}
