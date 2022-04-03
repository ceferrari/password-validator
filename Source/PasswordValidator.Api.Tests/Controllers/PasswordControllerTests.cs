using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Results;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PasswordValidator.Api.Tests.Controllers
{
    public class PasswordControllerTests
    {
        // padrão utilizado para nomeclatura dos testes: Roy Osherove's naming strategy
        // ref: https://osherove.com/blog/2005/4/3/naming-standards-for-unit-tests.html

        private readonly string _uriValidate = "/Password/Validate";
        private readonly HttpClient _httpClient;

        public PasswordControllerTests()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            IWebHostBuilder webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseConfiguration(configuration)
                .UseStartup<Startup>();

            TestServer testServer = new(webHostBuilder);
            _httpClient = testServer.CreateClient();
        }

        [Theory]
        [InlineData("strong@PWD#1")]
        [InlineData("123abcXYZ!@#")]
        [InlineData("1234abcdWXYZ!@#$")]
        public async Task Validate_Password_200OK(string value)
        {
            // Arrange
            Password password = new(value);
            string requestJson = password.ToJson();
            StringContent body = new(requestJson, Encoding.UTF8, MediaTypeNames.Application.Json);

            // Act
            HttpResponseMessage response = await _httpClient.PostAsync(_uriValidate, body);
            string responseJson = await response.Content.ReadAsStringAsync();
            ValidatePasswordResult result = ValidatePasswordResult.FromJson(responseJson);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(result.IsValid);
            Assert.Null(result.Errors);
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
        public async Task Validate_Password_422UnprocessableEntity(string value)
        {
            // Arrange
            Password password = new(value);
            string requestJson = password.ToJson();
            StringContent body = new(requestJson, Encoding.UTF8, MediaTypeNames.Application.Json);

            // Act
            HttpResponseMessage response = await _httpClient.PostAsync(_uriValidate, body);
            string responseJson = await response.Content.ReadAsStringAsync();
            ValidatePasswordResult result = ValidatePasswordResult.FromJson(responseJson);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Any());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task Validate_NullOrEmptyPassword_400BadRequest(string value)
        {
            // Arrange
            Password password = new(value);
            string requestJson = password.ToJson();
            StringContent body = new(requestJson, Encoding.UTF8, MediaTypeNames.Application.Json);

            // Act
            HttpResponseMessage response = await _httpClient.PostAsync(_uriValidate, body);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
