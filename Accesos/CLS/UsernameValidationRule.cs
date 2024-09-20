using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Accesos.CLS
{
    public class UsernameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string username = value as string;
            if (string.IsNullOrWhiteSpace(username))
            {
                return new ValidationResult(false, "El campo no puede estar vacío.");
            }
            if (username.Contains(" "))
            {
                return new ValidationResult(false, "El nombre de usuario no puede contener espacios.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
