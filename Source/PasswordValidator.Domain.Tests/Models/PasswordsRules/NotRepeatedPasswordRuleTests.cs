using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules;
using System;
using Xunit;

namespace PasswordValidator.Domain.Tests.Models.PasswordsRules
{
    public class NotRepeatedPasswordRuleTests
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
        [InlineData("abcABC")]
        [InlineData("XYZxyz")]
        [InlineData("123456")]
        [InlineData("!@#$%^")]
        [InlineData("abcXYZ123!@#")]
        public void Validate_Password_Valid(string value)
        {
            // Arrange
            Password password = new(value);

            NotRepeatedPasswordRule rule = new();

            // Act
            bool isValid = rule.IsValid(password);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("aa")]
        [InlineData("XX")]
        [InlineData("11")]
        [InlineData("!!")]
        [InlineData("abcabc")]
        [InlineData("XYZXYZ")]
        [InlineData("123123")]
        [InlineData("!@#!@#")]
        [InlineData("aabcXXYZ1123!!@#")]
        public void Validate_Password_Invalid(string value)
        {
            // Arrange
            Password password = new(value);

            NotRepeatedPasswordRule rule = new();

            // Act
            bool isValid = rule.IsValid(password);

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void Validate_NullPassword_ExceptionThrown()
        {
            // Arrange
            NotRepeatedPasswordRule rule = new();

            // Act
            void action() => rule.IsValid(null);

            // Assert
            Assert.Throws<NullReferenceException>(action);
        }
    }
}
