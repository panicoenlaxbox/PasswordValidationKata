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

    public class ObjectMother
    {
        public static PasswordValidator CreatePasswordValidator()
        {
            return new PasswordValidator("+-/&%");
        }
    }

    public class PasswordValidatorShould
    {
        private readonly PasswordValidator _passwordValidator = ObjectMother.CreatePasswordValidator();


        [Theory]
        [InlineData("Aa1", true)]
        [InlineData("AA1", false)]
        [InlineData("ANALYTICALWAYS", false)]
        [InlineData("+a1", true)]
        [InlineData(" ", false)]
        public void validate_that_password_has_at_least_three_points_of_sum(string password, bool expected)
        {
            var isValid = _passwordValidator.Validate(password);
            isValid.Should().Be(expected);
        }
    }

    public class PasswordValidator
    {
        private readonly string _allowedSigns;

        public PasswordValidator(string allowedSigns)
        {
            _allowedSigns = allowedSigns;
        }

        public bool Validate(string password)
        {
            var counter = 0;
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

        private bool HasSign(string password) => password.Intersect(_allowedSigns).Any();

        private static bool HasNumber(string password) => new Regex(@"([0-9])+").IsMatch(password);

        private static bool HasLowercaseLetter(string password) => new Regex(@"([a-z])+").IsMatch(password);

        private static bool HasUppercaseLetter(string password) => new Regex(@"([A-Z])+").IsMatch(password);
    }
}
