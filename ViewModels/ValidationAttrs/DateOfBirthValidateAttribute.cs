using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ViewModels.ValidationAttrs
{
    public class DateOfBirthValidateAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public DateOfBirthValidateAttribute(string comparisonProperty = "")
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var comparisonvalue = validationContext.ObjectType.GetProperty(_comparisonProperty)
                .GetValue(validationContext.ObjectInstance);
            if (value == null)
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));

            Gender comparisonValue = Gender.Male;
            if (comparisonvalue != null)
            {
                comparisonValue = (Gender)comparisonvalue;
            }

            var currentValue = (DateTime)value;

            DateTime today = DateTime.Today;
            int age = today.Year - currentValue.Year;

            if (currentValue.Date > today.AddYears(-age))
                age--;
            if (age < 18 || age > 55)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}
