using MediatR;
using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Results;
using System;

namespace PasswordValidator.Domain.Requests
{
    public class ValidatePasswordRequest : IRequest<ValidatePasswordResult>
    {
        public ValidatePasswordRequest(Password password)
        {
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public Password Password { get; private set; }
    }
}
