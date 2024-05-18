using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace API.Model
{
    public class MakeDataFromNHTSA : IResultFromNHTSA
    {
        public int Count { get; set; }
        public string Message { get; set; }
        //public string SearchCriteria { get; set; }
        public ObservableCollection<MakeData> Results { get; set; }
    }

}
