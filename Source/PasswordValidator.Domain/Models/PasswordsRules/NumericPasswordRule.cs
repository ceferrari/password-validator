using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules.Abstractions;

namespace PasswordValidator.Domain.Models.PasswordsRules
{
    public class NumericPasswordRule : MinPasswordRule
    {
        public override bool IsValid(Password password)
        {
            int count = 0;

            foreach (char c in password.Value)
            {
                if (c >= '0' && c <= '9' && ++count == Min)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
