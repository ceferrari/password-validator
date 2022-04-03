using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules.Abstractions;

namespace PasswordValidator.Domain.Models.PasswordsRules
{
    public class AllowedPasswordRule : MinCharsPasswordRule
    {
        public override bool IsValid(Password password)
        {
            foreach (char c in password.Value)
            {
                if (!Chars.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
