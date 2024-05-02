using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.State.Accounts;
using ViewModels.State.Authentication;

namespace ViewModels.State.Data
{
    public class DataStore : IDataStore
    {
        private readonly IAccountStore _accountStore;
        private string _id => _accountStore.CurrentAccount?.Id ?? "";

        public event Action StateChanged;
        public DataStore(IAccountStore accountStore)
        {
            _accountStore = accountStore;
            _accountStore.StateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke();
        }
    }
}
