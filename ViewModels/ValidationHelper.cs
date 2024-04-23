using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ValidationHelper : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        public bool HasErrors => _propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (_propertyErrors.ContainsKey(propertyName))
            {
                return _propertyErrors[propertyName];
            }
            return Enumerable.Empty<string>();
        }

        public void AddError(string errorMessage, [CallerMemberName] string property = "")
        {
            if (!_propertyErrors.ContainsKey(property))
            {
                _propertyErrors.Add(property, new List<string>());
            }

            _propertyErrors[property].Add(errorMessage);
            OnErrorsChanged(property);
        }

        public void ClearErrors([CallerMemberName]string propertyName = "")
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
