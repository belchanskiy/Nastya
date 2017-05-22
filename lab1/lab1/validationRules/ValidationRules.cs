using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace lab1
{
    public class MailValueValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string mailValue = value as string;
            if (mailValue == null)
                return new ValidationResult(false, null);

            Regex mailRegExp = new Regex(@"^([a-zA-Z0-9_-]+\.)*[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)*\.[a-zA-Z]{2,6}$");

            if (!mailRegExp.IsMatch(mailValue))
                return new ValidationResult(false, "Адрес электронной почты введен неверно");

            return new ValidationResult(true, null);
        }
    }

    public class LoginValueValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string loginValue = value as string;
            if (loginValue == null)
                return new ValidationResult(false, null);

            if (loginValue.Length < 5 || loginValue.Length > 20)
                return new ValidationResult(false, "Имя пользователя должно содержать от 5 до 20 символов");

            Regex loginRegExp = new Regex(@"^([a-zA-Z0-9_-]){5,20}$");

            if (!loginRegExp.IsMatch(loginValue))
                return new ValidationResult(false, "Имя пользователя может состоять из латинских символов, цифр и знаков \"-\" и \"_\"");

            return new ValidationResult(true, null);
        }
    }

    public class PasswordValueValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string passwordValue = value as string;
            if (passwordValue == null)
                return new ValidationResult(false, null);

            if (passwordValue.Length < 8)
                return new ValidationResult(false, "Пароль должен содержать не менее 8 символов");

            Regex passwordRegExp = new Regex(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");

            if (!passwordRegExp.IsMatch(passwordValue))
                return new ValidationResult(false, "Пароль должен содержать строчные, прописные латинские буквы, а также цифры и знаки специального алфавита");

            return new ValidationResult(true, null);
        }
    }
}
