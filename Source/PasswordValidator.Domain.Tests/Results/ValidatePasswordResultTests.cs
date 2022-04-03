using PasswordValidator.Domain.Results;
using System.Collections.Generic;
using Xunit;

namespace PasswordValidator.Domain.Tests.Results
{
    public class ValidatePasswordResultTests
    {
        // padrão utilizado para nomeclatura dos testes: Roy Osherove's naming strategy
        // ref: https://osherove.com/blog/2005/4/3/naming-standards-for-unit-tests.html

        [Fact]
        public void Deserialize_JsonStringNoError_ResultObjectValid()
        {
            // Arrange
            IEnumerable<string> errors = new List<string>();

            ValidatePasswordResult expected = new(errors);

            string json = "{\"is_valid\":\"true\"}";

            // Act
            ValidatePasswordResult result = ValidatePasswordResult.FromJson(json);

            // Assert
            Assert.Equal(expected.ToJson(), result.ToJson());
        }

        [Fact]
        public void Deserialize_JsonStringWithError_ResultObjectInvalid()
        {
            // Arrange
            string error = "test_error";

            IEnumerable<string> errors = new List<string>() { error };

            ValidatePasswordResult expected = new(errors);

            string json = "{\"is_valid\":\"false\",\"errors\":[\"" + error + "\"]}";

            // Act
            ValidatePasswordResult result = ValidatePasswordResult.FromJson(json);

            // Assert
            Assert.Equal(expected.ToJson(), result.ToJson());
        }

        [Fact]
        public void Construct_NullErros_Valid()
        {
            // Arrange
            IEnumerable<string> errors = null;

            // Act
            ValidatePasswordResult result = new(errors);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Construct_EmptyErros_Valid()
        {
            // Arrange
            IEnumerable<string> errors = new List<string>();

            // Act
            ValidatePasswordResult result = new(errors);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Construct_NotNullNotEmptyErros_Invalid()
        {
            // Arrange
            IEnumerable<string> errors = new List<string>() { "" };

            // Act
            ValidatePasswordResult result = new(errors);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}
