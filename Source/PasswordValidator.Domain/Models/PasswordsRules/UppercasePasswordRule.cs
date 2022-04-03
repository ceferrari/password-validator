using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules.Abstractions;

namespace PasswordValidator.Domain.Models.PasswordsRules
{
    public class UppercasePasswordRule : MinPasswordRule
    {
        public override bool IsValid(Password password)
        {
            int count = 0;

            foreach (char c in password.Value)
            {
                if (c >= 'A' && c <= 'Z' && ++count == Min)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
