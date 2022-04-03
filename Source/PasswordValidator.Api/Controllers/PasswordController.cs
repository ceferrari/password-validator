using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Requests;
using PasswordValidator.Domain.Results;
using System;
using System.Threading.Tasks;

namespace PasswordValidator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PasswordController : ControllerBase
    {
        // o MediatR foi a única biblioteca não-nativa do .NET utilizada no projeto

        private readonly IMediator _mediator;
        private readonly ILogger<PasswordController> _logger;

        public PasswordController(IMediator mediator, ILogger<PasswordController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Validate")]
        [ProducesResponseType(typeof(ValidatePasswordResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidatePasswordResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Validate([FromBody] Password password)
        {
            // o try catch abaixo atende as necessidade de tratamento de erros para o escopo deste projeto, que é simples
            // em um projeto de maior complexidade, o ideal seria utilizar uma abordagem mais robusta, como um Middleware

            try
            {
                if (string.IsNullOrEmpty(password?.Value)) return BadRequest("Bad Request");

                ValidatePasswordRequest request = new(password);
                ValidatePasswordResult result = await _mediator.Send(request);

                return result.IsValid ? Ok(result) : UnprocessableEntity(result);
            }
            catch (Exception ex)
            {
                string message = "Internal Server Error";

                _logger.LogError(ex, message);

                return StatusCode(500, message);
            }
        }
    }
}
