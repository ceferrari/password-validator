using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules.Abstractions;

namespace PasswordValidator.Domain.Models.PasswordsRules
{
    public class LowercasePasswordRule : MinPasswordRule
    {
        public override bool IsValid(Password password)
        {
            int count = 0;

            foreach (char c in password.Value)
            {
                if (c >= 'a' && c <= 'z' && ++count == Min)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
