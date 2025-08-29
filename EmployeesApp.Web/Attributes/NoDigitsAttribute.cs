using System.ComponentModel.DataAnnotations;

namespace EmployeesApp.Web.Attributes
{
    public class NoDigitsAttribute() : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            return value is string s 
                && !s.Any(c => char.IsAsciiDigit(c));
        }
    }
}
