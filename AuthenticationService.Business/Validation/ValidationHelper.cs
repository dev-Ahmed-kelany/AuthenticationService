using System.Text.RegularExpressions;

namespace AuthenticationService.Business.Validation
{
    public static class ValidationHelper
    {
        public static bool IsLengthBetween(string? value, int minLength, int maxLength)
        {
            return HasMinimumLength(value, minLength) && HasMaximumLength(value, maxLength);
        }

        public static bool HasMinimumLength(string? value, int minLength)
        {
            return value?.Length >= minLength;
        }

        public static bool HasMaximumLength(string? value, int maxLength)
        {
            
            return value?.Length <= maxLength;
        }

        public static bool IsValidEmail(string? email) 
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9._]+\.[A-Za-z]{2,}$");
        }

        public static bool IsValidUsername(string? username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            return Regex.IsMatch(username, @"^[A-Za-z0-9_]+$");
        }

        public static bool IsStrongPassword(string? password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }



    }
}
