using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules;
using System;
using Xunit;

namespace PasswordValidator.Domain.Tests.Models.PasswordsRules
{
    public class LengthPasswordRuleTests
    {
        // padrão utilizado para nomeclatura dos testes: Roy Osherove's naming strategy
        // ref: https://osherove.com/blog/2005/4/3/naming-standards-for-unit-tests.html

        [Theory]
        [InlineData("abc")]
        [InlineData("abcX")]
        [InlineData("abcXY")]
        [InlineData("abcXYZ")]
        [InlineData("abcXYZ1")]
        [InlineData("abcXYZ12")]
        [InlineData("abcXYZ123")]
        [InlineData("abcXYZ123!")]
        [InlineData("abcXYZ123!@")]
        [InlineData("abcXYZ123!@#")]
        public void Validate_Password_Valid(string value)
        {
            // Arrange
            Password password = new(value);

            LengthPasswordRule rule = new()
            {
                Min = 3,
                Max = 12
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
        [InlineData("ab")]
        [InlineData("aaaaaaaaaaaaa")]
        [InlineData("abcdWXYZ1234!@#$")]
        public void Validate_Password_Invalid(string value)
        {
            // Arrange
            Password password = new(value);

            LengthPasswordRule rule = new()
            {
                Min = 3,
                Max = 12
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
            LengthPasswordRule rule = new()
            {
                Min = 3,
                Max = 12
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
            LengthPasswordRule rule;

            // Act
            void action() => rule = new()
            {
                Min = -1,
                Max = 12
            };

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Fact]
        public void Construct_NegativeMax_ExceptionThrown()
        {
            // Arrange
            LengthPasswordRule rule;

            // Act
            void action() => rule = new()
            {
                Min = 3,
                Max = -1
            };

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Fact]
        public void Construct_MaxLowerThanMin_ExceptionThrown()
        {
            // Arrange
            LengthPasswordRule rule;

            // Act
            void action() => rule = new()
            {
                Min = 3,
                Max = 2
            };

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
        }
    }
}
