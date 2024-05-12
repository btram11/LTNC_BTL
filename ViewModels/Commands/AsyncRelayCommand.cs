using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Commands.ViewModels.Commands;

namespace ViewModels.Commands
{
    public class AsyncRelayCommand : AsyncCommandBase
    {
        private readonly Func<Task> _callback;

        public AsyncRelayCommand(Func<Task> callback/*, Action<Exception> onException*/) /*: base(onException)*/
        {
            _callback = callback;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            await _callback();
        }
    }


    public class AsyncRelayCommand<T> : AsyncCommandBase
    {
        private readonly Func<T, Task> _callback;

        public AsyncRelayCommand(Func<T, Task> callback)
        {
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _callback((T)parameter);
        }
    }

}
