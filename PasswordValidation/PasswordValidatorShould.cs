using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace PasswordValidation
{
    // there needs to be at least 1 uppercase letter
    // there needs to be at least 1 lowercase letter
    // there needs to be at least 1 number
    // the password needs to be at least 8 characters long
    // at least one sign character

    // each validation counts one point, at least has to be 3 points
    public class PasswordValidatorShould
    {
        private readonly PasswordValidator _passwordValidator;

        public PasswordValidatorShould()
        {
            _passwordValidator = new PasswordValidator();
        }

        [Fact]
        public void validate_that_password_that_has_not_at_least_8_characters_long_is_not_valid()
        {
            var password = "Aa1";
            var isValid = _passwordValidator.Validate(password);
            isValid.Should().Be(false);
        }

        [Fact]
        public void validate_that_password_that_has_not_at_least_one_uppercase_letter_is_not_valid()
        {
            var password = "analyticalways1";
            var isValid = _passwordValidator.Validate(password);
            isValid.Should().Be(false);
        }

        [Fact]
        public void validate_that_password_that_has_not_at_least_one_lowercase_letter_is_not_valid()
        {
            var password = "ANALYTICALWAYS1";
            var isValid = _passwordValidator.Validate(password);
            isValid.Should().Be(false);
        }

        [Fact]
        public void validate_that_password_that_has_not_at_least_one_number_is_not_valid()
        {
            var password = "aNALYTICALWAYS";
            var isValid = _passwordValidator.Validate(password);
            isValid.Should().Be(false);
        }

        [Fact]
        public void validate_that_password_that_has_not_at_least_one_sign_character_is_not_valid()
        {
            var password = "aNALYTICALW1YS";
            var isValid = _passwordValidator.Validate(password);
            isValid.Should().Be(false);
        }

        [Fact]
        public void validate_that_password_has_at_least_three_points_of_sum()
        {
            var password = "Aa1";
            var isValid = _passwordValidator.Validate(password);
            isValid.Should().Be(true);
        }
    }

    public class PasswordValidator
    {
        private readonly string _allowedSigns;

        public PasswordValidator() : this("+-/&%")
        {

        }
        public PasswordValidator(string allowedSigns)
        {
            _allowedSigns = allowedSigns;
        }
        public bool Validate(string password)
        {
            int counter = 0;
            if (password.Length >= 8)
            {
                counter++;
            }
            if (HasUppercaseLetter(password))
            {
                counter++;
            }
            if (HasLowercaseLetter(password))
            {
                counter++;
            }
            if (HasNumber(password))
            {
                counter++;
            }
            if (HasSign(password))
            {
                counter++;
            }
            return counter >= 3;
        }

        private bool HasSign(string password)
        {
            return password.Intersect(_allowedSigns).Any();
        }

        private static bool HasNumber(string password)
        {
            var regex = new Regex(@"([0-9])+");
            return regex.IsMatch(password);
        }

        private static bool HasLowercaseLetter(string password)
        {
            var regex = new Regex(@"([a-z])+");
            return regex.IsMatch(password);
        }

        private static bool HasUppercaseLetter(string password)
        {
            var regex = new Regex(@"([A-Z])+");
            return regex.IsMatch(password);
        }
    }
}
