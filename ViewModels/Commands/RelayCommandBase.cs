using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public abstract class RelayCommandBase : ICommand
    {
        protected readonly Func<object, bool> _canExecute;
        protected readonly Action<object> _execute;

        public RelayCommandBase(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public virtual bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute == null ? true : _canExecute(parameter);
            }
            catch
            {
                return true;
            }
        }
        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

}
