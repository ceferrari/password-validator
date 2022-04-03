using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Requests;
using System;
using Xunit;

namespace PasswordValidator.Domain.Tests.Requests
{
    public class ValidatePasswordRequestTests
    {
        // padrão utilizado para nomeclatura dos testes: Roy Osherove's naming strategy
        // ref: https://osherove.com/blog/2005/4/3/naming-standards-for-unit-tests.html

        [Theory]
        [InlineData("")]
        [InlineData("1234abcdWXYZ!@#$")]
        public void Construct_NotNullPassword_NoException(string value)
        {
            // Arrange
            Password password = new(value);

            // Act
            ValidatePasswordRequest request = new(password);

            // Assert
            Assert.NotNull(request.Password);
        }

        [Fact]
        public void Construct_NullPassword_ExceptionThrown()
        {
            // Arrange
            ValidatePasswordRequest request;

            // Act
            void action() => request = new(null);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}
