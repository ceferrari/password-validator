using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules;
using System;
using Xunit;

namespace PasswordValidator.Domain.Tests.Models.PasswordsRules
{
    public class AllowedPasswordRuleTests
    {
        // padrão utilizado para nomeclatura dos testes: Roy Osherove's naming strategy
        // ref: https://osherove.com/blog/2005/4/3/naming-standards-for-unit-tests.html

        [Theory]
        [InlineData("a")]
        [InlineData("X")]
        [InlineData("1")]
        [InlineData("!")]
        [InlineData("abc")]
        [InlineData("XYZ")]
        [InlineData("123")]
        [InlineData("!@#")]
        [InlineData("aaaaaa")]
        [InlineData("abcXYZ123!@#")]
        public void Validate_Password_Valid(string value)
        {
            // Arrange
            Password password = new(value);

            AllowedPasswordRule rule = new()
            {
                Chars = "abcXYZ123!@#"
            };

            // Act
            bool isValid = rule.IsValid(password);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("X")]
        [InlineData("1")]
        [InlineData("!")]
        [InlineData("abc")]
        [InlineData("XYZ")]
        [InlineData("123")]
        [InlineData("!@#")]
        [InlineData("aaaaaa")]
        [InlineData("abcXYZ123!@#")]
        public void Validate_Password_Invalid(string value)
        {
            // Arrange
            Password password = new(value);
            AllowedPasswordRule rule = new()
            {
                Chars = "ABCxyz456$%^"
            };

            // Act
            bool isValid = rule.IsValid(password);

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void Validate_NullPassword_ExceptionThrown()
        {
            // Arrange
            AllowedPasswordRule rule = new()
            {
                Chars = ""
            };

            // Act
            void action() => rule.IsValid(null);

            // Assert
            Assert.Throws<NullReferenceException>(action);
        }

        [Fact]
        public void Construct_NullChars_ExceptionThrown()
        {
            // Arrange
            AllowedPasswordRule rule;

            // Act
            void action() => rule = new()
            {
                Chars = null
            };

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}
