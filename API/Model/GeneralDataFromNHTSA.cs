using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public class GeneralDataFromNHTSA : IResultFromNHTSA
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public ObservableCollection<GeneralDataModel> Results { get; set; }
    }

    public class GeneralDataModel
    {
        public string Name { get; set; }
    }

}
