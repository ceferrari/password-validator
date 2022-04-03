using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules.Abstractions;
using PasswordValidator.Domain.Results;
using PasswordValidator.Domain.Settings;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordValidator.Domain.Requests
{
    public class ValidatePasswordRequestHandler : IRequestHandler<ValidatePasswordRequest, ValidatePasswordResult>
    {
        private readonly PasswordsRulesOptions _passwordsRulesOptions;
        private readonly ILogger<ValidatePasswordRequestHandler> _logger;

        public ValidatePasswordRequestHandler(IOptionsSnapshot<PasswordsRulesOptions> passwordsRulesOptions, ILogger<ValidatePasswordRequestHandler> logger)
        {
            _passwordsRulesOptions = passwordsRulesOptions.Value;
            _logger = logger;
        }

        public async Task<ValidatePasswordResult> Handle(ValidatePasswordRequest request, CancellationToken cancellationToken)
        {
            List<string> errors = new();

            // tenho ciência do custo computacional de usar Reflection, mas é o trade-off
            // para ter as opções dinâmicas, parametrizadas e configuráveis no appsettings

            foreach (PropertyInfo pi in _passwordsRulesOptions.GetType().GetProperties())
            {
                PasswordRule passwordRule = (PasswordRule)pi.GetValue(_passwordsRulesOptions);

                bool isActive = passwordRule?.IsActive == true;
                bool isValid = passwordRule.IsValid(request.Password);

                if (isActive && !isValid) errors.Add(passwordRule.ErrorMsg);

                LogValidation(request.Password, pi.Name, isActive, isValid);
            }

            return await Task.FromResult(new ValidatePasswordResult(errors));
        }

        private void LogValidation(Password password, string ruleName, bool isActive, bool isValid)
        {
            string result = "PASS";
            if (!isActive) result = "SKIP";
            else if (!isValid) result = "FAIL";

            // apenas debug/testes/desenvolvimento. a senha jamais deve ser logada em ambiente produtivo

            _logger.LogDebug($"[{result}] Validating password '{password.Value}' using rule '{ruleName}'");
        }
    }
}
