using Models.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class DataFromNHTSAViewModel: ViewModelBase
    {
        private readonly IDataFromNHTSAService _dataFromNHTSAService;
        public ObservableCollection<string> MakeList { get; set; } 

        public DataFromNHTSAViewModel(IDataFromNHTSAService dataFromNHTSAService)
        {
            _dataFromNHTSAService = dataFromNHTSAService;
        }

        public static DataFromNHTSAViewModel LoadDataFromNHTSAViewModel(IDataFromNHTSAService dataFromNHTSAService)
        {
            DataFromNHTSAViewModel dataFromNHTSAViewModel = new DataFromNHTSAViewModel(dataFromNHTSAService);
            dataFromNHTSAViewModel.LoadDataFromNHTSA();
            return dataFromNHTSAViewModel;
        }

        private void LoadDataFromNHTSA()
        {
            _dataFromNHTSAService.GetAllMakesListFromNHTSA().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    MakeList = task.Result;
                }
            });
        }
    }
}
