using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules;
using System;
using Xunit;

namespace PasswordValidator.Domain.Tests.Models.PasswordsRules
{
    public class LowercasePasswordRuleTests
    {
        // padrão utilizado para nomeclatura dos testes: Roy Osherove's naming strategy
        // ref: https://osherove.com/blog/2005/4/3/naming-standards-for-unit-tests.html

        [Theory]
        [InlineData("aaa")]
        [InlineData("abc")]
        [InlineData("abcd")]
        [InlineData("abcde")]
        [InlineData("abcdef")]
        public void Validate_Password_Valid(string value)
        {
            // Arrange
            Password password = new(value);

            LowercasePasswordRule rule = new()
            {
                Min = 3
            };

            // Act
            bool isValid = rule.IsValid(password);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("AAA")]
        [InlineData("ABC")]
        [InlineData("ABCDEF")]
        public void Validate_Password_Invalid(string value)
        {
            // Arrange
            Password password = new(value);

            LowercasePasswordRule rule = new()
            {
                Min = 3
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
            LowercasePasswordRule rule = new()
            {
                Min = 3
            };

            // Act
            void action() => rule.IsValid(null);

            // Assert
            Assert.Throws<NullReferenceException>(action);
        }

        [Fact]
        public void Construct_NegativeMin_ExceptionThrown()
        {
            // Arrange
            LowercasePasswordRule rule;

            // Act
            void action() => rule = new()
            {
                Min = -1
            };

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
        }
    }
}
