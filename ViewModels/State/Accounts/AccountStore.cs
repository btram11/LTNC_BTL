using Models;
using Models.ModelFirebase;
using Models.Services.Firebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.State.Accounts
{
    public class AccountStore : IAccountStore
    {
        private Account _currentAccount;
        private readonly IStoringDataManagementService _storingDataManagementService;

        public Account CurrentAccount
        {
            get
            {
                return _currentAccount;
            }
            set
            {
                _currentAccount = value;
                _storingDataManagementService.DataUid = _currentAccount.DataUid;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
        public AccountStore(IStoringDataManagementService storingDataManagementService)
        {
            _storingDataManagementService = storingDataManagementService;
        }

    }
}
