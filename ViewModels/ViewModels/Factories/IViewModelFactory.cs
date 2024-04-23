using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.State.Navigators;

namespace ViewModels.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
