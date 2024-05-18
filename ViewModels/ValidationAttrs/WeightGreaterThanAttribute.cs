using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ViewModels.ValidationAttrs
{
    public class WeightStringGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        private readonly int _percentage;
        public WeightStringGreaterThanAttribute(string comparisonProperty, int percentage = 0)
        {
            _comparisonProperty = comparisonProperty;
            _percentage = percentage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var pattern = "^[0-9]*\\.?[0-9]+$";
 
            var regex = new Regex(pattern);

            if (value == null || !regex.IsMatch((string)value) || string.IsNullOrEmpty(_comparisonProperty))
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            var currentValue = int.Parse(((string)value == string.Empty || value == null) ? "0" : (string)value);
            if (currentValue == 0) return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));

            var stringComValue = validationContext.ObjectType.GetProperty(_comparisonProperty).GetValue(validationContext.ObjectInstance) as string;
            if (string.IsNullOrEmpty(stringComValue))
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            var comparisonValue = int.Parse(stringComValue);

            if (currentValue <= comparisonValue - comparisonValue * _percentage)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}
