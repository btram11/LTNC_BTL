using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Commands;

namespace ViewModels
{
    public class RelayCommand<T> : RelayCommandBase
    {
        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        : base(obj => execute((T)obj), obj => canExecute?.Invoke((T)obj) ?? true)
        {
        }
        public override bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute == null ? true : _canExecute((T)parameter);
            }
            catch
            {
                return true;
            }
        }

        public override void Execute(object parameter) => _execute((T)parameter);
    }

    public class RelayCommand : RelayCommandBase
    {
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            : base(execute, canExecute)
        {
        }

        public override void Execute(object parameter) => _execute(parameter);
    }
}
