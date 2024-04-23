using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    namespace ViewModels.Commands
    {
        public abstract class AsyncCommandBase : ICommand
        {
            private bool _isExecuting;
            private readonly Action<Exception> _onException;

            public bool IsExecuting
            {
                get
                {
                    return _isExecuting;
                }
                set
                {
                    _isExecuting = value;
                    OnCanExecuteChanged();
                }
            }

            public event EventHandler CanExecuteChanged;

            protected AsyncCommandBase(Action<Exception> onException)
            {
                this._onException = onException;
            }

            public virtual bool CanExecute(object parameter)
            {
                return !IsExecuting;
            }

            public async void Execute(object parameter)
            {
                IsExecuting = true;
                try
                {
                    await ExecuteAsync(parameter);
                }
                catch (Exception ex)
                {
                    _onException?.Invoke(ex);
                }


                IsExecuting = false;
            }

            public abstract Task ExecuteAsync(object parameter);

            protected void OnCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }
    }

}
