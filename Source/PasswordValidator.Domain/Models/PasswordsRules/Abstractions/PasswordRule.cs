using PasswordValidator.Domain.Models.Passwords;
using System.Diagnostics.CodeAnalysis;

namespace PasswordValidator.Domain.Models.PasswordsRules.Abstractions
{
    [ExcludeFromCodeCoverage]
    public abstract class PasswordRule
    {
        public bool IsActive { get; set; }
        public virtual string ErrorMsg { get; set; }

        public abstract bool IsValid(Password password);
    }
}
