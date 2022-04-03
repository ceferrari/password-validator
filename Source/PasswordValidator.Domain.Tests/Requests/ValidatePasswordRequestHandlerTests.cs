using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Requests;
using PasswordValidator.Domain.Results;
using PasswordValidator.Domain.Settings;
using System.Threading.Tasks;
using Xunit;

namespace PasswordValidator.Domain.Tests.Requests
{
    public class ValidatePasswordRequestHandlerTests
    {
        // padrão utilizado para nomeclatura dos testes: Roy Osherove's naming strategy
        // ref: https://osherove.com/blog/2005/4/3/naming-standards-for-unit-tests.html

        private readonly IOptionsSnapshot<PasswordsRulesOptions> _passwordsRulesOptionsSnapshot;
        private readonly ILogger<ValidatePasswordRequestHandler> _logger;

        public ValidatePasswordRequestHandlerTests()
        {
            PasswordsRulesOptions passwordsRulesOptions = new()
            {
                Allowed = new() { IsActive = true, Chars = "!@#$%^&*()-+abcdefUVWXYZ123456" },
                Special = new() { IsActive = true, Chars = "!@#$%^&*()-+", Min = 1 },
                Lowercase = new() { IsActive = true, Min = 1 },
                Uppercase = new() { IsActive = true, Min = 1 },
                Numeric = new() { IsActive = true, Min = 1 },
                Length = new() { IsActive = true, Min = 9, Max = 20 },
                NotRepeated = new() { IsActive = true }
            };

            Mock<IOptionsSnapshot<PasswordsRulesOptions>> passwordsRulesOptionsSnapshotMock = new();
            passwordsRulesOptionsSnapshotMock.Setup(x => x.Value).Returns(passwordsRulesOptions);
            _passwordsRulesOptionsSnapshot = passwordsRulesOptionsSnapshotMock.Object;

            Mock<ILogger<ValidatePasswordRequestHandler>> loggerMock = new();
            _logger = loggerMock.Object;
        }

        [Theory]
        [InlineData("12abYZ!@#")]
        [InlineData("123abcXYZ!@#")]
        [InlineData("1234abcdWXYZ!@#$")]
        [InlineData("!@#$%abcdeVWXYZ12345")]
        public async Task Handle_Request_ValidResult(string value)
        {
            // Arrange
            Password password = new(value);

            ValidatePasswordRequest request = new(password);

            ValidatePasswordRequestHandler handler = new(_passwordsRulesOptionsSnapshot, _logger);

            // Act
            ValidatePasswordResult result = await handler.Handle(request, default);

            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("lowercase")]
        [InlineData("UPPERCASE")]
        [InlineData("12345")]
        [InlineData("!@#$%")]
        [InlineData("12abWX!@")]
        [InlineData("12345abcdeUVWXYZ!@#$%")]
        [InlineData("abcdWXYZ!@#$")]
        [InlineData("1234WXYZ!@#$")]
        [InlineData("1234abcd!@#$")]
        [InlineData("1234abcdWXYZ")]
        [InlineData("122abbWXX!@@")]
        [InlineData("12 ab WX !@")]
        [InlineData("         ")]
        [InlineData(" ")]
        public async Task Handle_Request_InvalidResult(string value)
        {
            // Arrange
            Password password = new(value);

            ValidatePasswordRequest request = new(password);

            ValidatePasswordRequestHandler handler = new(_passwordsRulesOptionsSnapshot, _logger);

            // Act
            ValidatePasswordResult result = await handler.Handle(request, default);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task Handle_RequestWithInactiveRuleAllowed_ValidResult()
        {
            // Arrange
            _passwordsRulesOptionsSnapshot.Value.Allowed.IsActive = false;

            Password password = new("123abc XYZ!@#");

            ValidatePasswordRequest request = new(password);

            ValidatePasswordRequestHandler handler = new(_passwordsRulesOptionsSnapshot, _logger);

            // Act
            ValidatePasswordResult result = await handler.Handle(request, default);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Handle_RequestWithInactiveRuleSpecial_ValidResult()
        {
            // Arrange
            _passwordsRulesOptionsSnapshot.Value.Special.IsActive = false;

            Password password = new("123abcXYZ");

            ValidatePasswordRequest request = new(password);

            ValidatePasswordRequestHandler handler = new(_passwordsRulesOptionsSnapshot, _logger);

            // Act
            ValidatePasswordResult result = await handler.Handle(request, default);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Handle_RequestWithInactiveRuleLowercase_ValidResult()
        {
            // Arrange
            _passwordsRulesOptionsSnapshot.Value.Lowercase.IsActive = false;

            Password password = new("123XYZ!@#");

            ValidatePasswordRequest request = new(password);

            ValidatePasswordRequestHandler handler = new(_passwordsRulesOptionsSnapshot, _logger);

            // Act
            ValidatePasswordResult result = await handler.Handle(request, default);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Handle_RequestWithInactiveRuleUppercase_ValidResult()
        {
            // Arrange
            _passwordsRulesOptionsSnapshot.Value.Uppercase.IsActive = false;

            Password password = new("123abc!@#");

            ValidatePasswordRequest request = new(password);

            ValidatePasswordRequestHandler handler = new(_passwordsRulesOptionsSnapshot, _logger);

            // Act
            ValidatePasswordResult result = await handler.Handle(request, default);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Handle_RequestWithInactiveRuleNumeric_ValidResult()
        {
            // Arrange
            _passwordsRulesOptionsSnapshot.Value.Numeric.IsActive = false;

            Password password = new("abcXYZ!@#");

            ValidatePasswordRequest request = new(password);

            ValidatePasswordRequestHandler handler = new(_passwordsRulesOptionsSnapshot, _logger);

            // Act
            ValidatePasswordResult result = await handler.Handle(request, default);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Handle_RequestWithInactiveRuleLength_ValidResult()
        {
            // Arrange
            _passwordsRulesOptionsSnapshot.Value.Length.IsActive = false;

            Password password = new("!@#$%^&*()-+abcdefUVWXYZ123456");

            ValidatePasswordRequest request = new(password);

            ValidatePasswordRequestHandler handler = new(_passwordsRulesOptionsSnapshot, _logger);

            // Act
            ValidatePasswordResult result = await handler.Handle(request, default);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Handle_RequestWithInactiveRuleNotRepeated_ValidResult()
        {
            // Arrange
            _passwordsRulesOptionsSnapshot.Value.NotRepeated.IsActive = false;

            Password password = new("1123abcXYZ!@#");

            ValidatePasswordRequest request = new(password);

            ValidatePasswordRequestHandler handler = new(_passwordsRulesOptionsSnapshot, _logger);

            // Act
            ValidatePasswordResult result = await handler.Handle(request, default);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
