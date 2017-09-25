using System;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace PasswordValidation
{
    // there needs to be at least 1 uppercase letter
    // there needs to be at least 1 lowercase letter
    // there needs to be at least 1 number
    // the password needs to be at least 8 characters long
    public class PasswordValidatorShould
    {
        private readonly PasswordValidator _passwordValidator;

        public PasswordValidatorShould()
        {
            _passwordValidator = new PasswordValidator();
        }

        [Fact]
        public void validate_that_password_has_not_at_least_8_characters_long_is_not_valid()
        {
            var password = "aa";
            var isValid = _passwordValidator.Validate(password);
            isValid.Should().Be(false);
        }

        [Fact]
        public void validate_that_password_that_has_not_at_least_one_uppercase_letter_is_not_valid()
        {
            var password = "analyticalways";
            var isValid = _passwordValidator.Validate(password);
            isValid.Should().Be(false);
        }
    }
    public class PasswordValidator
    {
        public bool Validate(string password)
        {
            var regex = new Regex(@"([A-Z])+");
            return (regex.IsMatch(password) && password.Length >= 8);
        }
    }
}
