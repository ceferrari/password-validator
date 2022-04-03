using PasswordValidator.Domain.Models.Passwords;
using PasswordValidator.Domain.Models.PasswordsRules.Abstractions;

namespace PasswordValidator.Domain.Models.PasswordsRules
{
    public class SpecialPasswordRule : MinCharsPasswordRule
    {
        public override bool IsValid(Password password)
        {
            int count = 0;

            foreach (char c in password.Value)
            {
                if (Chars.Contains(c) && ++count == Min)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
