using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace JamesSmartTextBox.Local.Validators;

public interface IValidator
{
    string Message { get; }
    bool Validate(string value);
}

public class RequiredValidator : IValidator
{
    public string Message { get; set; } = "This field is required.";

    public bool Validate(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }
}

public class EmailValidator : IValidator
{
    public string Message { get; set; } = "Please enter a valid email address.";

    public bool Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}

public class PasswordValidator : IValidator
{
    public string Message { get; set; } = "Password must be at least 8 characters with uppercase, lowercase, and a number.";

    public bool Validate(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length < 8)
            return false;

        return value.Any(char.IsUpper)
            && value.Any(char.IsLower)
            && value.Any(char.IsDigit);
    }
}

public class UrlValidator : IValidator
{
    public string Message { get; set; } = "Please enter a valid URL.";

    public bool Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return Uri.TryCreate(value, UriKind.Absolute, out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }
}

public class PassportValidator : IValidator
{
    public string Message { get; set; } = "Please enter a valid passport number (1-2 letters followed by 6-9 digits).";

    public bool Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return Regex.IsMatch(value.Trim(), @"^[A-Za-z]{1,2}\d{6,9}$");
    }
}

public class CreditCardValidator : IValidator
{
    public string Message { get; set; } = "Please enter a valid credit card number.";

    public bool Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        string digits = new(value.Where(char.IsDigit).ToArray());

        if (digits.Length < 13 || digits.Length > 19)
            return false;

        int sum = 0;
        bool alternate = false;

        for (int i = digits.Length - 1; i >= 0; i--)
        {
            int n = digits[i] - '0';

            if (alternate)
            {
                n *= 2;
                if (n > 9)
                    n -= 9;
            }

            sum += n;
            alternate = !alternate;
        }

        return sum % 10 == 0;
    }
}
