using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules.Abstractions;

namespace PasswordValidator.Domain.Models.PasswordsRules
{
    public class LengthPasswordRule : MinMaxPasswordRule
    {
        public override bool IsValid(Password password)
        {
            int len = password.Value.Length;

            return len >= Min && len <= Max;
        }
    }
}
