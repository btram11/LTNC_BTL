using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
    public interface IDataFromNHTSAService
    {
        Task<ObservableCollection<string>> GetAllMakesListFromNHTSA();
        Task<ObservableCollection<string>> GetDataListByNameFromNHTSA(string name);
    }
}
