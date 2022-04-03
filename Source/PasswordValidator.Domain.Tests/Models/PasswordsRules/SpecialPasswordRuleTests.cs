using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules;
using System;
using Xunit;

namespace PasswordValidator.Domain.Tests.Models.PasswordsRules
{
    public class SpecialPasswordRuleTests
    {
        // padrão utilizado para nomeclatura dos testes: Roy Osherove's naming strategy
        // ref: https://osherove.com/blog/2005/4/3/naming-standards-for-unit-tests.html

        [Theory]
        [InlineData("!@#")]
        [InlineData("!@#$")]
        [InlineData("!@#$%^")]
        [InlineData("!@#$%^&*")]
        [InlineData("!@#$%^&*()")]
        [InlineData("!@#$%^&*()-+")]
        [InlineData("!@#$%^&*()-+!@#$%^&*()-+")]
        public void Validate_Password_Valid(string value)
        {
            // Arrange
            Password password = new(value);

            SpecialPasswordRule rule = new()
            {
                Chars = "!@#$%^&*()-+",
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
        [InlineData("!")]
        [InlineData("!@")]
        [InlineData("abc")]
        [InlineData("!abc")]
        [InlineData("!@abc")]
        public void Validate_Password_Invalid(string value)
        {
            // Arrange
            Password password = new(value);

            SpecialPasswordRule rule = new()
            {
                Chars = "!@#$%^&*()-+",
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
            SpecialPasswordRule rule = new()
            {
                Chars = "",
                Min = 3
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
            SpecialPasswordRule rule;

            // Act
            void action() => rule = new()
            {
                Chars = null,
                Min = 3
            };

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}
