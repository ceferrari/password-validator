using PasswordValidator.Domain.Models.Passwords;
using Xunit;

namespace PasswordValidator.Domain.Tests.Models.Passwords
{
    public class PasswordTests
    {
        // padrão utilizado para nomeclatura dos testes: Roy Osherove's naming strategy
        // ref: https://osherove.com/blog/2005/4/3/naming-standards-for-unit-tests.html

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("a")]
        [InlineData("X")]
        [InlineData("1")]
        [InlineData("!")]
        [InlineData("abc")]
        [InlineData("XYZ")]
        [InlineData("123")]
        [InlineData("!@#")]
        [InlineData("abcXYZ123!@#")]
        [InlineData("1234abcdWXYZ!@#$")]
        public void Serialize_PasswordObject_JsonString(string value)
        {
            // Arrange
            string expected = "{\"value\":\"" + value + "\"}";

            Password password = new(value);

            // Act
            string json = password.ToJson();

            // Assert
            Assert.Equal(expected, json);
        }
    }
}
